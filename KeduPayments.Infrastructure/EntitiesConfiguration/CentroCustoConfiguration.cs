using KeduPayments.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeduPayments.Infrastructure.EntitiesConfiguration
{
    public class CentroCustoConfiguration : IEntityTypeConfiguration<CentroCusto>
    {
        public void Configure(EntityTypeBuilder<CentroCusto> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Natureza).HasMaxLength(100).IsRequired();;

            builder.HasData(
              new CentroCusto(1, "MATRICULA"),
              new CentroCusto(2, "MENSALIDADE"),
              new CentroCusto(3, "MATERIAL")
            );
        }
    }
}
