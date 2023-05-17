using Api.Apps.AdminApi.Dtos.BrandDtos;
using Api.Apps.AdminApi.Dtos.ProductDtos;
using Api.Apps.ClientApi.Dtos.ProductDtos;
using AutoMapper;
using Core.Entities;

namespace Api.Profiles
{
	public class MapProfile:Profile
	{
		public MapProfile()
		{
			CreateMap<Brand, BrandGetAllItemDto>();
			CreateMap<Brand,BrandGetDto>()/*.ForMember(d=>d.ProductsCount,s=>s.MapFrom(x=>x.Products.Count))*/;
			CreateMap<BrandDto, Brand>();
			CreateMap<Product, Apps.ClientApi.Dtos.ProductDtos.ProductGetAllItemDto>()
				.ForMember(d=>d.DiscountedPrice,s=>s.MapFrom(x=>x.SalePrice*(100-x.DiscountPercent)/100)) ;
			CreateMap<Product, Apps.ClientApi.Dtos.ProductDtos.ProductGetDto>();
			CreateMap<Brand, Apps.ClientApi.Dtos.ProductDtos.BrandInProductGetDto>() ;
			CreateMap<Product, Apps.ClientApi.Dtos.ProductDtos.ProductGetDto>()
				.ForMember(d => d.DiscountedPrice, s => s.MapFrom(x => x.SalePrice * (100 - x.DiscountPercent) / 100));
			CreateMap<Apps.AdminApi.Dtos.ProductDtos.ProductDto, Product>();
		}
	}
}
