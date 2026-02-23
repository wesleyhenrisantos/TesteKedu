using Catalogo.Domain.Entities;
using KeduPayments.Domain.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace KeduPayments.Domain.Entities
{
    /// <summary>
    /// Entidade de Pagamento.
    /// </summary>
    public sealed class Pagamento : Entity
    {
        /// <summary>
        /// Método Construtor.
        /// </summary>
        public Pagamento() { }
        /// <summary>
        /// Método Construtor para criar uma nova instância de Pagamento, garantindo que os valores fornecidos sejam válidos e associando o pagamento à cobrança correspondente.
        /// </summary>
        /// <param name="cobrancaId">ID da cobrança associada ao pagamento.</param>
        /// <param name="dataPagamentoUtc">Data em que o pagamento foi realizado.</param>
        public Pagamento(int cobrancaId, DateTime dataPagamentoUtc)
        {
            CobrancaId = cobrancaId;
            DataPagamento = dataPagamentoUtc;
        }

        /// <summary>
        /// Identificador da cobrança associada ao pagamento.   
        /// </summary>
        public int CobrancaId { get; set; }

        /// <summary>
        /// Propriedade de navegação para a cobrança associada ao pagamento.
        /// </summary>
        public Cobranca Cobranca { get; set; }

        /// <summary>
        /// Data em que o pagamento foi realizado.
        /// </summary>
        public DateTime DataPagamento { get; set; }
    }
}
