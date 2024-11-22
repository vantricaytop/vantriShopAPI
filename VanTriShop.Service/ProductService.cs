﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanTriShop.Common;
using VanTriShop.Data.Infrastructure;
using VanTriShop.Data.Repositories;
using VanTriShop.Model.Models;

namespace VanTriShop.Service
{
	public interface IProductService
	{
		Product Add(Product product);
		void Update(Product product);	
		Product Delete(int id);
		IEnumerable<Product> GetAll();

		IEnumerable<Product> GetAll(int? categoryId, string keyword);

		IEnumerable<Product> GetLastest(int top);
		IEnumerable<Product> GetHotProduct(int top);
		IEnumerable<Product> GetListProductByCategoryIdPaging(int categoryId, int pagge, int pageSize, string sort, out int totalRow);
		IEnumerable<Product> Search(string keyword, int page, int pageSize, string sort, out int totalRow);
		IEnumerable<Product> GetListProduct(string keyword);
		IEnumerable<Product> GetRelatedProducts(int id, int top);
		IEnumerable<string> GetListProductByName(string namw);
		Product GetById(int id);
		void Save();
		IEnumerable<Tag> GetListTagByProductId(int id);
		Tag GetTag(string tagId);
		void IncreaseView(int id);
		IEnumerable<Product> GetListProductByTag(string tagId, int page, int pageSize, out int totalRow);
		bool SellProduct(int productId, int quantity);

		IEnumerable<Tag> GetListProductTag(string searchText);


	}
	public class ProductService : IProductService

	{
		private IProductRepository _productRepository;
		private ITagRepository _tagRepository;
		private IProductTagRepository _productTagRepository;
		private IUnitOfWork _unitOfWork;

		public ProductService(IProductRepository productRepository, ITagRepository tagRepository, IProductTagRepository productTagRepository, IUnitOfWork unitOfWork)
		{
			_productRepository = productRepository;
			_tagRepository = tagRepository;
			_productTagRepository = productTagRepository;
			_unitOfWork = unitOfWork;
		}

		public Product Add(Product product)
		{
			var productItem = _productRepository.Add(product);
			_unitOfWork.Commit();
			if (!string.IsNullOrEmpty(product.Tags))
			{
				string[] tags = product.Tags.Split(',');
				for (var i = 0; i< tags.Length; i++)
				{
					var tagId = StringHelper.ToUnsignString(tags[i]);	
					if (_tagRepository.Count(x => x.ID == tagId) == 0)
					{
						Tag tag = new Tag();
						tag.ID = tagId;
						tag.Name = tags[i];
						tag.Type = CommonConstants.ProductTag;
						_tagRepository.Add(tag);
					}

					ProductTag productTag = new ProductTag();
					productTag.ProductID = product.Id;
					productTag.TagID = tagId;
					_productTagRepository.Add(productTag);
				}
			}
			return productItem;

		}

		public Product Delete(int id)
		{
			return _productRepository.Delete(id);
		}

		public IEnumerable<Product> GetAll()
		{
			return _productRepository.GetAll(new string[]
			{
				"ProductCategory", "ProductTags"
			});
		}

		public IEnumerable<Product> GetAll(int? categoryId, string keyword)
		{
			var query = _productRepository.GetAll(new string[]
			{
				"ProductCategory", "ProductTags"
			});
			if (!string.IsNullOrEmpty(keyword))
				query = query.Where(x => x.Name.Contains(keyword));

			if (categoryId.HasValue)
				query = query.Where(x => x.CategoryID == categoryId.Value);
			return query;
		}

		public Product GetById(int id)
		{
			return _productRepository.GetSingleById(id);
		}

		public IEnumerable<Product> GetHotProduct(int top)
		{
			return _productRepository.GetMulti(x => x.Status && x.HotFlag == true).OrderByDescending(x => x.CreatedDate).Take(top);
		}

		public IEnumerable<Product> GetLastest(int top)
		{
			return _productRepository.GetMulti(x => x.Status).OrderByDescending(x => x.CreatedDate).Take(top);

		}

		public IEnumerable<Product> GetListProduct(string keyword)
		{
			IEnumerable<Product> query;
			if (!string.IsNullOrEmpty(keyword))
				query = _productRepository.GetMulti(x => x.Name.Contains(keyword));
			else
				query = _productRepository.GetAll();
			return query;
		}

