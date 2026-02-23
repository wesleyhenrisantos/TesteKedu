using KeduPayments.Application.Abstractions;
using KeduPayments.Application.Services;
using KeduPayments.Domain.Common;
using KeduPayments.Domain.Entities;
using KeduPayments.Domain.Interfaces;
using KeduPayments.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Text;

namespace KeduPayments.Infrastructure.Repositories
{
    /// <summary>
    /// Repositório para a entidade Financeiro, implementando os métodos definidos na interface IFinanceiroRepository.
    /// </summary>
    public class FinanceiroRepository : IFinanceiroRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IPaymentCodeGenerator _codeGenerator;

        /// <summary>
        /// Método construtor para criar uma nova instância do repositório de Plano de Pagamento.
        /// </summary>
        /// <param name="context">Contexto do banco de dados a ser utilizado pelo repositório.</param>
        public FinanceiroRepository(ApplicationDbContext context, IPaymentCodeGenerator codeGenerator)
        {
            _context = context;
            _codeGenerator = codeGenerator;
        }

        /// <summary>
        /// Método para adicionar um novo pagamento associado a uma cobrança existente.
        /// </summary>
        /// <param name="pagamento">Objeto Pagamento a ser adicionado.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Pagamento> AdicionarPagamentoPorCobranca(Pagamento pagamento)
        {
            if (_context is not null && pagamento is not null &&
                                 _context.Pagamentos is not null)
            {

                var cobranca = await ObterCobranca(pagamento.CobrancaId);
                if (cobranca.Status == "PAGA") throw new InvalidOperationException("Cobrança já está paga.");

                _context.Pagamentos.Add(pagamento);
                await _context.SaveChangesAsync();
                return pagamento;
            }
            else
            {
                throw new InvalidOperationException("Dados inválidos.");
            }
        }

        /// <summary>
        /// Método para atualizar o status de uma cobrança existente, permitindo a modificação do status para refletir mudanças no processo de pagamento ou na situação da cobrança.
        /// </summary>
        /// <param name="id">ID da cobrança a ser atualizada.</param>
        /// <param name="status">Novo status a ser atribuído à cobrança.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task <Cobranca> AtualizarStatusCobranca(int id, string? status, decimal? valor)
        {
            var cobranca = await ObterCobranca(id);


            if (_context is not null && cobranca is not null &&
                                        _context.Cobrancas is not null)
            {
                if (cobranca.Status == "CANCELADA") throw new InvalidOperationException("Não é possível pagar uma cobrança cancelada.");
                if (cobranca.Status == "PAGA") throw new InvalidOperationException("Cobrança já está paga.");

                if (valor.HasValue && cobranca.Valor != valor.Value && status == "PAGA")
                {
                    var proximaParcela = _context.Cobrancas.Where(c => c.PlanoPagamentoId == cobranca.PlanoPagamentoId && c.Id != cobranca.Id && c.Status =="EMITIDA")
                        .OrderBy(c => c.DataVencimento)
                        .FirstOrDefault();

                    if (proximaParcela is not null)
                    {
                        decimal valorParcial = proximaParcela.Valor - (cobranca.Valor - valor.Value);
                        if (valorParcial > 0)
                            proximaParcela.Valor = proximaParcela.Valor + valorParcial;
                        else
                            proximaParcela.Valor = 0;


                        _context.Cobrancas.Update(proximaParcela);
                    }

                }

                cobranca.Valor = valor.HasValue ? valor.Value : cobranca.Valor;
                status = "PAGA";
                if (valor.HasValue && valor.Value < cobranca.Valor)
                    status = "PARCIAL";

                cobranca.Status = status; 
                _context.Cobrancas.Update(cobranca);


                await _context.SaveChangesAsync();
                return cobranca;
            }
            else
            {
                throw new InvalidOperationException("Dados inválidos.");
            }
        }


        /// <summary>
        /// Método para obter uma cobrança específica.
        /// </summary>
        /// <param name="id">ID da cobrança a ser obtida.</param>
        /// <returns>A cobrança correspondente ao ID fornecido.</returns>
        /// <exception cref="InvalidOperationException">Lançada quando a cobrança não é encontrada.</exception>
        public async Task<Cobranca?> ObterCobranca(int id)
        {
            var cobranca = await _context.Cobrancas.FirstOrDefaultAsync(l =>
                                                                 l.Id == id);
            if (cobranca is null)
                throw new InvalidOperationException($"Cobrança com ID {id} " +
                    $"não encontrada");
            return cobranca;
        }

