using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ProductsWebApp.Models.ViewModels;

namespace ProductsWebApp.Mapper
{
	public class AccountsProfile : Profile
	{
        public AccountsProfile()
        {
            CreateMap<RegisterViewModel, IdentityUser>();
		}
    }
}
