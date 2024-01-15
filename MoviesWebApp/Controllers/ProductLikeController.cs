using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesWebApp.Models;
using ProductsWebApp.Models.Domain;
using ProductsWebApp.Models.ViewModels;
using ProductsWebApp.Repositories;

namespace ProductsWebApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductLikeController : ControllerBase
	{
		private readonly IProductLikeRepository productLikeRepository;
		private readonly IMapper mapper;

		public ProductLikeController(IProductLikeRepository productLikeRepository, IMapper mapper)
        {
			this.productLikeRepository = productLikeRepository;
			this.mapper = mapper;
		}

        [HttpPost]
		[Route("Add")]
		public async Task<IActionResult> AddLike([FromBody] AddLikeRequest addLikeRequest)
		{
			//dodanie polubienia
			var model = new ProductLike();
			
			//mapowanie z view model do domain model
			model = mapper.Map<AddLikeRequest, ProductLike>(addLikeRequest);
			
			await productLikeRepository.AddLikeForProduct(model);

			return Ok();
		}

		[HttpGet]
		[Route("{productId:int}/totalLikes")]
		public async Task<IActionResult> GetTotalLikesForProduct([FromRoute] int productId)
		{
			//pobranie informacji o sumie polubien pod produktem
			var totalLikes = await productLikeRepository.GetTotalLikes(productId);

			return Ok(totalLikes);
		}
	}
}
