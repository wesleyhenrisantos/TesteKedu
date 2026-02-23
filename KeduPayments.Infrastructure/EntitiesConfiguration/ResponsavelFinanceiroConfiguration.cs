using System;
using System.Collections.Generic;
using System.Text;
using KeduPayments.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeduPayments.Infrastructure.EntitiesConfiguration
{
    public class ResponsavelFinanceiroConfiguration : IEntityTypeConfiguration<ResponsavelFinanceiro>
    {                                                 
        public void Configure(EntityTypeBuilder<ResponsavelFinanceiro> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Nome).IsRequired().HasMaxLength(100);
        }
    }
}
