
using CoreModule.Mapping;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.DbContextConfig
{
    public class MyDbContext:IdentityDbContext
    {
        private readonly IConfiguration _configuration;

        public MyDbContext(DbContextOptions<MyDbContext> options,IConfiguration configuration):base(options)
        {
            _configuration = configuration;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ActorMapping());
            builder.ApplyConfiguration(new ProducerMapping());
            builder.ApplyConfiguration(new MovieCategoryMapping());
            builder.ApplyConfiguration(new CinemalHallMapping());
            builder.ApplyConfiguration(new ActorMovieMapping());
            builder.ApplyConfiguration(new ApplicationUserMapping());
            builder.ApplyConfiguration(new CartMapping());
            builder.ApplyConfiguration(new ShippingAddressMapping());
            builder.ApplyConfiguration(new OrderMapping());
            builder.ApplyConfiguration(new OrderItemMapping());
            builder.ApplyConfiguration(new EmailTemplateMapping());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            var conString = _configuration.GetConnectionString("DefaultConnection");
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies().UseSqlServer(conString, a => a.MigrationsAssembly("ETicketing"));
            }
        }
    }
}
