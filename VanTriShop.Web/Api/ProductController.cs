using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VanTriShop.Service;
using VanTriShop.Web.Infrastructure.Core;
using VanTriShop.Model.Models;
using VanTriShop.Web.Models.Product;
using VanTriShop.Web.Models.Common;
using VanTriShop.Web.Infrastructure.Extensions;

namespace VanTriShop.Web.Api
{
	[Route("api/productCategory")]
	//[Authorize]
	[ApiController]
	public class ProductController : ApiControllerBase
	{
		private readonly IProductService _productService;
		private readonly IMapper _mapper;
		public ProductController(IErrorService errorService, IProductService productService, IMapper mapper) : base(errorService)
		{
			_productService = productService;
			_mapper = mapper;
		}

		[HttpGet("getallparents")]
		public IActionResult GetAllParents()
		{
			try
			{
				var model = _productService.GetAll();
				var responseData = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(model);
				return Ok(responseData);
			}
			catch (Exception ex)
			{
				return HandleException(ex);
			}
		}

		[HttpGet("gettags")]
		public IActionResult GetTags(string text)
		{
			try
			{
				var model = _productService.GetListProductTag(text);
				var responseData = _mapper.Map<IEnumerable<Tag>, IEnumerable<TagViewModel>>(model);
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
				var model = _productService.GetById(id);
				var responseData = _mapper.Map<Product, ProductViewModel>(model);
				return Ok(responseData);
			}
			catch (Exception ex)
			{
				return HandleException(ex);
			}
		}


		[HttpGet("getall")]
		public IActionResult GetAll(int? categoryId, string keyword, int page, int pageSize = 20)
		{
			try
			{
				int totalRow = 0;
				var model = _productService.GetAll(categoryId, keyword);

				totalRow = model.Count();
				var query = model.OrderByDescending(x => x.CreatedBy).Skip((page - 1) * pageSize).Take(pageSize).ToList();

				var responseData = _mapper.Map<List<Product>, List<ProductViewModel>>(query);
				var paginationSet = new PaginationSet<ProductViewModel>
				{
					Items = responseData,
					PageIndex = page,
					PageSize = pageSize,
					TotalRows = totalRow,
				};
				return Ok(paginationSet);
			}
			catch (Exception ex)
			{
				return HandleException(ex);
			}
		}

		[HttpPost("add")]
		public IActionResult Create([FromBody] ProductViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var newProduct = new Product();
				newProduct.UpdateProduct(model);
				newProduct.CreatedDate = DateTime.Now;
				newProduct.CreatedBy = User.Identity.Name;
				_productService.Add(newProduct);
				_productService.Save();

				var responseData = _mapper.Map<Product, ProductViewModel>(newProduct);
				return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, responseData);

			} catch (Exception ex) {
				return HandleException(ex);
			}
		}
		[HttpPut("update")]
		public IActionResult Update([FromBody] ProductViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var dbProduct = _productService.GetById(model.ID);

				dbProduct.UpdateProduct(model);
				dbProduct.CreatedDate = DateTime.Now;
				dbProduct.CreatedBy = User.Identity?.Name;

				_productService.Update(dbProduct);
				_productService.Save();

				var responseData = _mapper.Map<ProductViewModel>(dbProduct);
				return Ok(responseData);
			}
			catch (Exception ex)
			{
				return HandleException(ex);
			}
		}
		[HttpDelete("delete/{id:int}")]
		public IActionResult Delete(int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var deleteProduct = _productService.Delete(id);
				_productService.Save();

				var responseData = _mapper.Map<ProductViewModel>(deleteProduct);
				return Ok(responseData);
			}
			catch (Exception ex)
			{
				return HandleException(ex);
			}
		}

		[HttpDelete("deletemulti")]
		public IActionResult DeleteMulti([FromBody] List<int> checkedProducts)
		{
			try
			{
				foreach (var id in checkedProducts)
				{
					_productService.Delete(id);
				}
				_productService.Save();
				return Ok(new { Count = checkedProducts.Count });
			}
			catch (Exception ex)
			{
				return HandleException(ex);
			}
		}


		
	} 
}
