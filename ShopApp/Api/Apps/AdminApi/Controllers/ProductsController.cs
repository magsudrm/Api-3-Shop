using Api.Apps.AdminApi.Dtos.ProductDtos;
using AutoMapper;
using Core.Entities;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Apps.AdminApi.Controllers
{
	[ApiExplorerSettings(GroupName = "admin_v1")]
	[Route("admin/api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly ShopDbContext _context;
		private readonly IMapper _mapper;

		public ProductsController(ShopDbContext context,IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			var data = _context.Products.ToList();
			return Ok(data);
		}

		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			var data = _context.Products.Find(id);
			if (data == null)
			{
				return NotFound();
			}
			return Ok(data);
		}
		[HttpPost("")]
		public IActionResult Create(ProductDto dto)
		{
			if (_context.Products.Any(x => x.Id == dto.BrandId))
			{
				ModelState.AddModelError("BrandId", "BrandId not correct");
				return BadRequest(ModelState);
			}

			Product product = _mapper.Map<Product>(dto);
			_context.Products.Add(product);
			_context.SaveChanges();
			return Ok(product);
		}

		[HttpPut("{id}")]

		public IActionResult Edit(int id,ProductDto dto)
		{
			var existData = _context.Products.Find(id);
			if(existData==null)
				return NotFound();

			if(existData.BrandId!=dto.BrandId && _context.Brands.Any(x=>x.Id==dto.BrandId))
			{
				ModelState.AddModelError("BrandId", "BrandId not correct");
				return BadRequest(ModelState);
			}
			existData.Name = dto.Name;
			existData.CostPrice = dto.CostPrice;
			existData.SalePrice = dto.SalePrice;
			existData.DiscountPercent = dto.DiscountPercent;
			existData.BrandId=dto.BrandId;

			_context.SaveChanges();
			return NoContent();
		}

	}
}
