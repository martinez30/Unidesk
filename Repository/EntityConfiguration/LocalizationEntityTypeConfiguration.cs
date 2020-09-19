using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Repository.EntityConfiguration
{
    class LocalizationEntityTypeConfiguration : BaseEntityTypeConfiguration<Localization>
    {
        public override void Configure(EntityTypeBuilder<Localization> b)
        {
            base.Configure(b);

            b.Property(x => x.Address)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired()
                .HasColumnType("varchar(100)");

            b.Property(x => x.Neighborhood)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired()
                .HasColumnType("varchar(100)");

            b.Property(x => x.City)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired()
                .HasColumnType("varchar(100)");

            b.Property(x => x.State)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired();

        }
    }
}
