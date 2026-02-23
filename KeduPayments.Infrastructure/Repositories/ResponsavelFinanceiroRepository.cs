using KeduPayments.Domain.Entities;
using KeduPayments.Domain.Interfaces;
using KeduPayments.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeduPayments.Infrastructure.Repositories
{
    /// <summary>
    /// Repositório para a entidade Responsável Financeiro, implementando os métodos definidos na interface IResponsavelFinanceiroRepository.
    /// </summary>
    public class ResponsavelFinanceiroRepository : IResponsavelFinanceiroRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Método construtor para criar uma nova instância do repositório de Responsável Financeiro.
        /// </summary>
        /// <param name="context">Contexto do banco de dados a ser utilizado pelo repositório.</param>
        public ResponsavelFinanceiroRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Método para adicionar um novo responsável financeiro de forma assíncrona.
        /// </summary>
        /// <param name="responsavelFinanceiro">O objeto que representa o responsável financeiro a ser adicionado. Não pode ser nulo.</param>
        /// <returns>O responsável financeiro adicionado.</returns>
        /// <exception cref="InvalidOperationException">Lançada quando os dados fornecidos são inválidos.</exception>
        public async Task<ResponsavelFinanceiro> AdicionarResponsavelFinanceiro(ResponsavelFinanceiro responsavelFinanceiro)
        {
            if (_context is not null && responsavelFinanceiro is not null &&
                      _context.ResponsaveisFinanceiros is not null)
            {
                _context.ResponsaveisFinanceiros.Add(responsavelFinanceiro);
                await _context.SaveChangesAsync();
                return responsavelFinanceiro;
            }
            else
            {
                throw new InvalidOperationException("Dados inválidos.");
            }
        }

        /// <summary>
        /// Método para atualizar um responsável financeiro existente de forma assíncrona.
        /// </summary>
        /// <param name="responsavelFinanceiro">O objeto que representa o responsável financeiro a ser atualizado. Não pode ser nulo.</param>
        /// <returns>Uma tarefa assíncrona que representa a operação de atualização do responsável financeiro.</returns>
        /// <exception cref="ArgumentNullException">Lançada quando os dados fornecidos são inválidos.</exception>
        public async Task AtualizarResponsavelFinanceiro(ResponsavelFinanceiro responsavelFinanceiro)
        {
            if (responsavelFinanceiro is not null)
            {
                _context.Entry(responsavelFinanceiro).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentNullException("Dados inválidos...");
            }
        }

        /// <summary>
        /// Método para deletar um responsável financeiro
        /// </summary>
        /// <param name="responsavelFinanceiro">O objeto que representa o responsável financeiro a ser deletado. Não pode ser nulo.</param>
        /// <returns>Uma tarefa assíncrona que representa a operação de exclusão do responsável financeiro.</returns>
        /// <exception cref="InvalidOperationException">Lançada quando os dados fornecidos são inválidos.</exception>
        public async Task DeletarResponsavelFinanceiro(ResponsavelFinanceiro responsavelFinanceiro)
        {
            if (responsavelFinanceiro is not null)
            {
                _context.ResponsaveisFinanceiros.Remove(responsavelFinanceiro);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Dados inválidos...");
            }
        }

        /// <summary>
        /// Obtém o responsável financeiro correspondente ao identificador especificado.
        /// </summary>
        /// <param name="id">O identificador exclusivo do responsável financeiro a ser recuperado.</param>
        /// <returns>Um objeto ResponsavelFinanceiro correspondente ao identificador fornecido, caso encontrado.</returns>
        /// <exception cref="InvalidOperationException">Lançada quando nenhum responsável financeiro com o identificador especificado é encontrado.</exception>
        public async Task<ResponsavelFinanceiro?> ObterResponsavelFinanceiro(int id)
        {
            var responsavelFinanceiro = await _context.ResponsaveisFinanceiros.FirstOrDefaultAsync(l =>
                                                                 l.Id == id);
            if (responsavelFinanceiro is null)
                throw new InvalidOperationException($"Responsável financeiro com ID {id} " +
                    $"não encontrado");
            return responsavelFinanceiro;
        }

        /// <summary>
        /// Método para obter todos os responsáveis financeiros de forma assíncrona.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ResponsavelFinanceiro>> ObterResponsaveisFinanceiros()
        {
            if (_context is not null && _context.ResponsaveisFinanceiros is not null)
            {
                var responsaveis = await _context.ResponsaveisFinanceiros.ToListAsync();
                return responsaveis;
            }
            else
            {
                return new List<ResponsavelFinanceiro>();
            }
        }
    }
}