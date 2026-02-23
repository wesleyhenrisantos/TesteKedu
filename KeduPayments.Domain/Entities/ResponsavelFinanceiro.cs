using Catalogo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeduPayments.Domain.Entities
{
    /// <summary>
    /// Classe de Entetidade para o Responsável Financeiro.
    /// </summary>
    public sealed class ResponsavelFinanceiro : Entity
    {
        /// <summary>
        /// Método construtor para criar um novo Responsável Financeiro.
        /// </summary>
        /// <param name="nome">Nome do responsável financeiro.</param>
        public ResponsavelFinanceiro(string nome)
        {
            Nome = nome;
        }

        /// <summary>
        /// Nome do Responsável Financeiro.
        /// </summary>
        public string Nome { get;  set; }
      
    }
}