        /// <summary>
        /// Método para listar cobranças associadas a um responsável específico.
        /// </summary>
        /// <param name="responsavelId">ID do responsável cujas cobranças serão listadas.</param>
        /// <param name="status">Status das cobranças a serem filtradas.</param>
        /// <param name="metodoPagamento">Método de pagamento das cobranças a serem filtradas.</param>
        /// <param name="vencidasSomente">Indica se apenas cobranças vencidas devem ser retornadas.</param>
        /// <param name="vencimentoDe">Data de início do intervalo de vencimento das cobranças.</param>
        /// <param name="vencimentoAte">Data de término do intervalo de vencimento das cobranças.</param>
        /// <returns>Uma coleção de cobranças que atendem aos critérios especificados.</returns>
        public async Task<ICollection<Cobranca>> ObterCobrancasPorResponsavel(int responsavelId, string? status, int? metodoPagamento, bool? vencidasSomente, DateTime? vencimentoDe, DateTime? vencimentoAte)
        {
            if (_context is not null && _context.Cobrancas is not null && _context.PlanoPagamentos is not null
                && _context.Pagamentos is not null)
            {

                var query =
                   from c in _context.Cobrancas.Include(x => x.Pagamentos)
                   join p in _context.PlanoPagamentos on c.PlanoPagamentoId equals p.Id
                   where p.ResponsavelId == responsavelId
                   select c;

                if (!string.IsNullOrEmpty(status))
                    query = query.Where(c => c.Status == status);

                if (metodoPagamento.HasValue)
                    query = query.Where(c => (int)c.MetodoPagamento == metodoPagamento.Value);

                if (vencimentoDe.HasValue)
                    query = query.Where(c => c.DataVencimento >= vencimentoDe.Value);

                if (vencimentoAte.HasValue)
                    query = query.Where(c => c.DataVencimento <= vencimentoAte.Value);

                if (vencidasSomente == true)
                {
                    var hoje = DateTime.UtcNow;
                    query = query.Where(c =>
                        c.Status != "EMITIDA" && c.Status != "PAGA" && c.DataVencimento.Date < hoje.Date);
                }

                return await query
                    .OrderByDescending(c => c.DataVencimento)
                    .ToListAsync();
            }
            else
            {
                return new List<Cobranca>();
            }
        }

        #region Plano de Pagamento

        /// <summary>
        /// Método para adicionar um novo plano de pagamento de forma assíncrona.
        /// </summary>
        /// <param name="planoPagamento">O objeto que representa o plano de pagamento a ser adicionado. Não pode ser nulo.</param>
        /// <returns>O plano de pagamento adicionado.</returns>
        /// <exception cref="InvalidOperationException">Lançada quando os dados fornecidos são inválidos.</exception>
        public async Task<PlanoPagamento> AdicionarPlanoPagamento(PlanoPagamento planoPagamento)
        {
            if (_context is not null && planoPagamento is not null &&
                      _context.PlanoPagamentos is not null)
            {

                List<Cobranca> cobrancas = new List<Cobranca>();
                foreach (var item in planoPagamento.Cobrancas)
                {
                    var codigo = _codeGenerator.Generate(item.MetodoPagamento.ToString());
                    var cobranca = new Cobranca(planoPagamento.Id, item.Valor, item.DataVencimento, item.MetodoPagamento, codigo);
                    cobrancas.Add(cobranca);
                }
                planoPagamento.AddCobranca(cobrancas);
                _context.PlanoPagamentos.Add(planoPagamento);

                await _context.SaveChangesAsync();
                return planoPagamento;
            }
            else
            {
                throw new InvalidOperationException("Dados inválidos.");
            }
        }



        /// <summary>
        /// Obtém o plano de pagamento correspondente ao identificador especificado.
        /// </summary>
        /// <param name="id">O identificador exclusivo do plano de pagamento a ser recuperado.</param>
        /// <returns>Um objeto PlanoPagamento correspondente ao identificador fornecido, caso encontrado.</returns>
        /// <exception cref="InvalidOperationException">Lançada quando nenhum plano de pagamento com o identificador especificado é encontrado.</exception>
        public async Task<PlanoPagamento?> ObterPlanoPagamento(int id)
        {
            var planoPagamento = await _context.PlanoPagamentos.Include(p => p.Cobrancas).Include(c => c.CentrosCusto).FirstOrDefaultAsync(l =>
                                                                 l.Id == id);
            if (planoPagamento is null)
                throw new InvalidOperationException($"Plano de pagamento com ID {id} " +
                    $"não encontrado");
            return planoPagamento;
        }


        /// <summary>
        /// Método para obter todos os planos de pagamento de forma assíncrona.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<PlanoPagamento>> ObterPlanosPagamento()
        {
            if (_context is not null && _context.PlanoPagamentos is not null)
            {
                var planos = await _context.PlanoPagamentos.Include(p => p.Cobrancas).Include(c => c.CentrosCusto).ToListAsync();
                return planos;
            }
            else
            {
                return new List<PlanoPagamento>();
            }
        }

        public async Task<IEnumerable<PlanoPagamento>> ObterPlanosPagamentoPorResponsavel(int responsavelId)
        {
            if (_context is not null && _context.PlanoPagamentos is not null)
            {
                var planos = await _context.PlanoPagamentos.Where(p => p.ResponsavelId == responsavelId).ToListAsync();
                return planos;
            }
            else
            {
                return new List<PlanoPagamento>();
            }
        }

        #endregion
    }
}