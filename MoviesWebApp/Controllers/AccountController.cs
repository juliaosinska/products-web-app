using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductsWebApp.Models.ViewModels;

namespace ProductsWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
		private readonly SignInManager<IdentityUser> signInManager;
		private readonly IMapper mapper;

		public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
			this.mapper = mapper;
		}

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            //mapowanie z view model na domain model
            var identityUser = mapper.Map<RegisterViewModel, IdentityUser>(registerViewModel);

            //tworzenie uzytkownika w bazie przez repozytorium
            var identityResult = await userManager.CreateAsync(identityUser, registerViewModel.Password);

            //jesli pomyslnie utworzono
            if (identityResult.Succeeded)
            {
                //nadajemy mu role usera
                var roleIdentityResult = await userManager.AddToRoleAsync(identityUser, "User");

                //jesli pomyslnie nadano role
                if (roleIdentityResult.Succeeded)
                {
					ViewBag.SuccessMessage = "Rejestracja zakonczona pomyslnie!";
					return RedirectToAction("Register");
                }
            }
			ViewBag.ErrorMessage = "Blad podczas rejestracji. Haslo musi zawierac co najmniej 1 wielka litere, 1 znak specjalny, 1 cyfre i miec dlugosc co najmniej 8 znakow.";
			return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var model = new LoginViewModel 
            { 
                ReturnUrl = returnUrl 
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            //logowanie z pomoca SignInManagera i hasla
            var signInResult = await signInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, false, false);

            if (signInResult != null && signInResult.Succeeded)
            {
                if (!string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl))
                {
                    return Redirect(loginViewModel.ReturnUrl);
                }
                return RedirectToAction("Index", "Home");
            }

			ViewBag.ErrorMessage = "Blad podczas logowania. Sprobuj ponownie.";
			return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        { 
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
