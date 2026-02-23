using KeduPayments.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeduPayments.Domain.Interfaces
{
    // Interface para o repositório de Centro de Custo, definindo os métodos para operações CRUD.
    public interface ICentroCustoRepository
    {                                                       

        /// <summary>
        /// Método para adicionar um novo Centro de Custo. Recebe um objeto CentroCusto e retorna o objeto criado com seu ID gerado.
        /// </summary>
        /// <param name="centroCusto">Objeto Centro de Custo a ser adicionado.</param>
        /// <returns></returns>
        Task<CentroCusto> AdicionarCentroCusto(CentroCusto centroCusto);


        /// <summary>
        /// Método para obter todos os Centros de Custo. Retorna uma coleção de objetos CentroCusto.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CentroCusto>> ObterCentroCustos();
    }
}
