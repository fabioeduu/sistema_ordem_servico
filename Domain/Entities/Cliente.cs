namespace Domain.Entities
{
    // Entidade que representa um cliente
    public class Cliente : BaseEntity
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }

        // Relacionamento com Veículos
        public ICollection<Veiculo> Veiculos { get; set; }
    }
}
