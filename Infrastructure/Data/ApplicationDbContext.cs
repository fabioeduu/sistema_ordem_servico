using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Data
{
    // Contexto do banco de dados usando Entity Framework Core
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<OrdemServico> OrdensServico { get; set; }
        public DbSet<Servico> Servicos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configuração da entidade Cliente
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(200);
                entity.Property(e => e.CPF).IsRequired().HasMaxLength(14);
                entity.Property(e => e.Telefone).HasMaxLength(20);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.Endereco).HasMaxLength(300);

                entity.HasIndex(e => e.CPF).IsUnique();
            });

            // Configuração da entidade Veiculo
            modelBuilder.Entity<Veiculo>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Placa).IsRequired().HasMaxLength(10);
                entity.Property(e => e.Marca).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Modelo).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Cor).HasMaxLength(30);

                entity.HasIndex(e => e.Placa).IsUnique();

                entity.HasOne(e => e.Cliente)
                      .WithMany(c => c.Veiculos)
                      .HasForeignKey(e => e.ClienteId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuração da entidade OrdemServico
            modelBuilder.Entity<OrdemServico>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Descricao).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Observacoes).HasMaxLength(1000);
                entity.Property(e => e.ValorTotal).HasPrecision(10, 2);

                entity.HasOne(e => e.Veiculo)
                      .WithMany(v => v.OrdensServico)
                      .HasForeignKey(e => e.VeiculoId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuração da entidade Servico
            modelBuilder.Entity<Servico>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Descricao).IsRequired().HasMaxLength(300);
                entity.Property(e => e.Valor).HasPrecision(10, 2);

                entity.HasOne(e => e.OrdemServico)
                      .WithMany(os => os.Servicos)
                      .HasForeignKey(e => e.OrdemServicoId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
