namespace Domain.Interfaces
{
    // Interface específica para repositório de Veículos
    public interface IVeiculoRepository : IRepository<Entities.Veiculo>
    {
        Task<Entities.Veiculo?> GetByPlacaAsync(string placa);
        Task<IEnumerable<Entities.Veiculo>> GetByClienteIdAsync(int clienteId);
    }
}
