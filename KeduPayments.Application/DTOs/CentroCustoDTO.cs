using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KeduPayments.Application.DTOs
{

    /// <summary>
    /// Classe de Transferência de Dados (DTO) para a entidade de Centro de Custo, utilizada para criar ou atualizar um centro de custo.
    /// </summary>
    public class CentroCustoRequest
    {
        /// <summary>
        /// Natureza do Centro de Custo.
        ///<remark>O campo pode ser MATRICULA, MENSALIDADE, MATERIAL ou outros CC customizáveis</remark>
        /// </summary>
        [Required(ErrorMessage = "Informe a natureza do centro de custo")]
        [MinLength(3)]
        [MaxLength(100)]
        public string Natureza { get; set; }
    }

    /// <summary>
    /// Classe de Transferência de Dados (DTO) para a entidade de Centro de Custo, utilizada para retornar os dados de um centro de custo.
    /// </summary>
    public class CentroCustoResponse
    {
        /// <summary>
        /// Identificador único da cobrança.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Natureza do Centro de Custo.
        /// </summary>    
        public string Natureza { get; set; }
    }
}
