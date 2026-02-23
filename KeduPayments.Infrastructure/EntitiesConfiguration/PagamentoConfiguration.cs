using KeduPayments.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeduPayments.Infrastructure.EntitiesConfiguration
{
    public class PagamentoConfiguration : IEntityTypeConfiguration<Pagamento>
    {
        public void Configure(EntityTypeBuilder<Pagamento> builder)
        {
            builder.ToTable("pagamentos");

            builder.HasKey(t => t.Id);            
            builder.Property(p => p.DataPagamento).IsRequired();

            builder.HasOne(p => p.Cobranca)
                .WithMany(c => c.Pagamentos)
                .HasForeignKey(p => p.CobrancaId);

        }
    }
}
