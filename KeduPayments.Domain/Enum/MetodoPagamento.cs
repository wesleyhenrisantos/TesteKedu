using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace KeduPayments.Domain.Enum
{
    /// <summary>
    /// Enumeração para os métodos de pagamento disponíveis.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MetodoPagamento
    {
        /// <summary>
        /// Método de pagamento via Pix.
        /// </summary>
        [Description("Pix")]
        [DefaultValue("PIX")]
        [EnumMember(Value = "PIX")]
        Pix = 1,
        /// <summary>
        /// Método de pagamento via Boleto.
        /// </summary>
        [Description("Boleto")]  
        [DefaultValue("BOLETO")]
        [EnumMember(Value = "PIX")]
        Boleto = 2
    }
}
