using KeduPayments.Application.DTOs;
using KeduPayments.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeduPayments.Application.Interfaces
{
    // Interface para o serviço financeiro, definindo os métodos para operações CRUD.
    public interface IFinanceiroService
    {

        #region Cobrança

        /// <summary>
        /// Método para adicionar um novo pagamento associado a uma cobrança existente.
        /// </summary>
        /// <param name="pagamento">Objeto Pagamento a ser adicionado.</param>
        /// <returns></returns>
        Task<PagamentoResponse> AddPagamentoPorCobranca(PagamentoRequest pagamento);

        /// <summary>
        /// Método responsável por atualizar o status de uma cobrança específica, permitindo a alteração do status para refletir o progresso ou a situação atual da cobrança.
        /// </summary>
        /// <param name="id">ID da cobrança a ser atualizada.</param>
        /// <param name="status">Novo status da cobrança.</param>
        /// <param name="valor">Valor do pagamento.</param> 
        /// <returns></returns>
        Task UpdateStatusCobranca(int id, string? status, decimal? valor);

        /// <summary>
        /// Método para obter todas as cobranças de um responsável específico.
        /// </summary>
        /// <param name="responsavelId">ID do responsável cujas cobranças serão obtidas.</param>
        /// <param name="status">Status das cobranças a serem obtidas (opcional).</param>
        /// <param name="metodoPagamento">Método de pagamento das cobranças a serem obtidas (opcional).</param>
        /// <param name="vencidasSomente">Indica se devem ser obtidas apenas cobranças vencidas (opcional).</param>
        /// <param name="vencimentoDe">Data de início do período de vencimento das cobranças a serem obtidas (opcional).</param>
        /// <param name="vencimentoAte">Data de término do período de vencimento das cobranças a serem obtidas (opcional).</param>
        /// <returns>Uma coleção de cobranças associadas ao responsável.</returns>
        Task<ICollection<CobrancaResponse>>GetCobrancaByResponsavel(int responsavelId, string? status, int? metodoPagamento,
            bool? vencidasSomente, DateTime? vencimentoDe, DateTime? vencimentoAte);

        #endregion

        #region Plano de Pagamento

        /// <summary>
        /// Método para adicionar um novo Plano de Pagamento ao repositório.
        /// </summary>
        /// <param name="planoPagamento">Objeto Plano de Pagamento a ser adicionado.</param>
        /// <returns></returns>
        Task<PlanoPagamentoResponse> AddPlanoPagamento(PlanoPagamentoRequest planoPagamento);


        /// <summary>
        /// Método para obter um plano de pagamento específico pelo seu ID.
        /// </summary>
        /// <param name="id">ID do plano de pagamento a ser obtido.</param>
        /// <returns>O plano de pagamento correspondente ao ID fornecido.</returns>
        Task<PlanoPagamentoResponse> GetById(int id);

        /// <summary>
        /// Método para obter os planos de pagamento associados a um responsável específico.
        /// </summary>
        /// <param name="responsavelId">ID do responsável cujos planos de pagamento serão obtidos.</param>
        /// <returns>Uma coleção de planos de pagamento associados ao responsável.</returns>
        Task<IEnumerable<PlanoPagamentoResponse>> GetPlanosPagamentoByResponsavel(int responsavelId);

        #endregion
    }
}
