using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using VanTriShop.Model.Models;
using VanTriShop.Service;
using VanTriShop.Web.Infrastructure.Core;
using VanTriShop.Web.Infrastructure.Extensions;
using VanTriShop.Web.Models;

namespace VanTriShop.Web.Api
{
	[Route("api/[controller]")]
	[ApiController]
	public class PostCategoryController : ApiControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IPostCategoryService _postCategoryService;

		public PostCategoryController(IErrorService errorService, IPostCategoryService postCategoryService, IMapper mapper)
			: base(errorService)
		{
			_postCategoryService = postCategoryService;
			_mapper = mapper;	
		}

		[HttpGet("getall")]
		public IActionResult GetAll()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Invalid model state.");
			}
			try
			{
				var listCategory = _postCategoryService.GetAll();

				var listPostCaetgoryVM = _mapper.Map<List<PostCategoryViewModel>>(listCategory);

				_postCategoryService.Save();

				return Ok(listPostCaetgoryVM);
			}
			catch (Exception ex)
			{
				return HandleException(ex);
			}
		}

		[HttpPost]
		public IActionResult Post([FromBody] PostCategoryViewModel postCategoryVM)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Invalid model state.");
			}

			try
			{
				var newPostCategory = new PostCategory();
				newPostCategory.UpdatePostCategory(postCategoryVM);

				var category = _postCategoryService.Add(newPostCategory);
				_postCategoryService.Save();

				return CreatedAtAction(nameof(GetAll), new { id = category.ID }, category);
			}
			catch (Exception ex)
			{
				// Gọi HandleException từ ApiControllerBase để xử lý ngoại lệ
				return HandleException(ex);
			}
		}

		[HttpPut]
		public IActionResult Put([FromBody] PostCategoryViewModel postCategoryVM)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Invalid model state.");
			}
			try
			{
				var postCategoryDb = _postCategoryService.GetById(postCategoryVM.ID);
				postCategoryDb.UpdatePostCategory(postCategoryVM);

				_postCategoryService.Update(postCategoryDb);
				_postCategoryService.Save();

				return Ok("Update successful.");
			}
			catch (Exception ex)
			{
				return HandleException(ex);
			}

			
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Invalid model state.");
			}
			try
			{
				_postCategoryService.Delete(id);
				_postCategoryService.Save();

				return Ok("Delete successful.");
			}
			catch (Exception ex)
			{
				return HandleException(ex);
			}
			
		}
	}
}
