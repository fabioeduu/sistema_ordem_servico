using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    // Controller para gerenciar Clientes
    
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteService _clienteService;

        public ClientesController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }




        // Listar todos os clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetAll()
        {
            try
            {
                var clientes = await _clienteService.ListarTodosAsync();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao listar clientes", erro = ex.Message });
            }
        }

        // Buscar cliente por ID

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDTO>> GetById(int id)
        {
            try
            {
                var cliente = await _clienteService.ObterPorIdAsync(id);
                return Ok(cliente);
            }
            catch (Application.Exceptions.BusinessException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao buscar cliente", erro = ex.Message });
            }
        }



        // Criar novo cliente

        [HttpPost]
        public async Task<ActionResult<ClienteDTO>> Create([FromBody] ClienteDTO clienteDTO)
        {
            try
            {
                var cliente = await _clienteService.CriarClienteAsync(clienteDTO);
                return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, cliente);
            }
            catch (Application.Exceptions.BusinessException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao criar cliente", erro = ex.Message });
            }
        }
    }
}
