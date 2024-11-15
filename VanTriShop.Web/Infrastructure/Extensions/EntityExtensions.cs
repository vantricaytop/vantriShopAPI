using VanTriShop.Model.Models;
using VanTriShop.Web.Models;

namespace VanTriShop.Web.Infrastructure.Extensions
{
	public static class EntityExtensions
	{
		public static void UpdatePostCategory(this PostCategory postCategory, PostCategoryViewModel postCategoryViewModel)
		{
			postCategory.ID = postCategoryViewModel.ID;
			postCategory.Name = postCategoryViewModel.Name;
			postCategory.Alias = postCategoryViewModel.Alias;
			postCategory.Description = postCategoryViewModel.Description;
			postCategory.ParentID = postCategoryViewModel.ParentID;
			postCategory.DisplayOrder = postCategoryViewModel.DisplayOrder;
			postCategory.Image = postCategoryViewModel.Image;
			postCategory.HomeFlag = postCategoryViewModel.HomeFlag;
			
			postCategory.CreatedBy = postCategoryViewModel.CreatedBy;	
			postCategory.UpdatedBy = postCategoryViewModel.UpdatedBy;
			postCategory.UpdatedDate = postCategoryViewModel.UpdatedDate;
			postCategory.CreatedDate = postCategoryViewModel.CreatedDate;
			postCategory.Status = postCategoryViewModel.Status;
			postCategory.MetaDescription = postCategoryViewModel.MetaDescription;
			postCategory.MetaKeyword = postCategoryViewModel.MetaKeyword;
		}

		public static void UpdatePost(this Post post, PostViewModel postVM)
		{
			post.Id = postVM.Id;
			post.Name = postVM.Name;
			post.Alias = postVM.Alias;
			post.Description = postVM.Description;
			post.CategoryID = postVM.CategoryID;
			post.Content = postVM.Content;
			post.Image = postVM.Image;
			post.HomeFlag = postVM.HomeFlag;
			post.ViewCount = postVM.ViewCount;
			post.HotFlag = postVM.HotFlag;

			post.CreatedDate = postVM.CreatedDate;
			post.Status = postVM.Status;
			post.MetaDescription = postVM.MetaDescription;
			post.UpdatedDate = postVM.UpdatedDate;
			post.CreatedBy = postVM.CreatedBy;
			post.UpdatedBy = postVM.UpdatedBy;
			post.MetaKeyword = postVM.MetaKeyword;
		}
	}
}
