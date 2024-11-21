using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanTriShop.Data.Infrastructure;
using VanTriShop.Data.Repositories;
using VanTriShop.Model.Models;

namespace VanTriShop.Service
{
	public interface IProductCategoryService
	{
		ProductCategory Add(ProductCategory category);
		void Update(ProductCategory category);	
		ProductCategory Delete(int id);
		IEnumerable<ProductCategory> GetAll();
		IEnumerable<ProductCategory> GetAll(string keyword);
		IEnumerable<ProductCategory> GetAllByParentId(int parentId);
		ProductCategory GetById(int id);
		void Save();
	}
	public class ProductCategoryService : IProductCategoryService
	{
		private readonly IProductCategoryRepository _repository;
		private readonly IUnitOfWork _unitOfWork;
		public ProductCategoryService(IUnitOfWork unitOfWork, IProductCategoryRepository productCategoryRepository) {
			_repository = productCategoryRepository;
			_unitOfWork = unitOfWork;
		}
		public ProductCategory Add(ProductCategory category)
		{
			return _repository.Add(category);
		}

		public ProductCategory Delete(int id)
		{
			return (ProductCategory)_repository.Delete(id);
		}

		public IEnumerable<ProductCategory> GetAll()
		{
			return _repository.GetAll().OrderBy(x => x.ParentID);
		}

		public IEnumerable<ProductCategory> GetAll(string keyword)
		{
			if (!string.IsNullOrEmpty(keyword)) 
				return _repository.GetMulti(x => x.Name.Contains(keyword)).OrderBy(x =>x.ParentID);
			else 
				return _repository.GetAll().OrderBy(x =>x.ParentID);
		}


		public IEnumerable<ProductCategory> GetAllByParentId(int parentId)
		{
			return _repository.GetMulti(x => x.Status && x.ParentID == parentId).OrderBy(x =>x.ParentID);
		}

		public ProductCategory GetById(int id)
		{
			return _repository.GetSingleById(id);
		}

		public void Save()
		{
			_unitOfWork.Commit();
		}

		public void Update(ProductCategory category)
		{
			_repository.Update(category);
		}
	}
}
