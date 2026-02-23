using KeduPayments.Domain.Entities;
using KeduPayments.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KeduPayments.Application.DTOs
{

    /// <summary>
    /// Classe de transferência de dados (DTO) para a entidade Cobrança, utilizada para criar ou atualizar uma cobrança.
    /// </summary>
    public class CobrancaRequest
    {
        /// <summary>
        /// Valor da cobrança.
        /// </summary>
        [Required(ErrorMessage = "Informe o valor da cobrança")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; }
        /// <summary>
        /// Data de vencimento da cobrança.
        /// </summary>
        [Required(ErrorMessage = "Informe a data de vencimento da cobrança")]
        public DateTime DataVencimento { get; set; }
        /// <summary>
        /// Método de pagamento utilizado para a cobrança.
        /// </summary>
        [Required(ErrorMessage = "Informe o método de pagamento da cobrança")]
        public MetodoPagamento MetodoPagamento { get; set; }

    }

    /// <summary>
    /// Classe de transferência de dados (DTO) para a entidade Cobrança, utilizada para retornar os dados de uma cobrança.
    /// </summary>
    public class CobrancaResponse
    {
        /// <summary>
        /// Método Construtor para criar uma nova instância de Cobrança, garantindo que os valores fornecidos sejam válidos e inicializando o status da cobrança como "EMITIDA".
        /// </summary>        
        /// <param name="valor">Valor da cobrança.</param>
        /// <param name="dataVencimento">Data de vencimento da cobrança.</param>
        /// <param name="metodoPagamento">Método de pagamento utilizado para a cobrança.</param>
        /// <param name="codigoPagamento">Código de pagamento da cobrança.</param>
        public CobrancaResponse(int id, decimal valor, DateTime dataVencimento, MetodoPagamento metodoPagamento, string status, string codigoPagamento)
        {
            Id = id;
            Valor = valor;
            DataVencimento = dataVencimento.Kind == DateTimeKind.Unspecified
                ? DateTime.SpecifyKind(dataVencimento, DateTimeKind.Utc)
                : dataVencimento.ToUniversalTime();
            MetodoPagamento = metodoPagamento;
            CodigoPagamento = codigoPagamento.Trim();
            Status = EstaVencida(DateOnly.FromDateTime(DateTime.UtcNow)) ? "VENCIDA" : status;
        }
        /// <summary>
        /// Identificador único da cobrança.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Valor da cobrança.
        /// </summary>
        public decimal Valor { get; set; }

        /// <summary>
        /// Data de vencimento da cobrança.
        /// </summary>        
        public DateTime DataVencimento { get; set; }

        /// <summary>
        /// Método de pagamento utilizado para a cobrança.
        /// </summary>
        public MetodoPagamento MetodoPagamento { get; set; }

        /// <summary>
        /// Status da cobrança.
        /// </summary>  
        public string Status { get; set; }

        /// <summary>
        /// Código de pagamento da cobrança.        
        /// </summary>        
        public string CodigoPagamento { get; set; }


        public bool EstaVencida(DateOnly hoje) =>
           Status is not ("PAGA" or "CANCELADA") && hoje > DateOnly.FromDateTime(DataVencimento);
    }
}
