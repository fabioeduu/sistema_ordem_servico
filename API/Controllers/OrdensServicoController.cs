using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{



    // Controller para gerenciar Ordens de Serviço
    [ApiController]
    [Route("api/[controller]")]
    public class OrdensServicoController : ControllerBase
    {
        private readonly OrdemServicoService _ordemServicoService;

        public OrdensServicoController(OrdemServicoService ordemServicoService)
        {
            _ordemServicoService = ordemServicoService;
        }



        // Listar todas as ordens de serviço
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdemServicoDTO>>> GetAll()
        {
            try
            {
                var ordensServico = await _ordemServicoService.ListarTodasAsync();
                return Ok(ordensServico);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao listar ordens de serviço", erro = ex.Message });
            }
        }




        // Buscar ordem de serviço por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdemServicoDTO>> GetById(int id)
        {
            try
            {
                var ordemServico = await _ordemServicoService.ObterPorIdAsync(id);
                return Ok(ordemServico);
            }
            catch (Application.Exceptions.BusinessException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao buscar ordem de serviço", erro = ex.Message });
            }
        }

        
        

        // Criar nova ordem de serviço
        
        [HttpPost]
        public async Task<ActionResult<OrdemServicoDTO>> Create([FromBody] OrdemServicoDTO ordemServicoDTO)
        {
            try
            {
                var ordemServico = await _ordemServicoService.CriarOrdemServicoAsync(ordemServicoDTO);
                return CreatedAtAction(nameof(GetById), new { id = ordemServico.Id }, ordemServico);
            }
            catch (Application.Exceptions.BusinessException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao criar ordem de serviço", erro = ex.Message });
            }
        }

        // Fechar ordem de serviço
        [HttpPut("{id}/fechar")]
        public async Task<ActionResult> FecharOrdemServico(int id)
        {
            try
            {
                await _ordemServicoService.FecharOrdemServicoAsync(id);
                return Ok(new { mensagem = "Ordem de serviço fechada com sucesso" });
            }
            catch (Application.Exceptions.BusinessException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao fechar ordem de serviço", erro = ex.Message });
            }
        }
    }
}
