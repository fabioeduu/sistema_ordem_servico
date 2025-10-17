using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// Implementação do repositório de Veículos
    /// </summary>
    public class VeiculoRepository : Repository<Veiculo>, IVeiculoRepository
    {
        private readonly ApplicationDbContext _context;

        public VeiculoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        // Override para sempre incluir o Cliente ao buscar todos os veículos
        public override async Task<IEnumerable<Veiculo>> GetAllAsync()
        {
            return await _context.Veiculos
                .Include(v => v.Cliente)
                .ToListAsync();
        }

        // Override para sempre incluir o Cliente ao buscar por ID
        public override async Task<Veiculo> GetByIdAsync(int id)
        {
            var veiculo = await _context.Veiculos
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(v => v.Id == id);
            
            return veiculo!;
        }

        public async Task<Veiculo?> GetByPlacaAsync(string placa)
        {
            return await _context.Veiculos
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(v => v.Placa == placa);
        }

        public async Task<IEnumerable<Veiculo>> GetByClienteIdAsync(int clienteId)
        {
            return await _context.Veiculos
                .Include(v => v.Cliente)
                .Where(v => v.ClienteId == clienteId)
                .ToListAsync();
        }
    }
}
