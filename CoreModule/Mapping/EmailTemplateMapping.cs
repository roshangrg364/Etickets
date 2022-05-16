using EmailModule.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Mapping
{
   
    public class EmailTemplateMapping : IEntityTypeConfiguration<EmailTemplate>
    {
        public void Configure(EntityTypeBuilder<EmailTemplate> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property<int>(a => a.Id).HasMaxLength(20).IsRequired();
            builder.Property<string>(a => a.Template).IsRequired();
            builder.Property<string>(a => a.Type).IsRequired();
            // builder.Ignore(a => a.TemplateVariables);
            builder.ToTable("template");
        }

      
    }
}
