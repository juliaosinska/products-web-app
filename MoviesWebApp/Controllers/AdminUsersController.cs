using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductsWebApp.Models.Domain;
using ProductsWebApp.Models.ViewModels;
using ProductsWebApp.Repositories;

namespace ProductsWebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;

        public AdminUsersController(IUserRepository userRepository, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var users = await userRepository.GetAll();

            var usersViewModel = new UserViewModel();
            usersViewModel.Users = new List<User>();

            foreach (var user in users)
            {
                usersViewModel.Users.Add(new Models.ViewModels.User
                {
                    Id = Guid.Parse(user.Id),
                    Username = user.UserName,
                    EmailAddress = user.Email
                });
            }

            return View(usersViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> List(UserViewModel request)
        {
            var identityUser = new IdentityUser
            {
                UserName = request.Username,
                Email = request.Email
            };

            var identityResult = await userManager.CreateAsync(identityUser, request.Password);

            if (identityResult != null) 
            {
                if (identityResult.Succeeded)
                {
                    var roles = new List<string> { "User" };

                    if (request.AdminRoleCheckbox)
                    {
                        roles.Add("Admin");
                    }

                    identityResult = await userManager.AddToRolesAsync(identityUser, roles);

                    if (identityResult != null && identityResult.Succeeded) 
                    {
                        return RedirectToAction("List", "AdminUsers");
                    }

                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());

            if (user != null)
            {
                var identityResult = await userManager.DeleteAsync(user);

                if (identityResult != null && identityResult.Succeeded) 
                {
                    return RedirectToAction("List", "AdminUsers");
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, UserViewModel request)
        {
            var user = await userManager.FindByIdAsync(id.ToString());

            if (user != null)
            {
                var currentRoles = await userManager.GetRolesAsync(user);
                var identityResult = await userManager.RemoveFromRolesAsync(user, currentRoles);

                if (identityResult.Succeeded)
                {
                    var rolesToAdd = new List<string> { "User" };

                    if (request.AdminRoleCheckbox)
                    {
                        rolesToAdd.Add("Admin");
                    }

                    identityResult = await userManager.AddToRolesAsync(user, rolesToAdd);

                    if (identityResult != null && identityResult.Succeeded)
                    {
                        return RedirectToAction("List", "AdminUsers");
                    }
                }
            }

            return View();
        }
    }
}
