using Catalogo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeduPayments.Domain.Entities
{
    /// <summary>
    /// Entidade de Centro de Custo.    
    /// </summary>
    public sealed class CentroCusto : Entity
    {

        /// <summary>
        /// Método construtor para criar um novo Centro de Custo.
        /// </summary>
        public CentroCusto() 
        {
        }

        /// <summary>
        /// Método construtor para criar um novo Centro de Custo com os parâmetros necessários.
        /// </summary>
        /// <param name="id">O identificador exclusivo do Centro de Custo.</param>
        /// <param name="natureza">A natureza do Centro de Custo.</param>
        public CentroCusto(int id, string natureza)
        {
            Id = id;
            Natureza = natureza;
        }

        /// <summary>
        /// Natureza do Centro de Custo.
        ///<remark>O campo pode ser MATRICULA, MENSALIDADE, MATERIAL ou outros CC customizáveis</remark>
        /// </summary>
        public string Natureza { get; set; }
    }
}
