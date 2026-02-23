using KeduPayments.Application.DTOs;
using KeduPayments.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KeduPayments.Controllers
{
    /// <summary>
    /// Controlador responsável por gerenciar os centros de custo.
    /// </summary>
    [Route("centros-de-custo")]
    [ApiController]
    public class CentroCustoController : ControllerBase
    {
        private readonly ICentroCustoService _centroCustoService;

        /// <summary>
        /// Método construtor para criar uma nova instância do controlador de Centro de Custo.
        /// </summary>
        /// <param name="centroCustoService"></param>
        public CentroCustoController(ICentroCustoService centroCustoService)
        {
            _centroCustoService = centroCustoService;
        }

        /// <summary>
        /// Endpoint para criar um novo centro de custo.
        /// </summary>
        /// <param name="centroCustoDTO">Objeto CentroCustoDTO contendo as informações do centro de custo a ser criado.</param>
        /// <returns>Retorna o centro de custo criado.</returns>
        [HttpPost]
        public async Task<ActionResult<CentroCustoResponse>> Create([FromBody] CentroCustoRequest centroCustoRequest)
        {
            try
            {
                await _centroCustoService.Add(centroCustoRequest);
                return CreatedAtAction(nameof(List), null, centroCustoRequest);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Endpoint para listar os centros de custo disponíveis.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CentroCustoResponse>>> List()
        {
            try
            {
                var list = await _centroCustoService.GetCentroCusto();
                return Ok(list);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
    }
}
