using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    // Serviço de aplicação para gerenciar Clientes
    public class ClienteService : BaseService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ClienteDTO> CriarClienteAsync(ClienteDTO dto)
        {
            // Validar se CPF já existe
            var clienteExistente = await _clienteRepository.GetByCPFAsync(dto.CPF);
            if (clienteExistente != null)
            {
                throw new Exceptions.BusinessException("Já existe um cliente com este CPF.");
            }

            // Criar entidade
            var cliente = new Cliente
            {
                Nome = dto.Nome ?? string.Empty,
                CPF = dto.CPF ?? string.Empty,
                Telefone = dto.Telefone ?? string.Empty,
                Email = dto.Email ?? string.Empty,
                Endereco = dto.Endereco ?? string.Empty
            };

            // Salvar
            var resultado = await _clienteRepository.AddAsync(cliente);

            // Retornar DTO
            return new ClienteDTO
            {
                Id = resultado.Id,
                Nome = resultado.Nome,
                CPF = resultado.CPF,
                Telefone = resultado.Telefone,
                Email = resultado.Email,
                Endereco = resultado.Endereco
            };
        }

        public async Task<ClienteDTO> ObterPorIdAsync(int id)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);
            if (cliente == null)
            {
                throw new Exceptions.BusinessException("Cliente não encontrado.");
            }

            return ConverterParaDTO(cliente);
        }

        public async Task<IEnumerable<ClienteDTO>> ListarTodosAsync()
        {
            var clientes = await _clienteRepository.GetAllAsync();
            return clientes.Select(c => ConverterParaDTO(c));
        }

        private ClienteDTO ConverterParaDTO(Cliente cliente)
        {
            return new ClienteDTO
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                CPF = cliente.CPF,
                Telefone = cliente.Telefone,
                Email = cliente.Email,
                Endereco = cliente.Endereco
            };
        }
    }
}
