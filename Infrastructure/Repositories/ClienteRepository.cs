using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    // Implementação do repositório de Clientes
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        private readonly ApplicationDbContext _context;

        public ClienteRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Cliente?> GetByCPFAsync(string cpf)
        {
            return await _context.Clientes
                .FirstOrDefaultAsync(c => c.CPF == cpf);
        }

        public async Task<IEnumerable<Cliente>> GetByNomeAsync(string nome)
        {
            return await _context.Clientes
                .Where(c => c.Nome.Contains(nome))
                .ToListAsync();
        }
    }
}
