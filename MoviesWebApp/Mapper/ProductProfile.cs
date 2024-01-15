using AutoMapper;
using MoviesWebApp.Models.ViewModels;
using MoviesWebApp.Models;
using ProductsWebApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductsWebApp.Models.Domain;

namespace ProductsWebApp.Mapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<AddProductRequest, Product>();
            CreateMap<Product, EditProductRequest>()
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src =>
                src.Categories.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })))
            .ForMember(dest => dest.SelectedCategories, opt => opt.MapFrom(src =>
                src.Categories.Select(x => x.Id.ToString()).ToArray()));

            CreateMap<EditProductRequest, Product>();

		}
    }
}
