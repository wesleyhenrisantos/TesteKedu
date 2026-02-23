using KeduPayments.Application.DTOs;
using KeduPayments.Application.Interfaces;
using KeduPayments.Domain.Entities;
using Microsoft.AspNetCore.Mvc;


namespace KeduPayments.Controllers
{
    /// <summary>
    /// Controlador responsável por gerenciar as operações financeiras, incluindo planos de pagamento, cobranças e pagamentos.
    /// </summary>
    [ApiController]
    public class FinanceiroController : ControllerBase
    {
        private readonly IFinanceiroService _financeiroService;

        /// <summary>
        /// Método Construtor para criar uma nova instância do controlador financeiro, injetando os serviços necessários para gerenciar planos de pagamento, cobranças e pagamentos.
        /// </summary>
        /// <param name="financeiroService">Serviço responsável por gerenciar as operações financeiras.</param>
        public FinanceiroController(IFinanceiroService financeiroService)
        {
            _financeiroService = financeiroService;
        }

        /// <summary>
        /// Endpoint para criar um novo plano de pagamento, recebendo os detalhes do plano no corpo da requisição e retornando o plano criado com um status HTTP 201 (Created).
        /// </summary>
        /// <param name="request">Detalhes do plano de pagamento a ser criado.</param>
        /// <returns>Retorna o plano de pagamento criado.</returns>
        [HttpPost]
        [Route("planos-de-pagamento")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<PlanoPagamentoResponse>> Create([FromBody] PlanoPagamentoRequest request)
        {
            try
            {
                var result = await _financeiroService.AddPlanoPagamento(request);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Endpoint para retorna planos de pagamento.
        /// </summary>
        /// <param name="id">ID do plano de pagamento a ser retornado.</param>
        /// <returns>Retorna o plano de pagamento correspondente ao ID fornecido.</returns>
        [HttpGet]
        [Route("planos-de-pagamento/{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PlanoPagamentoResponse>> GetById(int id)
        {
            try
            {
                var result = await _financeiroService.GetById(id);
                result.ValorTotal = result.Cobrancas.Aggregate(0m, (acc, c) => acc + c.Valor);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Endpoint para calcular o valor total de um plano de pagamento, somando os valores de todas as cobranças associadas ao plano e retornando o total calculado.
        /// </summary>
        /// <param name="id">ID do plano de pagamento para o qual o total será calculado.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("planos-de-pagamento/{id:int}/total")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<object>> GetTotal(int id)
        {
            try
            {
                var planoPagamento = await _financeiroService.GetById(id);
                planoPagamento.ValorTotal = planoPagamento.Cobrancas.Aggregate(0m, (acc, c) => acc + c.Valor);
                return Ok(new { PlanoPagamentoId = id, Total = planoPagamento.ValorTotal });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Endpoint para registrar um pagamento para uma cobrança específica.
        /// </summary>
        /// <param name="cobrancaId">ID da cobrança para a qual o pagamento será registrado.</param>
        /// <returns>Retorna o pagamento registrado ou um erro caso o registro falhe.</returns>
        [HttpPost]
        [Route("cobrancas/{cobrancaId:int}/pagamentos")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagamentoResponse>> Register(int cobrancaId, decimal? valor, string? status)
        {
            try
            {
                PagamentoRequest request = new PagamentoRequest()
                {
                    CobrancaId = cobrancaId,
                    DataPagamento = DateTime.UtcNow
                };

                await _financeiroService.UpdateStatusCobranca(cobrancaId, status, valor);
                var result = await _financeiroService.AddPagamentoPorCobranca(request);

                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
    }
}
