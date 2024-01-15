using AutoMapper;
using MoviesWebApp.Models;
using ProductsWebApp.Models.Domain;
using ProductsWebApp.Models.ViewModels;

namespace ProductsWebApp.Mapper
{
	public class ProductDetailsProfile : Profile
	{
        public ProductDetailsProfile()
        {
			CreateMap<Product, ProductDetailsViewModel>();
			CreateMap<AddLikeRequest, ProductLike>();
			CreateMap<ProductDetailsViewModel, ProductComment>()
				.ForMember(dest => dest.Description,
			opt => opt.MapFrom(src => src.CommentDescription))
				.ForMember(dest => dest.ProductId,
			opt => opt.MapFrom(src => src.Id))
				.ForMember(x => x.Id, opt => opt.Ignore());
			CreateMap<ProductComment, ProductCommentViewModel>();
		}
    }
}
