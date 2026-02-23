using KeduPayments.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeduPayments.Domain.Interfaces
{
    /// <summary>
    /// Interface para o repositório de Responsável Financeiro, definindo os métodos para operações CRUD.
    /// </summary>
    public interface IResponsavelFinanceiroRepository
    {

        /// <summary>
        /// Método para adicionar um novo Responsável Financeiro ao repositório.
        /// </summary>
        /// <param name="responsavelFinanceiro">Objeto Responsável Financeiro a ser adicionado.</param>
        /// <returns></returns>
        Task<ResponsavelFinanceiro> AdicionarResponsavelFinanceiro(ResponsavelFinanceiro responsavelFinanceiro);
        /// <summary>
        /// Método para atualizar um Responsável Financeiro existente no repositório.
        /// </summary>
        /// <param name="responsavelFinanceiro">Objeto Responsável Financeiro com as informações atualizadas.</param>
        /// <returns></returns> 
        Task AtualizarResponsavelFinanceiro(ResponsavelFinanceiro responsavelFinanceiro);

        /// <summary>
        /// Método para deletar um Responsável Financeiro do repositório.
        /// </summary>
        /// <param name="planoPagamento">Objeto Plano de Pagamento utilizado como referência para deletar o Responsável Financeiro.</param>
        /// <returns></returns>
        Task DeletarResponsavelFinanceiro(ResponsavelFinanceiro responsavelFinanceiro);        

        /// <summary>
        /// Método para obter um Responsável Financeiro pelo seu ID.
        /// </summary>
        /// <param name="id">ID do Responsável Financeiro a ser obtido.</param>
        /// <returns></returns>
        Task<ResponsavelFinanceiro?> ObterResponsavelFinanceiro(int id);

        /// <summary>
        /// Método para obter todos os Responsáveis Financeiros do repositório.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ResponsavelFinanceiro>> ObterResponsaveisFinanceiros();
    }
}
