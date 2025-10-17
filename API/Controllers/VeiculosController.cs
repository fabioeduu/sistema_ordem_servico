using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers

{


    // Controller para gerenciar Veículos

    [ApiController]
    [Route("api/[controller]")]
    public class VeiculosController : ControllerBase
    {
        private readonly VeiculoService _veiculoService;

        public VeiculosController(VeiculoService veiculoService)
        {
            _veiculoService = veiculoService;
        }



        // Listar todos os veículos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VeiculoDTO>>> GetAll()
        {
            try
            {
                var veiculos = await _veiculoService.ListarTodosAsync();
                return Ok(veiculos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao listar veículos", erro = ex.Message });
            }
        }

        // Buscar veículo por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<VeiculoDTO>> GetById(int id)
        {
            try
            {
                var veiculo = await _veiculoService.ObterPorIdAsync(id);
                return Ok(veiculo);
            }
            catch (Application.Exceptions.BusinessException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao buscar veículo", erro = ex.Message });
            }
        }



        // Criar novo veículo
        
        [HttpPost]
        public async Task<ActionResult<VeiculoDTO>> Create([FromBody] VeiculoDTO veiculoDTO)
        {
            try
            {
                var veiculo = await _veiculoService.CriarVeiculoAsync(veiculoDTO);
                return CreatedAtAction(nameof(GetById), new { id = veiculo.Id }, veiculo);
            }
            catch (Application.Exceptions.BusinessException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao criar veículo", erro = ex.Message });
            }
        }
    }
}
