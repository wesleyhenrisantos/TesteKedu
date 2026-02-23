using AutoMapper;
using KeduPayments.Application.DTOs;
using KeduPayments.Application.Interfaces;
using KeduPayments.Domain.Entities;
using KeduPayments.Domain.Enum;
using KeduPayments.Domain.Interfaces;
using System.Net.NetworkInformation;

namespace KeduPayments.Application.Services
{
    /// <summary>
    /// Serviço para a entidade Financeiro, implementando os métodos definidos na interface IFinanceiroService.
    /// </summary>
    public class FinanceiroService : IFinanceiroService
    {
        private readonly IFinanceiroRepository _financeiroRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Método construtor para criar uma nova instância do serviço de Cobrança.
        /// </summary>
        /// <param name="financeiroRepository">Repositório financeiro a ser utilizado pelo serviço.</param>
        /// <param name="mapper">Objeto IMapper para mapeamento de entidades.</param>
        public FinanceiroService(IFinanceiroRepository financeiroRepository, IMapper mapper)
        {
            _financeiroRepository = financeiroRepository;
            _mapper = mapper;
        }

        #region Cobrança
        /// <summary>
        /// Tarefa assíncrona para adicionar um novo pagamento relacionado a uma cobrança específica.
        /// </summary>
        /// <param name="pagamentoDTO">Objeto PagamentoDTO a ser adicionado.</param>
        /// <returns>Objeto PagamentoDTO adicionado.</returns>
        public async Task<PagamentoResponse> AddPagamentoPorCobranca(PagamentoRequest pagamentoDTO)
        {
            var planoPagamentoEntity = _mapper.Map<Pagamento>(pagamentoDTO);
            await _financeiroRepository.AdicionarPagamentoPorCobranca(planoPagamentoEntity);
            return _mapper.Map<PagamentoResponse>(planoPagamentoEntity);
        }

        /// <summary>
        /// Tarefa assíncrona para atualizar o status de uma cobrança específica.
        /// </summary>
        /// <param name="id">ID da cobrança a ser atualizada.</param>
        /// <param name="status">Novo status da cobrança.</param>
        /// <returns>Tarefa assíncrona representando a operação de atualização do status da cobrança.</returns>
        public async Task UpdateStatusCobranca(int id, string? status, decimal? valor)
        {
             await _financeiroRepository.AtualizarStatusCobranca(id, status, valor);

        }

        /// <summary>
        /// Tarefa assíncrona para obter uma coleção de cobranças relacionadas a um responsável específico.
        /// </summary>
        /// <param name="responsavelId">ID do responsável pelas cobranças.</param>
        /// <param name="status">Status das cobranças a serem filtradas.</param>
        /// <param name="metodoPagamento">Método de pagamento das cobranças a serem filtradas.</param>
        /// <param name="vencidasSomente">Indica se devem ser retornadas apenas cobranças vencidas.</param>
        /// <param name="vencimentoDe">Data de início do intervalo de vencimento das cobranças.</param>
        /// <param name="vencimentoAte">Data de término do intervalo de vencimento das cobranças.</param>
        /// <returns>Uma coleção de objetos CobrancaDTO representando as cobranças filtradas.</returns>
        public async Task<ICollection<CobrancaResponse>> GetCobrancaByResponsavel(int responsavelId, string? status, int? metodoPagamento, bool? vencidasSomente, DateTime? vencimentoDe, DateTime? vencimentoAte)
        {
            var cobrancaEntity = await _financeiroRepository.ObterCobrancasPorResponsavel(responsavelId, status, metodoPagamento, vencidasSomente, vencimentoDe, vencimentoAte );
            
            return cobrancaEntity.Select(c => new CobrancaResponse(
                c.Id,  c.Valor, c.DataVencimento, c.MetodoPagamento,
                c.Status.ToString(), c.CodigoPagamento
            )).ToList();
        }
        #endregion

        #region PlanoPagamento

        /// <summary>
        /// Tarefa assíncrona para adicionar um novo plano de pagamento.
        /// </summary>
        /// <param name="planoPagamentoDTO">Objeto PlanoPagamentoDTO a ser adicionado.</param>
        /// <returns>Objeto PlanoPagamentoDTO adicionado.</returns>
        public async Task<PlanoPagamentoResponse> AddPlanoPagamento(PlanoPagamentoRequest planoPagamentoDTO)
        {
            var planoPagamentoEntity = _mapper.Map<PlanoPagamento>(planoPagamentoDTO);
            await _financeiroRepository.AdicionarPlanoPagamento(planoPagamentoEntity);
            return _mapper.Map<PlanoPagamentoResponse>(planoPagamentoEntity);
        }

        /// <summary>
        /// Tarefa assíncrona para obter um plano de pagamento específico pelo seu ID.
        /// </summary>
        /// <param name="id">ID do plano de pagamento a ser obtido.</param>
        /// <returns>O plano de pagamento correspondente ao ID fornecido.</returns>
        public async Task<PlanoPagamentoResponse> GetById(int id)
        {
            var planoPagamentoEntity = await _financeiroRepository.ObterPlanoPagamento(id);
            return _mapper.Map<PlanoPagamentoResponse>(planoPagamentoEntity);
        }

        /// <summary>
        /// Tarefa assíncrona para obter uma coleção de planos de pagamento relacionados a um responsável específico.
        /// </summary>
        /// <param name="responsavelId">ID do responsável cujos planos de pagamento serão obtidos.</param>
        /// <returns>Uma coleção de objetos PlanoPagamentoDTO representando os planos de pagamento do responsável.</returns>
        public async Task<IEnumerable<PlanoPagamentoResponse>> GetPlanosPagamentoByResponsavel(int responsavelId)
        {
            var planoPagamentoEntity = await _financeiroRepository.ObterPlanosPagamentoPorResponsavel(responsavelId);
            return _mapper.Map<ICollection<PlanoPagamentoResponse>>(planoPagamentoEntity);
        }
        #endregion
    }
}