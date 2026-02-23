using Catalogo.Domain.Entities;
using KeduPayments.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeduPayments.Domain.Entities
{
    /// <summary>
    /// Entidade de Plano de Pagamento.
    /// </summary>
    public sealed class PlanoPagamento : Entity
    {
        /// <summary>
        /// Método construtor para criar um novo Plano de Pagamento.
        /// </summary>
        public PlanoPagamento() 
        {
            Cobrancas = new List<Cobranca>();
        }

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
        public ICollection<Cobranca> Cobrancas { get; set; }

        /// <summary>
        /// Lista de centros de custo associados ao plano de pagamento.
        /// </summary>
        public ICollection<CentroCusto> CentrosCusto { get; set; }


        public void AddCobranca(List<Cobranca> cobrancas)
        {
            Cobrancas = new List<Cobranca>();
            foreach (var cobranca in cobrancas)
            {
                if (cobranca.PlanoPagamentoId != Id) throw new DomainException("Cobrança não pertence a este plano.");
                Cobrancas.Add(cobranca);
            }
        }
    }
}
