using Core.Entities;

namespace Api.Apps.ClientApi.Dtos.ProductDtos
{
	public class ProductGetDto
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public decimal SalePrice { get; set; }
		public decimal DiscountPercent { get; set; }
		public decimal DiscountedPrice { get; set; }
		public BrandInProductGetDto Brand { get; set; }

	}

	public class BrandInProductGetDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}

}
