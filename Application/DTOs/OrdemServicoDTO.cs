namespace Application.DTOs
{
    // DTO para transferência de dados de Ordem de Serviço
    
    public class OrdemServicoDTO : BaseDTO
    {
        public DateTime DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public decimal ValorTotal { get; set; }
        public string Observacoes { get; set; } = string.Empty;
        public int VeiculoId { get; set; }
        public string PlacaVeiculo { get; set; } = string.Empty;
        public string ModeloVeiculo { get; set; } = string.Empty;
        public List<ServicoDTO> Servicos { get; set; } = new();
    }
}
