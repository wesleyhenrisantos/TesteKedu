using KeduPayments.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace KeduPayments.Application.DTOs
{
    /// <summary>
    /// Classe de transferência de dados (DTO) para a entidade Pagamento, utilizada para criar um novo pagamento, incluindo o identificador da cobrança associada e a data do pagamento.
    /// </summary>
    public class PagamentoRequest
    {

        /// <summary>
        /// Identificador da cobrança associada ao pagamento.   
        /// </summary>
        [Required(ErrorMessage = "Informe o identificador da cobrança associada ao pagamento")]
        public int CobrancaId { get; set; }


        /// <summary>
        /// Data em que o pagamento foi realizado.
        /// </summary>
        [Required(ErrorMessage = "Informe a data do pagamento")]
        public DateTime DataPagamento { get; set; }
    }

    /// <summary>
    /// Classe de transferência de dados (DTO) para a entidade Pagamento, utilizada para retornar os dados de um pagamento, incluindo o identificador único do pagamento, o identificador da cobrança associada e a data do pagamento.
    /// </summary>
    public class PagamentoResponse
    {
        /// <summary>
        /// Identificador único do pagamento.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador da cobrança associada ao pagamento.   
        /// </summary>
        public int CobrancaId { get; set; }
        /// <summary>
        /// Data em que o pagamento foi realizado.
        /// </summary>
        public DateTime DataPagamento { get; set; }
    }
}
