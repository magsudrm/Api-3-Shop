using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.Property(x => x.Name).IsRequired(true).HasMaxLength(40);
			builder.Property(x => x.SalePrice).HasColumnType("decimal(18,2)");
			builder.Property(x => x.CostPrice).HasColumnType("decimal(18,2)");
			builder.Property(x => x.DiscountPercent).HasColumnType("decimal(18,2)");
		}
	}
}
