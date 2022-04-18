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
    public class ActorMapping : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property<string>(a => a.FullName).IsRequired().HasMaxLength(100);
            builder.Property<string>(a => a.Description).IsRequired();
            builder.Property<string?>(a => a.Image).IsRequired(false).HasMaxLength(100);
            builder.HasMany(a => a.ActorMovies).WithOne(a => a.Actor).HasForeignKey(a => a.ActorId);
            builder.ToTable("Actor");
        }
    }
}
