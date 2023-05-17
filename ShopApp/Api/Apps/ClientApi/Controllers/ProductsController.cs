using Api.Apps.ClientApi.Dtos.ProductDtos;
using AutoMapper;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Apps.ClientApi.Controllers
{
	[ApiExplorerSettings(GroupName = "user_v1")]
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly ShopDbContext _context;
		private readonly IMapper _mapper;

		public ProductsController( ShopDbContext context,IMapper mapper)
		{
			_context= context;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			var data = _mapper.Map<List<ProductGetAllItemDto>>(_context.Products.Include(x=>x.Brand).ToList());
			return Ok(data);
		}

		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			var data = _context.Products.Include(x => x.Brand).FirstOrDefault(x => x.Id == id);
			if(data==null)
				return NotFound();

			ProductGetDto dto = _mapper.Map<ProductGetDto>(data);

			return Ok(dto);
		} 

	}
}
