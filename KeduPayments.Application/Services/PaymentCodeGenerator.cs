using KeduPayments.Application.Abstractions;

namespace KeduPayments.Application.Services;

public sealed class PaymentCodeGenerator : IPaymentCodeGenerator
{
    public string Generate(string metodo)
    {
        var stamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
        var rand = Random.Shared.Next(100000, 999999);

        return metodo switch
        {
            "Boleto" => $"BOL-{stamp}-{rand}",
            "Pix" => $"PIX-{stamp}-{rand}",
            _ => throw new ArgumentOutOfRangeException(nameof(metodo))
        };
    }
}
