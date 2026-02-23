using KeduPayments.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KeduPayments.Application.DTOs
{

    /// <summary>
    /// Classe de transferência de dados (DTO) para a entidade Plano de Pagamento, utilizada para criar ou atualizar um plano de pagamento, incluindo as cobranças associadas.
    /// </summary>
    public class PlanoPagamentoRequest
    {
        /// <summary>
        /// Identificador do responsável financeiro associado ao plano de pagamento.
        /// </summary>
        [Required(ErrorMessage = "O campo Responsável Financeiro é obrigatório.")]
        public int ResponsavelId { get; set; }

        /// <summary>
        /// Identificador do centro de custo associado ao plano de pagamento.
        /// </summary>
        [Required(ErrorMessage = "O campo Centro de Custo é obrigatório.")]
        public int CentroCustoId { get; set; }

        /// <summary>
        /// Lista de cobranças associadas ao plano de pagamento.
        /// </summary>
        [Required(ErrorMessage = "O campo Cobranças é obrigatório.")]
        public ICollection<CobrancaRequest> Cobrancas { get; set; }
    }

    /// <summary>
    /// Classe de transferência de dados (DTO) para a entidade Plano de Pagamento, utilizada para retornar os dados de um plano de pagamento, incluindo o valor total calculado com base nas cobranças associadas.
    /// </summary>
    public class PlanoPagamentoResponse
    {
        /// <summary>
        /// Identificador único do plano de pagamento.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identificador do responsável financeiro associado ao plano de pagamento.
        /// </summary>
        public int ResponsavelId { get; set; }

        /// <summary>
        /// Identificador do centro de custo associado ao plano de pagamento.
        /// </summary>
        public int CentroCustoId { get; set; }

        /// <summary>
        /// Lista de cobranças associadas ao plano de pagamento.
        /// </summary>
        public ICollection<CobrancaResponse> Cobrancas { get; set; }

        /// <summary>
        /// Valor total do plano de pagamento, calculado com base nas cobranças associadas.
        /// </summary>
        public decimal ValorTotal { get; set; }
    }
}
