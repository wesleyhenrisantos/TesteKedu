namespace KeduPayments.Application.Abstractions;

public interface IPaymentCodeGenerator
{
    string Generate(string metodo);
}
