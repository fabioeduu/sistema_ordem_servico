using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    
    // Implementação do repositório de Ordens de Serviço
    public class OrdemServicoRepository : Repository<OrdemServico>, IOrdemServicoRepository
    {
        private readonly ApplicationDbContext _context;

        public OrdemServicoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        // Override para sempre incluir o Veículo ao buscar todas as ordens
        public override async Task<IEnumerable<OrdemServico>> GetAllAsync()
        {
            return await _context.OrdensServico
                .Include(os => os.Veiculo)
                .Include(os => os.Servicos)
                .ToListAsync();
        }

        // Override para sempre incluir o Veículo ao buscar por ID
        public override async Task<OrdemServico> GetByIdAsync(int id)
        {
            var ordem = await _context.OrdensServico
                .Include(os => os.Veiculo)
                .Include(os => os.Servicos)
                .FirstOrDefaultAsync(os => os.Id == id);
            
            return ordem!;
        }

        public async Task<IEnumerable<OrdemServico>> GetByStatusAsync(StatusOrdemServico status)
        {
            return await _context.OrdensServico
                .Include(os => os.Veiculo)
                .Include(os => os.Servicos)
                .Where(os => os.Status == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrdemServico>> GetByVeiculoIdAsync(int veiculoId)
        {
            return await _context.OrdensServico
                .Include(os => os.Veiculo)
                .Include(os => os.Servicos)
                .Where(os => os.VeiculoId == veiculoId)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrdemServico>> GetByPeriodoAsync(DateTime dataInicio, DateTime dataFim)
        {
            return await _context.OrdensServico
                .Include(os => os.Veiculo)
                .Include(os => os.Servicos)
                .Where(os => os.DataAbertura >= dataInicio && os.DataAbertura <= dataFim)
                .ToListAsync();
        }
    }
}
