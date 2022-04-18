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
    public class MovieCategoryMapping : IEntityTypeConfiguration<MovieCategory>
    {
        public void Configure(EntityTypeBuilder<MovieCategory> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property<string>(a => a.Name).IsRequired().HasMaxLength(100);
            builder.HasIndex(a => a.Name).IsUnique();
            builder.HasMany(a => a.Movies).WithOne(a => a.Category).HasForeignKey(a => a.MovieCategoryId);
            builder.ToTable("Category");

        }
    }
}
