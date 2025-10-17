using Domain.Entities;

namespace Domain.Interfaces
{

    // Interface específica para repositório de Ordens de Serviço
    public interface IOrdemServicoRepository : IRepository<OrdemServico>
    {
        Task<IEnumerable<OrdemServico>> GetByStatusAsync(StatusOrdemServico status);
        Task<IEnumerable<OrdemServico>> GetByVeiculoIdAsync(int veiculoId);
        Task<IEnumerable<OrdemServico>> GetByPeriodoAsync(DateTime dataInicio, DateTime dataFim);
    }
}
