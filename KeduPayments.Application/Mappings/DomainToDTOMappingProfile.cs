using AutoMapper;
using KeduPayments.Application.DTOs;
using KeduPayments.Domain.Entities;

namespace KeduPayments.Application.Mappings
{
    /// <summary>
    /// Classe de perfil de mapeamento do AutoMapper para mapear as entidades de domínio para os DTOs e vice-versa.
    /// </summary>
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Cobranca, CobrancaRequest>().ReverseMap();
            CreateMap<Cobranca, CobrancaResponse>().ReverseMap();
            CreateMap<CentroCusto, CentroCustoRequest>().ReverseMap();
            CreateMap<CentroCusto, CentroCustoResponse>().ReverseMap();
            CreateMap<Pagamento, PagamentoRequest>().ReverseMap();
            CreateMap<Pagamento, PagamentoResponse>().ReverseMap();
            CreateMap<PlanoPagamento, PlanoPagamentoRequest>().ReverseMap();
            CreateMap<PlanoPagamento, PlanoPagamentoResponse>().ReverseMap();
            CreateMap<ResponsavelFinanceiro, ResponsavelFinanceiroRequest>().ReverseMap();
            CreateMap<ResponsavelFinanceiro, ResponsavelFinanceiroResponse>().ReverseMap();
        }
    }
}
