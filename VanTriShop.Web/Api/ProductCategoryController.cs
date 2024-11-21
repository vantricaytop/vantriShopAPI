using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VanTriShop.Service;
using VanTriShop.Web.Infrastructure.Core;
using VanTriShop.Model.Models;
using VanTriShop.Web.Models.Product;
using VanTriShop.Web.Infrastructure.Extensions;
using Lucene.Net.Messages;

namespace VanTriShop.Web.Api
{
    [Route("api/productCategory")]
	//[Authorize]
	[ApiController]
	public class ProductCategoryController : ApiControllerBase
	{
		private readonly IProductCategoryService _productCategoryService;
		private readonly IMapper _mapper;
		public ProductCategoryController(IErrorService errorService,IMapper mapper, IProductCategoryService productCategoryService) : base(errorService)
		{
			_productCategoryService = productCategoryService;
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
				var model = _productCategoryService.GetAll();

				var responseData = _mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
				return Ok(responseData);
			}
			catch (Exception ex)
			{
				return HandleException(ex);
			}
		}

		[HttpGet("getallhierachy")]
		public IActionResult GetAllHierachy()
		{
			try
			{
				var responseData = GetCategoryViewModel();
				return Ok(responseData);
			}
			catch (Exception ex)
			{
				return HandleException(ex);
			}
		
		}

		[HttpGet("detail/{id:int}")]
		public IActionResult GetById(int id)
		{
			try
			{
				var model = _productCategoryService.GetById(id);
				if (model == null)
					return NotFound();

				var responseData = _mapper.Map<ProductCategory, ProductCategoryViewModel>(model);
				return Ok(responseData);
			}
			catch (Exception ex)
			{
				return HandleException(ex);
			}
		}

		[HttpGet("getallpagehierarchy")]
		public IActionResult GetAll(string keyword, int page = 0, int pageSize = 20)
		{
			try
			{
				// Bảo vệ dữ liệu đầu vào
				if (pageSize <= 0 || page < 0)
					return BadRequest("Page size và page index phải là số dương.");

				// Lấy dữ liệu từ service
				var model = _productCategoryService.GetAll(keyword);

				if (model == null || !model.Any())
					return Ok(new PaginationSet<ProductCategoryViewModel>
					{
						Items = new List<ProductCategoryViewModel>(),
						PageIndex = page,
						TotalRows = 0,
						PageSize = pageSize
					});

				// Tính toán số lượng bản ghi
				int totalRow = model.Count();

				// Phân trang
				var query = model.OrderByDescending(x => x.CreatedDate)
								 .Skip(page * pageSize)
								 .Take(pageSize);

				// Ánh xạ sang ViewModel
				var responseData = _mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(query);

				// Tạo đối tượng phân trang
				var paginationSet = new PaginationSet<ProductCategoryViewModel>
				{
					Items = responseData,
					PageIndex = page,
					TotalRows = totalRow,
					PageSize = pageSize
				};

				return Ok(paginationSet);
			}
			catch (Exception ex)
			{
				return HandleException(ex);
			}
		}


		[HttpPost("add")]
		public IActionResult Create([FromBody] ProductCategoryViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var newProductCategory = new ProductCategory();
				newProductCategory.UpdateProductCategory(model);
				newProductCategory.CreatedDate = DateTime.Now;

				_productCategoryService.Add(newProductCategory);
				_productCategoryService.Save();

				var responseData = _mapper.Map<ProductCategory, ProductCategoryViewModel>(newProductCategory);
				return CreatedAtAction(nameof(GetById), new { id = responseData.ID }, responseData);
			}
			catch (Exception ex)
			{
				return HandleException(ex);
			}
		}

		[HttpPut("update")]
		public IActionResult Update([FromBody] ProductCategoryViewModel model)
		{
			if (!ModelState.IsValid) { return BadRequest(ModelState); }

			try
			{
				var dbProductCategory = _productCategoryService.GetById(model.ID);
				if (dbProductCategory == null)
					return NotFound();

				if (model.ID == model.ParentID)
				{
					return BadRequest("Danh mục không thể là con chính nó");
				}

				dbProductCategory.UpdateProductCategory(model);
				dbProductCategory.UpdatedDate = DateTime.Now;

				_productCategoryService.Update(dbProductCategory);
				_productCategoryService.Save();

				var responseData = _mapper.Map<ProductCategory, ProductCategoryViewModel>(dbProductCategory);
				return Ok(responseData);
			}
			catch (Exception ex)
			{
				return HandleException(ex);
			}
		}

		[HttpDelete("delete/{id:int}")]
		[AllowAnonymous]
		public IActionResult Delete(int id)
		{
			try
			{
				var oldProductCategory = _productCategoryService.GetById(id);
				if (oldProductCategory == null) return NotFound();

				_productCategoryService.Delete(id);
				_productCategoryService.Save();

				var responseData = _mapper.Map<ProductCategory, ProductCategoryViewModel>(oldProductCategory);
				return Ok(responseData);
			}
			catch (Exception ex)
			{
				return HandleException(ex);
			}
		}

		[HttpDelete("deletemulti")]
		[AllowAnonymous]
		public IActionResult DeleteMulti(string checkedProductCategories)
		{
			if (string.IsNullOrWhiteSpace(checkedProductCategories))
				return BadRequest("Danh sách ID không được để trống.");

			try
			{
				//Deserialize JSON string thành danh sách các ID
				var listProductCategory = System.Text.Json.JsonSerializer.Deserialize<List<int>>(checkedProductCategories);

				if (listProductCategory == null || !listProductCategory.Any()) return BadRequest("Danh sách ID không hợp lệ hoặc rỗng.");

				foreach(var id in listProductCategory)
				{
					var category = _productCategoryService.GetById(id);
					if (category == null) return NotFound($"Không tìm thấy danh mục với ID {id} ");

					_productCategoryService.Delete(id);
				}
				_productCategoryService.Save();
				return Ok(new { Message = "Xóa thành công các danh mục.", DeletedCount = listProductCategory.Count });
			}
			catch (Exception ex)
			{
				return HandleException(ex);
			}
		}
		private List<ProductCategoryViewModel> GetCategoryViewModel(long? selectedParent = null)
		{
			List<ProductCategoryViewModel> items = new List<ProductCategoryViewModel>();
			var allCategories = _productCategoryService.GetAll();
			IEnumerable<ProductCategory> parentCategories = allCategories.Where(c => c.ParentID == null).ToList();

			foreach (var parent in parentCategories)
			{
				items.Add(new ProductCategoryViewModel
				{
					ID = parent.Id,
					Name = parent.Name,
					DisplayOrder = parent.DisplayOrder,
					Status = parent.Status,
					CreatedDate = parent.CreatedDate,
				});

				//now get all its children (separate Category in case you need recursion)
				GetSubTree(allCategories.ToList(), parent, items);
			}
			return items;
		}

		private void GetSubTree(IList<ProductCategory> allCats, ProductCategory parent, IList<ProductCategoryViewModel> items)
		{
			var subCategories = allCats.Where(c => c.ParentID == parent.Id);
			foreach (var category in subCategories)
			{
				//add this category
				items.Add(new ProductCategoryViewModel
				{
					ID = category.Id,
					Name = parent.Name + " >> " + category.Name,
					DisplayOrder = category.DisplayOrder,
					Status = category.Status,
					CreatedDate = category.CreatedDate,
				});

				//recursive call in case your have a hierarchy more than 1 level deep
				GetSubTree(allCats, category, items);
			}
		}
	}
}
