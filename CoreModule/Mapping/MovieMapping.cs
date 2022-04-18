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
    public class MovieMapping : IEntityTypeConfiguration<Movie>

    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property<string>(a => a.Name).IsRequired().HasMaxLength(100);
            builder.Property<string>(a => a.Description).IsRequired();
            builder.Property<string>(a => a.Image).IsRequired().HasMaxLength(100);
            builder.Property(a => a.TicketPrice).IsRequired().HasColumnType("decimal(18,2");
            builder.Property<DateTime>(a => a.StartDate).IsRequired();
            builder.Property<DateTime>(a => a.EndDate).IsRequired();
            builder.HasOne(a => a.Category).WithMany(a => a.Movies).HasForeignKey(a => a.MovieCategoryId);
            builder.HasOne(a => a.Producer).WithMany().HasForeignKey(a => a.ProducerId);
            builder.HasMany(a => a.ActorMovies).WithOne(a => a.Movie).HasForeignKey(a => a.MovieId);
            builder.Navigation(a => a.ActorMovies).UsePropertyAccessMode(PropertyAccessMode.Property);
            builder.ToTable("Movie");
        }
    }
}
