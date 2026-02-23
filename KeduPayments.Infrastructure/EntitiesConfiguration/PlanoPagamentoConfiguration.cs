using KeduPayments.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeduPayments.Infrastructure.EntitiesConfiguration
{
    public class PlanoPagamentoConfiguration : IEntityTypeConfiguration<PlanoPagamento>
    {
        public void Configure(EntityTypeBuilder<PlanoPagamento> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.CentroCustoId).IsRequired();
            builder.Property(p => p.ResponsavelId).IsRequired();

            builder.HasOne<ResponsavelFinanceiro>()
                .WithMany()
                .HasForeignKey(x => x.ResponsavelId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<CentroCusto>()
                .WithMany()
                .HasForeignKey(x => x.CentroCustoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Cobrancas)
                .WithOne()
                .HasForeignKey(x => x.PlanoPagamentoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
