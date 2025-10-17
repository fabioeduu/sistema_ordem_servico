namespace Domain.Entities
{
    // Entidade que representa uma ordem de serviço automotivo
    public class OrdemServico : BaseEntity
    {
        public DateTime DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; }
        public string Descricao { get; set; }
        public StatusOrdemServico Status { get; set; }
        public decimal ValorTotal { get; set; }
        public string Observacoes { get; set; }

        // Relacionamento com Veículo
        public int VeiculoId { get; set; }
        public Veiculo Veiculo { get; set; }

        // Relacionamento com Serviços
        public ICollection<Servico> Servicos { get; set; }
    }

    // Status possíveis para uma ordem de serviço
    public enum StatusOrdemServico
    {
        Aberta = 1,
        EmAndamento = 2,
        Aguardando = 3,
        Concluida = 4,
        Cancelada = 5
    }
}
