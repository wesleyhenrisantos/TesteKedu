using Catalogo.Domain.Entities;
using KeduPayments.Domain.Common;
using KeduPayments.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace KeduPayments.Domain.Entities
{
    /// <summary>
    /// Entidade de Cobrança.
    /// </summary>
    public sealed class Cobranca : Entity
    {
        /// <summary>
        /// Método Construtor.
        /// </summary>
        public Cobranca() { }

        /// <summary>
        /// Método Construtor para criar uma nova instância de Cobrança, garantindo que os valores fornecidos sejam válidos e inicializando o status da cobrança como "EMITIDA".
        /// </summary>
        /// <param name="planoPagamentoId">ID do plano de pagamento associado à cobrança.</param>
        /// <param name="valor">Valor da cobrança.</param>
        /// <param name="dataVencimento">Data de vencimento da cobrança.</param>
        /// <param name="metodoPagamento">Método de pagamento utilizado para a cobrança.</param>
        /// <param name="codigoPagamento">Código de pagamento da cobrança.</param>
        /// <exception cref="DomainException"></exception>
        public Cobranca(int planoPagamentoId, decimal valor, DateTime dataVencimento, MetodoPagamento metodoPagamento ,string codigoPagamento)
        {
            if (valor <= 0) throw new DomainException("Valor da cobrança deve ser maior que zero.");
            if (string.IsNullOrWhiteSpace(codigoPagamento)) throw new DomainException("Código de pagamento é obrigatório.");

            PlanoPagamentoId = planoPagamentoId;
            Valor = valor;
            DataVencimento = dataVencimento.Kind == DateTimeKind.Unspecified
                ? DateTime.SpecifyKind(dataVencimento, DateTimeKind.Utc)
                : dataVencimento.ToUniversalTime();
            MetodoPagamento = metodoPagamento;
            CodigoPagamento = codigoPagamento.Trim();
            Status = "EMITIDA";
        }

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
        // <remark>O campo pode conter o código de pagamento gerado para a cobrança, como o código do Pix ou o número do boleto.</remark>
        /// </summary>
        public string CodigoPagamento { get; set; }

        /// <summary>
        /// Coleção de pagamentos associados à cobrança.
        /// </summary>
        public ICollection<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();


        /// <summary>
        /// Chave estrangeira para o plano de pagamento associado à cobrança.
        /// </summary>
        public int PlanoPagamentoId { get; set; }
                                    

        public void RegistrarPagamento(decimal valor, DateTime dataPagamentoUtc)
        {
            if (Status == "CANCELADA") throw new DomainException("Não é possível pagar uma cobrança cancelada.");
            if (Status == "PAGA") throw new DomainException("Cobrança já está paga.");
            if (valor != Valor) throw new DomainException("Pagamento deve ser do valor total da cobrança.");

            Pagamentos.Add(new Pagamento(Id, dataPagamentoUtc));
            Status = "PAGA";
        }
  
    }
}
