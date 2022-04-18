using CoreModule.Source.BaseTypes;
using CoreModule.Source.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Mapping
{
    public class ShippingAddressMapping : IEntityTypeConfiguration<ShippingAddress>
    {
        public void Configure(EntityTypeBuilder<ShippingAddress> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property<string>(a => a.FullName).IsRequired().HasMaxLength(100);
            builder.Property<string>(a => a.Address).IsRequired().HasMaxLength(100);
            builder.Property<string>(a => a.ZipCode).IsRequired().HasMaxLength(100);
            builder.Property(a => a.PhoneNumber).IsRequired().HasMaxLength(10).HasConversion(a=>a.ToString(),a=>new MobileNumber(a));
            builder.ToTable("ShippingAddress");
        }
    }
}
