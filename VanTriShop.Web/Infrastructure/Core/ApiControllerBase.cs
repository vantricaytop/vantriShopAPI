using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Net;
using VanTriShop.Model.Models;
using VanTriShop.Service;

namespace VanTriShop.Web.Infrastructure.Core
{
	public class ApiControllerBase : ControllerBase
	{
		private readonly IErrorService _errorService;

		public ApiControllerBase(IErrorService errorService)
		{
			_errorService = errorService;
		}

		protected IActionResult HandleException(Exception ex)
		{
			LogError(ex);

			if (ex is DbUpdateException || ex is DbEntityValidationException)
			{
				return BadRequest(new { error = ex.Message });
			}

			return StatusCode((int)HttpStatusCode.InternalServerError, new { error = ex.Message });
		}

		private void LogError(Exception ex)
		{
			try
			{
				var error = new Error
				{
					Message = ex.Message,
					CreatedDate = DateTime.Now,
					StackTrace = ex.StackTrace
				};
				_errorService.Create(error);
				_errorService.Save();
			}
			catch (Exception)
			{
				// Logging failed; handle as necessary
			}
		}
	}
}
