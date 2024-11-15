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
	public interface IPostCategoryService
	{
		PostCategory Add(PostCategory postCategory);
		void Update(PostCategory postCategory);
		void Delete(int id);
		IEnumerable<PostCategory> GetAll();
		IEnumerable<PostCategory> GetByParentId(int parentID);

		PostCategory GetById(int id);
		void Save();
	}
	public class PostCategoryService : IPostCategoryService
	{
		private readonly IPostCategoryRepository _postCategoryRepository;
		private readonly IUnitOfWork _unitOfWork;
		public PostCategoryService(IPostCategoryRepository repository, IUnitOfWork unitOfWork) { 
			_postCategoryRepository = repository;
			_unitOfWork = unitOfWork;
		}
		public PostCategory Add(PostCategory postCategory)
		{
			return _postCategoryRepository.Add(postCategory);
		}

		public void Delete(int id)
		{
			_postCategoryRepository.Delete(id);
		}

		public IEnumerable<PostCategory> GetAll()
		{
			return _postCategoryRepository.GetAll();
		}

		public PostCategory GetById(int id)
		{
			return _postCategoryRepository.GetSingleById(id); 
		}

		public IEnumerable<PostCategory> GetByParentId(int parentID)
		{
			return _postCategoryRepository.GetMulti(x => x.Status && x.ParentID == parentID);
		}

		public void Save()
		{
			_unitOfWork.Commit();
		}

		public void Update(PostCategory postCategory)
		{
			_postCategoryRepository.Update(postCategory);
		}
	}
}
