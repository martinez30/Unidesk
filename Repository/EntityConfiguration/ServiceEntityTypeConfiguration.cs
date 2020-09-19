using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Repository.EntityConfiguration
{
    public class ServiceEntityTypeConfiguration : BaseEntityTypeConfiguration<Service>
    {
        public override void Configure(EntityTypeBuilder<Service> b)
        {
            base.Configure(b);

            b.HasOne(x => x.Order);

            b.Property(x => x.Description)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnType("varchar(100)")
                .IsRequired();
        }
    }
}
