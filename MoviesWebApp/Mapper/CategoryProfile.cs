using AutoMapper;
using MoviesWebApp.Models;
using MoviesWebApp.Models.ViewModels;
using ProductsWebApp.Models.ViewModels;

namespace ProductsWebApp.Mapper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<AddCategoryRequest, Category>();
            CreateMap<EditCategoryRequest, Category>();
            CreateMap<Category, EditCategoryRequest>();
        }
    }
}
