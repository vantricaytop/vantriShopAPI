using System;
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
	public interface ICommonServvice
	{
		Footer GetFooter();
		IEnumerable<Slide> GetSlides();
		SystemConfig GetSystemConfig(string code);
	}
	public class CommonService : ICommonServvice
	{
		private readonly IFooterRepository _footerRepository;
		private IUnitOfWork _unitOfWork;
		private readonly ISlideRepository _slideRepository;
		private readonly ISystemConfigRepository _systemConfigRepository;
		public CommonService(IFooterRepository footerRepository, IUnitOfWork unitOfWork, ISlideRepository slideRepository, ISystemConfigRepository systemConfigRepository) { 
			_footerRepository = footerRepository;
			_unitOfWork = unitOfWork;
			_slideRepository = slideRepository;
			_systemConfigRepository = systemConfigRepository;
		}

		public Footer GetFooter()
		{
			return _footerRepository.GetSingleByCondition(x => x.Id == CommonConstants.DefaultFooterId);
		}

		public IEnumerable<Slide> GetSlides()
		{
			return _slideRepository.GetMulti(X => X.Status);
		}

		public SystemConfig GetSystemConfig(string code)
		{
			return _systemConfigRepository.GetSingleByCondition(X => X.Code == code);
		}
	}
}
