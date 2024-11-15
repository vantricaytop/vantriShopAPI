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
	public interface IErrorService
	{
		Error Create(Error error);
		void Save();
	}
	public class ErrorService : IErrorService
	{
		private readonly IErrorRepository _errorRepository;
		private readonly IUnitOfWork _unitOfWork;

		public ErrorService(IErrorRepository errorRepository, IUnitOfWork unitOfWork)
		{
			_errorRepository = errorRepository;
			_unitOfWork = unitOfWork;
		}

		public Error Create(Error error)
		{
			return _errorRepository.Add(error);
		}

		public void Save()
		{
			_unitOfWork.Commit();
		}
	}
}
