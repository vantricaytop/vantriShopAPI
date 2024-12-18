﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanTriShop.Data.Infrastructure;
using VanTriShop.Data.Repositories;
using VanTriShop.Model.Models;

namespace VanTriShop.Service
{
	public interface IPostService
	{
		void Add(Post post);
		void Update(Post post);
		void Delete(int id);
		IEnumerable<Post> GetAll();
		IEnumerable<Post> GetAllByCategoryPaging(int categoryId, int page, int pageSize, out int totalRow);
		IEnumerable<Post> GetAllPaging(int page, int pageSize, out int totalRow);
		Post GetById (int id);
		IEnumerable<Post> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);
		void SaveChanges();
	}
	public class PostService : IPostService
	{
		private readonly IPostRepository _postRepository;
		private readonly IUnitOfWork _unitOfWork;


		public PostService(IUnitOfWork unitOfWork, IPostRepository postRepository)
		{
			_postRepository = postRepository;
			_unitOfWork = unitOfWork;

		}
		public void Add(Post post)
		{
			_postRepository.Add(post);
		}

		public void Delete(int id)
		{
			_postRepository.Delete(id);
		}

		public IEnumerable<Post> GetAll()
		{
			return _postRepository.GetAll(new string[] {"PostCategory"});
		}

		public IEnumerable<Post> GetAllByCategoryPaging(int categoryId, int page, int pageSize, out int totalRow)
		{
			return _postRepository.GetMultiPaging(x => x.Status && x.CategoryID == categoryId, out totalRow, page, pageSize, new string[] { "PostCategory" });
		}

		public IEnumerable<Post> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
		{
			//TODO: select all post by tag
			return _postRepository.GetAllByTag(tag, page, pageSize, out totalRow);
		}

		public IEnumerable<Post> GetAllPaging(int page, int pageSize, out int totalRow)
		{
			return _postRepository.GetMultiPaging(x => x.Status, out totalRow, page, pageSize);
		}

		public Post GetById(int id)
		{
			return _postRepository.GetSingleById(id);
		}

		public void SaveChanges()
		{
			_unitOfWork.Commit();
		}

		public void Update(Post post)
		{
			_postRepository.Update(post);
		}
	}
}
