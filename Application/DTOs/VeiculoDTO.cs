namespace Application.DTOs
{
    // DTO para transferência de dados de Veículo
   
    public class VeiculoDTO : BaseDTO
    {
        public string Placa { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public int Ano { get; set; }
        public string Cor { get; set; } = string.Empty;
        public int ClienteId { get; set; }
        public string NomeCliente { get; set; } = string.Empty;
    }
}
