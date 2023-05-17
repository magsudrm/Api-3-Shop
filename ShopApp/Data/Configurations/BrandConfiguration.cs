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
	public class BrandConfiguration : IEntityTypeConfiguration<Brand>
	{
		public void Configure(EntityTypeBuilder<Brand> builder)
		{
			builder.Property(x=>x.Name).IsRequired(true).HasMaxLength(30);
		}
	}
}
