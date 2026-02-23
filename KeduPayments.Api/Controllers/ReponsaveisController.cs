using KeduPayments.Application.DTOs;
using KeduPayments.Application.Interfaces;
using KeduPayments.Domain.Enum;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KeduPayments.Controllers
{
    /// <summary>
    /// Controler para gerenciar os responsáveis financeiros.
    /// </summary>
    [Route("responsaveis")]
    [ApiController]
    public class ReponsaveisController : ControllerBase
    {
        private IResponsavelFinanceiroService _responsavelFinanceiroService;
        private IFinanceiroService _financeiroService;

        /// <summary>
        /// Método Construtor do Controlador de Responsáveis Financeiros.
        /// </summary>
        /// <param name="responsavelFinanceiroService">Serviço responsável por gerenciar as operações de responsáveis financeiros.</param>
        /// <param name="financeiroService">Serviço responsável por gerenciar as operações financeiras.</param>
        public ReponsaveisController(IResponsavelFinanceiroService responsavelFinanceiroService, IFinanceiroService financeiroService)
        {
            _responsavelFinanceiroService = responsavelFinanceiroService;
            _financeiroService = financeiroService;
        }

        /// <summary>
        /// Endpoint para criar um novo responsável financeiro.
        /// </summary>
        /// <param name="request">Detalhes do responsável financeiro a ser criado.</param>
        /// <returns>Retorna o responsável financeiro criado.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ResponsavelFinanceiroResponse>> Create([FromBody] ResponsavelFinanceiroRequest request)
        {
            try
            {
                var result = await _responsavelFinanceiroService.Add(request);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Endpoint para obter um responsável financeiro por ID.
        /// </summary>
        /// <param name="id">ID do responsável financeiro a ser obtido.</param>
        /// <returns>Retorna o responsável financeiro correspondente ao ID fornecido.</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ResponsavelFinanceiroResponse>> GetById(int id)
        {
            try
            {
                var result = await _responsavelFinanceiroService.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Endpoint para obter as cobranças associadas a um responsável financeiro específico, permitindo filtrar os resultados com base em parâmetros.
        /// </summary>
        /// <param name="responsavelId">ID do responsável cujas cobranças serão obtidas.</param>
        /// <param name="status">Status das cobranças a serem obtidas (opcional).</param>
        /// <param name="metodoPagamento">Método de pagamento das cobranças a serem obtidas (opcional).</param>
        /// <param name="vencidasSomente">Indica se devem ser obtidas apenas cobranças vencidas (opcional).</param>
        /// <param name="vencimentoDe">Data de início do período de vencimento das cobranças a serem obtidas (opcional).</param>
        /// <param name="vencimentoAte">Data de término do período de vencimento das cobranças a serem obtidas (opcional).</param>
        /// <returns></returns>

        [HttpGet]
        [Route("responsaveis/{responsavelId:int}/cobrancas")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CobrancaResponse>> GetCobrancas(int responsavelId, [FromQuery] string? status, [FromQuery] int? metodoPagamento,
        [FromQuery] bool? vencidasSomente, [FromQuery] DateTime? vencimentoDe, [FromQuery] DateTime? vencimentoAte)
        {
            try
            {
                var list = await _financeiroService.GetCobrancaByResponsavel(
                    responsavelId, status, metodoPagamento, vencidasSomente, vencimentoDe, vencimentoAte);
                return Ok(list);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Edpoint para contar a quantidade de cobranças associadas a um responsável financeiro específico.
        /// </summary>
        /// <param name="responsavelId">ID do responsável cujas cobranças serão contadas.</param>
        /// <returns>Retorna a quantidade de cobranças associadas ao responsável financeiro.</returns>
        [HttpGet]
        [Route("responsaveis/{responsavelId:int}/quantidade")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CobrancaResponse>> CountCobrancas(int responsavelId)
        {
            try
            {
                var count = await _financeiroService.GetCobrancaByResponsavel(responsavelId, null, null, null, null, null);
                return Ok(new { ResponsavelFinanceiroId = responsavelId, Quantidade = count.Count });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
