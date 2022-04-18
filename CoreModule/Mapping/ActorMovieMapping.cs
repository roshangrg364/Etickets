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
    public class ActorMovieMapping : IEntityTypeConfiguration<ActorMovie>
    {
        public void Configure(EntityTypeBuilder<ActorMovie> builder)
        {
            builder.HasKey(a => new { a.MovieId, a.ActorId });
            builder.HasOne(a => a.Movie).WithMany(a=>a.ActorMovies).HasForeignKey(a => a.MovieId);
            builder.HasOne(a => a.Actor).WithMany(a=>a.ActorMovies).HasForeignKey(a => a.ActorId);
            builder.ToTable("Actor_Movie");
        }
    }
}
