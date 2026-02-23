using KeduPayments.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KeduPayments.Infrastructure.Context;

/// <summary>
/// Classe de contexto do Entity Framework para a aplicação de pagamentos, responsável por gerenciar as entidades e suas relações com o banco de dados.
/// </summary>
public class ApplicationDbContext : DbContext
{
    /// <summary>
    /// Método construtor para criar uma nova instância do contexto do banco de dados, utilizando as opções de configuração fornecidas.
    /// </summary>
    /// <param name="options">Opções de configuração do contexto do banco de dados.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    public DbSet<CentroCusto> CentroCustos { get; set; }
    public DbSet<Cobranca> Cobrancas { get; set; }
    public DbSet<Pagamento> Pagamentos { get; set; }
    public DbSet<PlanoPagamento> PlanoPagamentos { get; set; } 
    public DbSet<ResponsavelFinanceiro> ResponsaveisFinanceiros { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext)
            .Assembly);
    }
}
