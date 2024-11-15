﻿using AutoMapper;
using VanTriShop.Model.Models;
using VanTriShop.Web.Models;

namespace VanTriShop.Web.Mappings
{
	public class AutoMapperConfiguration : Profile
	{
		public AutoMapperConfiguration() {
			CreateMap<Post, PostViewModel>();
			CreateMap<PostCategory, PostCategoryViewModel>();
			CreateMap<Tag, TagViewModel>();
			CreateMap<PostTag, PostTagViewModel>();
		}
		
	}
}