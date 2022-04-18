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
    public class CartMapping : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property<decimal>(a => a.Rate).IsRequired().HasPrecision(18, 2);
            builder.Property<int>(a => a.Quantity).IsRequired().HasMaxLength(10);
            builder.Property<DateTime>(a => a.AddedOn).IsRequired();
            builder.HasOne(a => a.Movie).WithMany().HasForeignKey(a => a.MovieId);
            builder.HasOne(a => a.User).WithMany().HasForeignKey(a => a.UserId);
            builder.ToTable("Cart");

        }
    }
}
