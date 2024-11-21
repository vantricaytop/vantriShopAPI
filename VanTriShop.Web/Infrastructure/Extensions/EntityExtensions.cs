using VanTriShop.Model.Models;
using VanTriShop.Web.Models;
using VanTriShop.Web.Models.Product;

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

		public static void UpdateProductCategory(this ProductCategory productCategory, ProductCategoryViewModel productCategoryVm)
		{
			productCategory.Id = productCategoryVm.ID;
			productCategory.Name = productCategoryVm.Name;
			productCategory.Description = productCategoryVm.Description;
			productCategory.Alias = productCategoryVm.Alias;
			productCategory.ParentID = productCategoryVm.ParentID;
			productCategory.DisplayOrder = productCategoryVm.DisplayOrder;
			productCategory.HomeOrder = productCategoryVm.HomeOrder;
			productCategory.Image = productCategoryVm.Image;
			productCategory.HomeFlag = productCategoryVm.HomeFlag;
			productCategory.HomeOrder = productCategoryVm.HomeOrder;
			productCategory.CreatedDate = productCategoryVm.CreatedDate;
			productCategory.CreatedBy = productCategoryVm.CreatedBy;
			productCategory.UpdatedDate = productCategoryVm.UpdatedDate;
			productCategory.UpdatedBy = productCategoryVm.UpdatedBy;
			productCategory.MetaKeyword = productCategoryVm.MetaKeyword;
			productCategory.MetaDescription = productCategoryVm.MetaDescription;
			productCategory.Status = productCategoryVm.Status;
		}


		public static void UpdateProduct(this Product product, ProductViewModel productVM)
		{
			product.Id = productVM.ID;
			product.Name = productVM.Name;
			product.Description = productVM.Description;
			product.Alias = productVM.Alias;
			product.CategoryID = productVM.CategoryID;
			product.Content = productVM.Content;
			product.ThumbnailImage = productVM.ThumbnailImage;
			product.Price = productVM.Price;
			product.PromotionPrice = productVM.PromotionPrice;
			product.Warranty = productVM.Warranty;
			product.HomeFlag = productVM.HomeFlag;
			product.HotFlag = productVM.HotFlag;
			product.ViewCount = productVM.ViewCount;

			product.CreatedBy = productVM.CreatedBy;
			product.CreatedDate = productVM.CreatedDate;
			product.UpdatedDate = productVM.UpdatedDate;
			product.UpdatedBy = productVM.UpdatedBy;
			product.MetaDescription = productVM.MetaDescription;
			product.Status = productVM.Status;
			product.MetaKeyword = productVM.MetaKeyword;
			product.Tags = productVM.Tags;
			product.OriginalPrice = productVM.OriginalPrice;
		}

		public static void UpdateProductQuantity(this ProductQuantity quantity, ProductQuantityViewModel quantityVM )
		{
			quantity.ColorId = quantityVM.ColorId;
			quantity.ProductId = quantityVM.ProductId;
			quantity.SizeId = quantityVM.SizeId;
			quantity.Quantity = quantityVM.Quantity;
		}

		public static void UpdateOrder(this Order order, OrderViewModel orderVM)
		{
			order.CustomerName = orderVM.CustomerName;
			order.CustomerAddress = orderVM.CustomerAddress;
			order.CustomerEmail = orderVM.CustomerEmail;
			order.CustomerMobile = orderVM.CustomerMobile;
			order.CustomerMessage = orderVM.CustomerMessage;
			order.PaymentMethod = orderVM.PaymentMethod;
			order.CreatedDate = DateTime.Now;
			order.CreatedBy = orderVM.CreatedBy;
			order.PaymentStatus = orderVM.PaymentStatus;
			order.Status = orderVM.Status;
			order.CustomerID = orderVM.CustomerId;

		}
	}
}
