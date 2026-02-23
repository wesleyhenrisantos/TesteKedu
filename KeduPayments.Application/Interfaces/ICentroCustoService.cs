using KeduPayments.Application.DTOs;

namespace KeduPayments.Application.Interfaces
{
    // Interface para o serviço de Centro de Custo, definindo os métodos para operações CRUD.
    public interface ICentroCustoService
    {

        /// <summary>
        /// Método para adicionar um novo Centro de Custo. Recebe um objeto CentroCusto e retorna o objeto criado com seu ID gerado.
        /// </summary>
        /// <param name="centroCusto">Objeto Centro de Custo a ser adicionado.</param>
        /// <returns></returns>
        Task Add(CentroCustoRequest centroCustoRequest);

        /// <summary>
        /// Método para obter todos os Centros de Custo. Retorna uma coleção de objetos CentroCusto.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CentroCustoResponse>> GetCentroCusto();
    }
}
