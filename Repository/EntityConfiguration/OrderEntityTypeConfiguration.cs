using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Repository.EntityConfiguration
{
    public class OrderEntityTypeConfiguration : BaseEntityTypeConfiguration<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> b)
        {
            base.Configure(b);

            b.Property(b => b.InitialDate)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired();

            b.Property(b => b.FinishDate)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired(false);

            b.Property(x => x.Name)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnType("varchar(50)")
                .IsRequired(false);

            b.HasOne(x=> x.LightPole);

            b.Property(x => x.RequestDescription)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired(false)
                .HasColumnType("varchar(255)");

            b.Property(x => x.Problem)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired();

            b.Property(x => x.ResponseDescription)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnType("varchar(255)")
                .IsRequired(false);

            b.HasMany(x => x.Proviced_Services);

            b.Property(x => x.Status)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired();
        }
    }
}