		public IEnumerable<Product> GetListProductByCategoryIdPaging(int categoryId, int pagge, int pageSize, string sort, out int totalRow)
		{
			var query = _productRepository.GetMulti(x => x.Status && x.CategoryID == categoryId);
			switch(sort)
			{
				case "popular":
					query = query.OrderByDescending(x => x.ViewCount);
					break;
				case "discount":
					query = query.OrderByDescending(x => x.PromotionPrice.HasValue);
					break;
				case "price":
					query = query.OrderBy(x => x.Price);
					break;
				default:
					query = query.OrderByDescending(x => x.CreatedDate);
					break;
			}
			totalRow = query.Count();
			return query.Skip((pagge - 1) * pageSize).Take(pageSize);
		}

		public IEnumerable<string> GetListProductByName(string name)
		{
			return _productRepository.GetMulti(x => x.Status && x.Name.Contains(name)).Select(y => y.Name);
		}

		public IEnumerable<Product> GetListProductByTag(string tagId, int page, int pageSize, out int totalRow)
		{
			var model = _productRepository.GetListProductByTag(tagId, page, pageSize, out totalRow);
            return model;
		}

		public IEnumerable<Tag> GetListProductTag(string searchText)
		{
			return _tagRepository.GetMulti(x => x.Type == CommonConstants.ProductTag && searchText.Contains(x.Name));
		}

		public IEnumerable<Tag> GetListTagByProductId(int id)
		{
			return _productTagRepository.GetMulti(x => x.ProductID == id, new string[] { "Tag" }).Select(y=>y.Tag);
		}

		public IEnumerable<Product> GetRelatedProducts(int id, int top)
		{
			var product = _productRepository.GetSingleById(id);
			return _productRepository.GetMulti(x => x.Status && x.Id != id && x.CategoryID == product.CategoryID).OrderByDescending(x => x.CreatedDate).Take(top);
		}

		public Tag GetTag(string tagId)
		{
			return _tagRepository.GetSingleByCondition(x => x.ID == tagId);
		}

		public void IncreaseView(int id)
		{
			var product = _productRepository.GetSingleById(id);
			if (product.ViewCount.HasValue)
			{
				product.ViewCount += 1;
			} else
			{
				product.ViewCount = 1;
			}
		}

		public void Save()
		{
			_unitOfWork.Commit();
		}

		public IEnumerable<Product> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
		{
			var query = _productRepository.GetMulti(x => x.Status && x.Name.Contains(keyword));
			switch (sort)
			{
				case "popular":
					query = query.OrderByDescending(x => x.ViewCount);
					break;

				case "discount":
					query = query.OrderByDescending(x => x.PromotionPrice.HasValue);
					break;

				case "price":
					query = query.OrderBy(x => x.Price);
					break;

				default:
					query = query.OrderByDescending(x => x.CreatedDate);
					break;
			}
			totalRow = query.Count();

			return query.Skip((page - 1) * pageSize).Take(pageSize);
		}

		public bool SellProduct(int productId, int quantity)
		{
			var product = _productRepository.GetSingleById(productId);
			if (product.Quantity < quantity)
			   return false;
			product.Quantity -= quantity;
			return true;
		}

		public void Update(Product product)
		{
			 _productRepository.Update(product);
			if (!string.IsNullOrEmpty(product.Tags))
			{
				string[] tags = product.Tags.Split(',');
				for (var i = 0; i < tags.Length; i++)
				{
					var tagId = StringHelper.ToUnsignString(tags[i]);
					if (_tagRepository.Count(x => x.ID == tagId) == 0)
					{
						Tag tag = new Tag();
						tag.ID = tagId;
						tag.Name = tags[i];
						tag.Type = CommonConstants.ProductTag;
						_tagRepository.Add(tag);
					}
					_productTagRepository.DeleteMulti(x => x.ProductID == product.Id);

					ProductTag productTag = new ProductTag();
					productTag.ProductID = product.Id;
					productTag.TagID = tagId;
					_productTagRepository.Add(productTag);
				}
			}
		}
	}
}
