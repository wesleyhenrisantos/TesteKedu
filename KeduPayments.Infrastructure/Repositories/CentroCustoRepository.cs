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
    /// Repositório para a entidade Centro de Custo, implementando os métodos definidos na interface ICentroCustoRepository.
    /// </summary>
    public class CentroCustoRepository   : ICentroCustoRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Método construtor para criar uma nova instância do repositório de Centro de Custo.
        /// </summary>
        /// <param name="context">Contexto do banco de dados a ser utilizado pelo repositório.</param>
        public CentroCustoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Método para adicionar um novo centro de custo de forma assíncrona.
        /// </summary>
        /// <param name="centroCusto">O objeto que representa o centro de custo a ser adicionado. Não pode ser nulo.</param>
        /// <returns>O centro de custo adicionado.</returns>
        /// <exception cref="InvalidOperationException">Lançada quando os dados fornecidos são inválidos.</exception>
        public async Task<CentroCusto> AdicionarCentroCusto(CentroCusto centroCusto)
        {
            if (_context is not null && centroCusto is not null &&
                      _context.CentroCustos is not null)
            {
                _context.CentroCustos.Add(centroCusto);
                await _context.SaveChangesAsync();
                return centroCusto;
            }
            else
            {
                throw new InvalidOperationException("Dados inválidos.");
            }
        }


        /// <summary>
        /// Método para obter todos os centros de custo de forma assíncrona.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CentroCusto>> ObterCentroCustos()
        {
            if (_context is not null && _context.CentroCustos is not null)
            {
                var centroCustos = await _context.CentroCustos.ToListAsync();
                return centroCustos;
            }
            else
            {
                return new List<CentroCusto>();
            }
        }
    }
}
