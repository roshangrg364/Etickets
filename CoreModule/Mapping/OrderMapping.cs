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
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property<decimal>(a=>a.Amount).IsRequired();
            builder.Property<DateTime>(a => a.CreatedOn).IsRequired();
            builder.Property<string>(a => a.Status).IsRequired().HasMaxLength(100);
            builder.HasOne(a => a.ShippingAddress).WithMany().HasForeignKey(a => a.ShippingAddressId);
            builder.HasOne(a => a.User).WithMany().HasForeignKey(a => a.UserId);
            builder.HasMany(a => a.OrderItems).WithOne(a=>a.Order).HasForeignKey(a => a.OrderId);
            builder.ToTable("Order");
        }
    }
}
