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
    public class OrderItemMapping : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property<decimal>(a => a.Rate).IsRequired();
            builder.Property<int>(a => a.Quantity).IsRequired();
            builder.Property<decimal>(a => a.Amount).IsRequired();
            builder.HasOne(a => a.Order).WithMany(a => a.OrderItems).HasForeignKey(a => a.OrderId);
            builder.HasOne(a => a.Movie).WithMany().HasForeignKey(a => a.MovieId);
            builder.ToTable("OrderItem");


        }
    }
}
