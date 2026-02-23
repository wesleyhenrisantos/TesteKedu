using AutoMapper;
using KeduPayments.Application.DTOs;
using KeduPayments.Application.Interfaces;
using KeduPayments.Domain.Entities;
using KeduPayments.Domain.Interfaces;

namespace KeduPayments.Application.Services
{
    /// <summary>
    /// Serviço para a entidade ResponsavelFinanceiro, implementando os métodos definidos na interface IResponsavelFinanceiroService.
    /// </summary>
    public class ResponsavelFinanceiroService : IResponsavelFinanceiroService
    {
        private readonly IResponsavelFinanceiroRepository _responsavelFinanceiroRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Método construtor para criar uma nova instância do serviço de ResponsavelFinanceiro.
        /// </summary>
        /// <param name="responsavelFinanceiroRepository">Repositório de ResponsavelFinanceiro a ser utilizado pelo serviço.</param>
        /// <param name="mapper">Objeto IMapper para mapeamento de entidades.</param>
        public ResponsavelFinanceiroService(IResponsavelFinanceiroRepository responsavelFinanceiroRepository, IMapper mapper)
        {
            _responsavelFinanceiroRepository = responsavelFinanceiroRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Tarefa para adicionar um novo ResponsavelFinanceiro, recebendo um DTO como parâmetro, mapeando-o para a entidade e utilizando o repositório para persistir os dados.
        /// </summary>
        /// <param name="responsavelFinanceiroDTO">Objeto DTO contendo as informações do ResponsavelFinanceiro a ser adicionado.</param>
        /// <returns></returns>
        public async Task<ResponsavelFinanceiroResponse> Add(ResponsavelFinanceiroRequest responsavelFinanceiroDTO)
        {
            var responsavelFinanceiroEntity = _mapper.Map<ResponsavelFinanceiro>(responsavelFinanceiroDTO);
            await _responsavelFinanceiroRepository.AdicionarResponsavelFinanceiro(responsavelFinanceiroEntity);   
            return _mapper.Map<ResponsavelFinanceiroResponse>(responsavelFinanceiroEntity);
        }

        /// <summary>
        /// Tarefa para atualizar um ResponsavelFinanceiro existente, recebendo um DTO como parâmetro, mapeando-o para a entidade e utilizando o repositório para persistir as alterações.
        /// </summary>
        /// <param name="responsavelFinanceiroDTO">Objeto DTO contendo as informações do ResponsavelFinanceiro a ser atualizado.</param>
        /// <returns></returns>
        public async Task Update(ResponsavelFinanceiroRequest responsavelFinanceiroDTO)
        {
            var responsavelFinanceiroEntity = _mapper.Map<ResponsavelFinanceiro>(responsavelFinanceiroDTO);
            await _responsavelFinanceiroRepository.AtualizarResponsavelFinanceiro(responsavelFinanceiroEntity);
        }

        /// <summary>
        /// Tarefa para deletar um ResponsavelFinanceiro existente, recebendo o ID do ResponsavelFinanceiro a ser deletado, obtendo a entidade correspondente e utilizando o repositório para remover os dados.
        /// </summary>
        /// <param name="id">ID do ResponsavelFinanceiro a ser deletado.</param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            var responsavelFinanceiroEntity = _responsavelFinanceiroRepository.ObterResponsavelFinanceiro(id).Result;
            await _responsavelFinanceiroRepository.DeletarResponsavelFinanceiro(responsavelFinanceiroEntity);
        }

        /// <summary>
        /// Tarefa para obter um ResponsavelFinanceiro por ID, recebendo o ID do ResponsavelFinanceiro a ser obtido, utilizando o repositório para buscar a entidade correspondente e mapeando-a para um DTO antes de retornar os dados.
        /// </summary>
        /// <param name="id">ID do ResponsavelFinanceiro a ser obtido.</param>
        /// <returns>Objeto DTO contendo as informações do ResponsavelFinanceiro obtido.</returns>
        public async Task<ResponsavelFinanceiroResponse> GetById(int id)
        {
            var responsavelFinanceiroEntity = await _responsavelFinanceiroRepository.ObterResponsavelFinanceiro(id);
            return _mapper.Map<ResponsavelFinanceiroResponse>(responsavelFinanceiroEntity);
        }

        /// <summary>
        /// Tarefa para obter todos os ResponsaveisFinanceiros, utilizando o repositório para buscar todas as entidades correspondentes e mapeando-as para uma coleção de DTOs antes de retornar os dados.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ResponsavelFinanceiroResponse>> GetResponsaveisFinanceiros()
        {
            try
            {
                var responsaveisFinanceirosEntity = await _responsavelFinanceiroRepository.ObterResponsaveisFinanceiros();
                var responsaveisFinanceirosDTOs = _mapper.Map<IEnumerable<ResponsavelFinanceiroResponse>>(responsaveisFinanceirosEntity);
                return responsaveisFinanceirosDTOs;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}