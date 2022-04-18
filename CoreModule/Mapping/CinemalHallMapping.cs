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
    public class CinemalHallMapping : IEntityTypeConfiguration<CinemaHall>
    {
        public void Configure(EntityTypeBuilder<CinemaHall> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property<string>(a => a.Name).IsRequired().HasMaxLength(100);
            builder.Property<string>(a => a.Description).IsRequired();
            builder.Property<string?>(a => a.Image).IsRequired(false).HasMaxLength(100);
            builder.HasMany(a => a.Movies).WithOne(a => a.CinemaHall).HasForeignKey(a => a.CinemaHallId);
            builder.ToTable("Cinema");

        }
    }
}
