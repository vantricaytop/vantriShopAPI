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
	public interface IPageService
	{
		Page GetByAlias(string alias);
	}
	public class PageService : IPageService
	{
		IPageRepository _pageRepository;
		IUnitOfWork _unitOfWork;

		public PageService(IPageRepository pageRepository, IUnitOfWork unitOfWork)
		{
			_pageRepository = pageRepository;
			_unitOfWork = unitOfWork;
		}

		public Page GetByAlias(string alias)
		{
			return _pageRepository.GetSingleByCondition(x => x.Alias == alias);
		}
	}
}
