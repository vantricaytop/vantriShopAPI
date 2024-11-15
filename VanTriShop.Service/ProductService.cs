using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Util;
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

		IEnumerable<Product> GetAll(string keyword);

		IEnumerable<Product> GetLastest(int top);
		IEnumerable<Product> GetHotProduct(int top);
		IEnumerable<Product> GetListProductByCategoryIdPaging(int categoryId, int pagge, int pageSize, string sort, out int totalRow);
		IEnumerable<Product> Search(string keyword, int page, int pageSize, string sort, out int totalRow);
		IEnumerable<Product> GetListProduct(string keyword);
		IEnumerable<Product> GetReadedProducts(int id, int top);
		IEnumerable<string> GetListProductByName(string namw);
		Product GetById(int id);
		void Save();
		IEnumerable<Tag> GetListTagByProductId(int id);
		Tag GetTag(string tagId);
		void IncreaseView(int id);
		IEnumerable<Product> GetListProductByTag(string tagId, int page, int pageSize, out int totalRow);
		bool SellProduct(int productId, int quantity);


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
			/*var productItem = _productRepository.Add(product);
			_unitOfWork.Commit();
			if (!string.IsNullOrEmpty(product.Tags))
			{
				string[] tags = product.Tags.Split(',');
				for (var i = 0; i < tags.Length; i++)
				{
					var tagId = 
				}
			}*/
			return product;
		}

		public Product Delete(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Product> GetAll()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Product> GetAll(string keyword)
		{
			throw new NotImplementedException();
		}

		public Product GetById(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Product> GetHotProduct(int top)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Product> GetLastest(int top)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Product> GetListProduct(string keyword)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Product> GetListProductByCategoryIdPaging(int categoryId, int pagge, int pageSize, string sort, out int totalRow)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<string> GetListProductByName(string namw)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Product> GetListProductByTag(string tagId, int page, int pageSize, out int totalRow)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Tag> GetListTagByProductId(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Product> GetReadedProducts(int id, int top)
		{
			throw new NotImplementedException();
		}

		public Tag GetTag(string tagId)
		{
			throw new NotImplementedException();
		}

		public void IncreaseView(int id)
		{
			throw new NotImplementedException();
		}

		public void Save()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Product> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
		{
			throw new NotImplementedException();
		}

		public bool SellProduct(int productId, int quantity)
		{
			throw new NotImplementedException();
		}

		public void Update(Product product)
		{
			throw new NotImplementedException();
		}
	}
}
