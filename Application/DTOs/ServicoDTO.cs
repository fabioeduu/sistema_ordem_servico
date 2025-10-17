namespace Application.DTOs
{
    
    // DTO para transferência de dados de Serviço
    
    public class ServicoDTO : BaseDTO
    {
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int TempoEstimado { get; set; }
        public int OrdemServicoId { get; set; }
    }
}
