using AutoMapper;
using VanTriShop.Model.Models;
using VanTriShop.Web.Models;
using VanTriShop.Web.Models.Common;
using VanTriShop.Web.Models.Product;

namespace VanTriShop.Web.Mappings
{
    public class AutoMapperConfiguration : Profile
	{
		public AutoMapperConfiguration() {
			CreateMap<Post, PostViewModel>();
			CreateMap<PostCategory, PostCategoryViewModel>();
			CreateMap<Tag, TagViewModel>().ReverseMap();
			CreateMap<PostTag, PostTagViewModel>().ReverseMap();
			CreateMap<ProductCategory, ProductCategoryViewModel>().ReverseMap();
			CreateMap<Product, ProductViewModel>().ReverseMap();

		}
		
	}
}
