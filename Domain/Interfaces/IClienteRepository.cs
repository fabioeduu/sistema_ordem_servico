namespace Domain.Interfaces
{
    // Interface específica para repositório de Clientes
    public interface IClienteRepository : IRepository<Entities.Cliente>
    {
        Task<Entities.Cliente?> GetByCPFAsync(string cpf);
        Task<IEnumerable<Entities.Cliente>> GetByNomeAsync(string nome);
    }
}
