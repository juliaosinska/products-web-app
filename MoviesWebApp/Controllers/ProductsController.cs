using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesWebApp.Models.ViewModels;
using MoviesWebApp.Models;
using ProductsWebApp.Repositories;
using System.Runtime.CompilerServices;
using ProductsWebApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration.UserSecrets;
using ProductsWebApp.Models.Domain;

namespace ProductsWebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IProductLikeRepository productLikeRepository;
        private readonly IMapper mapper;
		private readonly SignInManager<IdentityUser> signInManager;
		private readonly UserManager<IdentityUser> userManager;
		private readonly IProductCommentRepository productCommentRepository;

		public ProductsController(IProductRepository productRepository, IProductLikeRepository productLikeRepository, IMapper mapper,
            SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IProductCommentRepository productCommentRepository)
        {
            this.productRepository = productRepository;
            this.productLikeRepository = productLikeRepository;
            this.mapper = mapper;
			this.signInManager = signInManager;
			this.userManager = userManager;
			this.productCommentRepository = productCommentRepository;
		}

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var product = await productRepository.GetAsync(id);
            var productDetailsViewModel = new ProductDetailsViewModel();
            var liked = false;

            if (product != null)
            {
                var totalLikes = await productLikeRepository.GetTotalLikes(product.Id);

                //jesli uzytkownik jest zalogowany
                if (signInManager.IsSignedIn(User))
                {
                    var likesForProduct = await productLikeRepository.GetLikesForProduct(product.Id);
                    var userId = userManager.GetUserId(User);

                    //sprawdzamy czy juz polubil dany produkt
                    if (userId != null)
                    {
                        var likeFromUser = likesForProduct.FirstOrDefault(x => x.UserId == Guid.Parse(userId));
                        liked = likeFromUser != null;
                    }
				}

                //pobranie komentarzy do danego produktu
                var productComments = await productCommentRepository.GetCommentsByProductIdAsync(product.Id);
                
                var productCommentsForView = new List<ProductCommentViewModel>();

				foreach (var comment in productComments)
				{
                    //mapowanie z domain view do view model
					var productCommentViewModel = mapper.Map<ProductComment, ProductCommentViewModel>(comment);
					productCommentViewModel.Username = (await userManager.FindByIdAsync(comment.UserId.ToString())).UserName;
					productCommentsForView.Add(productCommentViewModel);
				}

                //mapowanie z domain model na view model
				productDetailsViewModel = mapper.Map<Product, ProductDetailsViewModel>(product);
				productDetailsViewModel.TotalLikes = totalLikes;
				productDetailsViewModel.Liked = liked;
                productDetailsViewModel.Comments = productCommentsForView;
			}

            return View(productDetailsViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ProductDetailsViewModel productDetailsViewModel)
        {
            //jesli uzytkownik jest zalogowany
            if (signInManager.IsSignedIn(User))
            {
                //ma mozliwosc dodania komentarza
                var productComment = new ProductComment();

                //mapowanie z view model na domain model
                productComment = mapper.Map<ProductDetailsViewModel, ProductComment>(productDetailsViewModel);
                productComment.UserId = Guid.Parse(userManager.GetUserId(User));
                productComment.DateAdded = DateTime.Now;

				await productCommentRepository.AddAsync(productComment);
                return RedirectToAction("Index", "Products", new { id = productDetailsViewModel.Id });
            }

            return View();
        }
    }
}
