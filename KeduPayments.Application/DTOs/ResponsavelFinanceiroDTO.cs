using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KeduPayments.Application.DTOs
{

    /// <summary>
    /// Classe de transferência de dados (DTO) para a entidade Responsável Financeiro, utilizada para criar ou atualizar um responsável financeiro associado a um plano de pagamento.
    /// </summary>
    public class ResponsavelFinanceiroRequest
    {
        /// <summary>
        /// Nome do responsável financeiro associado ao plano de pagamento.
        /// </summary>
        [Required(ErrorMessage = "O campo Nome do Responsável Financeiro é obrigatório.")]
        public string Nome { get; set; }
    }

    /// <summary>
    /// Classe de transferência de dados (DTO) para a entidade Responsável Financeiro, utilizada para retornar os dados de um responsável financeiro associado a um plano de pagamento.
    /// </summary>
    public class ResponsavelFinanceiroResponse
    {
        /// <summary>
        /// Identificador único do responsável financeiro.
        /// </summary>
        public int Id { get; set; }


        /// <summary>
        /// Nome do responsável financeiro associado ao plano de pagamento.
        /// </summary>
        public string Nome { get; set; }
    }
}
