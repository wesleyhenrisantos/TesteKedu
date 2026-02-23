using KeduPayments.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeduPayments.Infrastructure.EntitiesConfiguration
{
    public class CobrancaConfiguration : IEntityTypeConfiguration<Cobranca>
    {
        public void Configure(EntityTypeBuilder<Cobranca> builder)
        {
            builder.ToTable("cobrancas");
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Valor).HasPrecision(18, 2).IsRequired();
            builder.Property(p => p.DataVencimento).IsRequired();
            builder.Property(p => p.MetodoPagamento).IsRequired();
            builder.Property(p => p.Status).IsRequired();
            builder.Property(p => p.CodigoPagamento).IsRequired();

            builder.HasMany(x => x.Pagamentos)
                .WithOne()
                .HasForeignKey(x => x.CobrancaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
