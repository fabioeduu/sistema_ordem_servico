using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{

    // Serviço de aplicação para gerenciar Veículos
    public class VeiculoService : BaseService
    {
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly IClienteRepository _clienteRepository;

        public VeiculoService(
            IVeiculoRepository veiculoRepository,
            IClienteRepository clienteRepository)
        {
            _veiculoRepository = veiculoRepository;
            _clienteRepository = clienteRepository;
        }

        public async Task<VeiculoDTO> CriarVeiculoAsync(VeiculoDTO dto)
        {
            // Validar se a placa já existe
            var veiculoExistente = await _veiculoRepository.GetByPlacaAsync(dto.Placa);
            if (veiculoExistente != null)
            {
                throw new Exceptions.BusinessException("Já existe um veículo com esta placa.");
            }

            // Validar se o cliente existe
            var cliente = await _clienteRepository.GetByIdAsync(dto.ClienteId);
            if (cliente == null)
            {
                throw new Exceptions.BusinessException("Cliente não encontrado.");
            }

            // Criar entidade
            var veiculo = new Veiculo
            {
                Placa = dto.Placa ?? string.Empty,
                Marca = dto.Marca ?? string.Empty,
                Modelo = dto.Modelo ?? string.Empty,
                Ano = dto.Ano,
                Cor = dto.Cor ?? string.Empty,
                ClienteId = dto.ClienteId
            };

            // Salvar
            var resultado = await _veiculoRepository.AddAsync(veiculo);

            // Retornar DTO
            return new VeiculoDTO
            {
                Id = resultado.Id,
                Placa = resultado.Placa,
                Marca = resultado.Marca,
                Modelo = resultado.Modelo,
                Ano = resultado.Ano,
                Cor = resultado.Cor,
                ClienteId = resultado.ClienteId,
                NomeCliente = cliente.Nome
            };
        }

        public async Task<VeiculoDTO> ObterPorIdAsync(int id)
        {
            var veiculo = await _veiculoRepository.GetByIdAsync(id);
            if (veiculo == null)
            {
                throw new Exceptions.BusinessException("Veículo não encontrado.");
            }

            return ConverterParaDTO(veiculo);
        }

        public async Task<IEnumerable<VeiculoDTO>> ListarTodosAsync()
        {
            var veiculos = await _veiculoRepository.GetAllAsync();
            return veiculos.Select(v => ConverterParaDTO(v));
        }

        private VeiculoDTO ConverterParaDTO(Veiculo veiculo)
        {
            return new VeiculoDTO
            {
                Id = veiculo.Id,
                Placa = veiculo.Placa,
                Marca = veiculo.Marca,
                Modelo = veiculo.Modelo,
                Ano = veiculo.Ano,
                Cor = veiculo.Cor,
                ClienteId = veiculo.ClienteId,
                NomeCliente = veiculo.Cliente?.Nome ?? string.Empty
            };
        }
    }
}
