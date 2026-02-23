using AutoMapper;
using KeduPayments.Application.DTOs;
using KeduPayments.Application.Interfaces;
using KeduPayments.Domain.Entities;
using KeduPayments.Domain.Interfaces;

namespace KeduPayments.Application.Services
{
    /// <summary>
    /// Serviço para a entidade Centro de Custo, implementando os métodos definidos na interface ICentroCustoService.
    /// </summary>
    public class CentroCustoService   : ICentroCustoService
    {
        private readonly ICentroCustoRepository _centroCustoRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Método construtor para criar uma nova instância do serviço de Centro de Custo.
        /// </summary>
        /// <param name="centroCustoRepository">Repositório de Centro de Custo a ser utilizado pelo serviço.</param>
        /// <param name="mapper">Objeto IMapper para mapeamento de entidades.</param>
        public CentroCustoService(ICentroCustoRepository centroCustoRepository, IMapper mapper)
        {
            _centroCustoRepository = centroCustoRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Tarefa para adicionar um novo Centro de Custo, recebendo um DTO como parâmetro, mapeando-o para a entidade e utilizando o repositório para persistir os dados.
        /// </summary>
        /// <param name="centroCustoDTO">Objeto DTO contendo as informações do Centro de Custo a ser adicionado.</param>
        /// <returns></returns>
        public async Task Add(CentroCustoRequest centroCustoDTO)
        {
            var centroCustoEntity = _mapper.Map<CentroCusto>(centroCustoDTO);
            await _centroCustoRepository.AdicionarCentroCusto(centroCustoEntity);
        }

        /// <summary>
        /// Tarefa para obter todos os Centros de Custo, utilizando o repositório para buscar todas as entidades correspondentes e mapeando-as para uma coleção de DTOs antes de retornar os dados.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CentroCustoResponse>> GetCentroCusto()
        {
            try
            {
                var centroCustosEntity = await _centroCustoRepository.ObterCentroCustos();
                var centroCustoDTOs = _mapper.Map<IEnumerable<CentroCustoResponse>>(centroCustosEntity);
                return centroCustoDTOs;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
