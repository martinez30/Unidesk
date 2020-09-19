using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Infra.Repository.EntityConfiguration
{
    public class LightPoleEntityTypeConfiguration : BaseEntityTypeConfiguration<LightPole>
    {
        public override void Configure(EntityTypeBuilder<LightPole> b)
        {
            base.Configure(b);

            b.HasOne(x => x.Localization);

            b.Property(x => x.Number)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired();

            b.Property(x => x.SerialNumber)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired();

            b.Property(x => x.Active)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired();

            b.HasIndex(x => x.SerialNumber).IsUnique();

        }
    }
}
