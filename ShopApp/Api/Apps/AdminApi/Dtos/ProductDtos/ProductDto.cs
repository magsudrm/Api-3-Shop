using FluentValidation;

namespace Api.Apps.AdminApi.Dtos.ProductDtos
{
	public class ProductDto
	{
		public int BrandId { get; set; }
		public string Name { get; set; }
		public decimal CostPrice { get; set; }
		public decimal SalePrice { get; set; }
		public decimal DiscountPercent { get; set; }

		public class ProductDtoValidator : AbstractValidator<ProductDto>
		{
			public ProductDtoValidator()
			{
				RuleFor(x=>x.Name).NotEmpty().MaximumLength(30).MinimumLength(2);
				RuleFor(x => x.SalePrice).NotEmpty().GreaterThanOrEqualTo(0);
				RuleFor(x => x.CostPrice).NotEmpty().GreaterThanOrEqualTo(0);
				RuleFor(x => x.DiscountPercent).GreaterThanOrEqualTo(0).LessThanOrEqualTo(100);
				RuleFor(x=>x.CostPrice).LessThanOrEqualTo(x=>x.SalePrice);
				RuleFor(x => x).Custom((x, context) =>
				{
					if ((x.SalePrice * (100 - x.DiscountPercent) / 100) < x.CostPrice)
						context.AddFailure(nameof(ProductDto.DiscountPercent), "Discount percent must bu between 0 and 100");
				});
			}
		}
	}
}
