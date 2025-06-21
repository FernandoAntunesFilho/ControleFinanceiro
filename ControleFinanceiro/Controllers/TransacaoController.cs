using ControleFinanceiro.Models.DTOs;
using ControleFinanceiro.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class TransacaoController : ControllerBase
    {
        private readonly ITransacaoService _service;
        public TransacaoController(ITransacaoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TransacaoRequestAddDTO request)
        {
            try
            {
                var response = await _service.AddAsync(request);
                return Created("", response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] TransacaoRequestGetDto request)
        {
            try
            {
                var response = await _service.GetAsync(request.dataInicial, request.dataFinal);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TransacaoRequestDTO request)
        {
            try
            {
                var result = await _service.UpdateAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
