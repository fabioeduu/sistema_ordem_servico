namespace Domain.Entities
{
    // Entidade que representa um veículo
    public class Veiculo : BaseEntity
    {
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public string Cor { get; set; }
        
        // Relacionamento com Cliente
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        // Relacionamento com Ordens de Serviço
        public ICollection<OrdemServico> OrdensServico { get; set; }
    }
}
