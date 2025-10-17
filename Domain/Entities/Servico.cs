namespace Domain.Entities
{
    // Entidade que representa um serviço realizado
    public class Servico : BaseEntity
    {
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int TempoEstimado { get; set; } // em minutos

        // Relacionamento com Ordem de Serviço
        public int OrdemServicoId { get; set; }
        public OrdemServico OrdemServico { get; set; }
    }
}
