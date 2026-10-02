using ConstruSys.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConstruSys.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Fornecedor> Fornecedores { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigurarProduto(modelBuilder);
            ConfigurarCliente(modelBuilder);
            ConfigurarFornecedor(modelBuilder);
            ConfigurarUsuario(modelBuilder);
        }

        private static void ConfigurarProduto(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>(entity =>
            {
                entity.ToTable("Produtos");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Codigo)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.HasIndex(x => x.Codigo)
                    .IsUnique();

                entity.Property(x => x.CodigoBarras)
                    .HasMaxLength(50);

                entity.HasIndex(x => x.CodigoBarras);

                entity.Property(x => x.Nome)
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(x => x.Descricao)
                    .HasMaxLength(500);

                entity.Property(x => x.Categoria)
                    .HasMaxLength(100);

                entity.Property(x => x.Subcategoria)
                    .HasMaxLength(100);

                entity.Property(x => x.Marca)
                    .HasMaxLength(100);

                entity.Property(x => x.UnidadeMedida)
                    .HasMaxLength(10)
                    .IsRequired();

                entity.Property(x => x.PrecoCusto)
                    .HasPrecision(18, 2);

                entity.Property(x => x.PrecoVenda)
                    .HasPrecision(18, 2);

                entity.Property(x => x.EstoqueAtual)
                    .HasPrecision(18, 3);

                entity.Property(x => x.EstoqueMinimo)
                    .HasPrecision(18, 3);

                entity.Property(x => x.EstoqueMaximo)
                    .HasPrecision(18, 3);

                entity.Property(x => x.LocalizacaoEstoque)
                    .HasMaxLength(100);

                entity.Property(x => x.Peso)
                    .HasPrecision(18, 3);

                entity.Ignore(x => x.MargemLucro);
            });
        }

        private static void ConfigurarCliente(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Clientes");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.TipoPessoa)
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(x => x.NomeRazaoSocial)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(x => x.NomeFantasia)
                    .HasMaxLength(200);

                entity.Property(x => x.CpfCnpj)
                    .HasMaxLength(20);

                entity.HasIndex(x => x.CpfCnpj);

                entity.Property(x => x.RgIe)
                    .HasMaxLength(30);

                entity.Property(x => x.Telefone)
                    .HasMaxLength(20);

                entity.Property(x => x.WhatsApp)
                    .HasMaxLength(20);

                entity.Property(x => x.Email)
                    .HasMaxLength(150);

                entity.Property(x => x.Cep)
                    .HasMaxLength(10);

                entity.Property(x => x.Endereco)
                    .HasMaxLength(200);

                entity.Property(x => x.Numero)
                    .HasMaxLength(20);

                entity.Property(x => x.Complemento)
                    .HasMaxLength(100);

                entity.Property(x => x.Bairro)
                    .HasMaxLength(100);

                entity.Property(x => x.Cidade)
                    .HasMaxLength(100);

                entity.Property(x => x.Estado)
                    .HasMaxLength(2);

                entity.Property(x => x.LimiteCredito)
                    .HasPrecision(18, 2);

                entity.Property(x => x.Observacoes)
                    .HasMaxLength(1000);
            });
        }

        private static void ConfigurarFornecedor(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fornecedor>(entity =>
            {
                entity.ToTable("Fornecedores");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.RazaoSocial)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(x => x.NomeFantasia)
                    .HasMaxLength(200);

                entity.Property(x => x.Cnpj)
                    .HasMaxLength(20)
                    .IsRequired();

                entity.HasIndex(x => x.Cnpj)
                    .IsUnique();

                entity.Property(x => x.InscricaoEstadual)
                    .HasMaxLength(30);

                entity.Property(x => x.NomeContato)
                    .HasMaxLength(150);

                entity.Property(x => x.Telefone)
                    .HasMaxLength(20);

                entity.Property(x => x.WhatsApp)
                    .HasMaxLength(20);

                entity.Property(x => x.Email)
                    .HasMaxLength(150);

                entity.Property(x => x.Cep)
                    .HasMaxLength(10);

                entity.Property(x => x.Endereco)
                    .HasMaxLength(200);

                entity.Property(x => x.Numero)
                    .HasMaxLength(20);

                entity.Property(x => x.Complemento)
                    .HasMaxLength(100);

                entity.Property(x => x.Bairro)
                    .HasMaxLength(100);

                entity.Property(x => x.Cidade)
                    .HasMaxLength(100);

                entity.Property(x => x.Estado)
                    .HasMaxLength(2);

                entity.Property(x => x.Representante)
                    .HasMaxLength(150);

                entity.Property(x => x.Observacoes)
                    .HasMaxLength(1000);
            });
        }

        private static void ConfigurarUsuario(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuarios");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Nome)
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(x => x.Login)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.HasIndex(x => x.Login)
                    .IsUnique();

                entity.Property(x => x.SenhaHash)
                    .HasMaxLength(500)
                    .IsRequired();

                entity.Property(x => x.Perfil)
                    .HasMaxLength(50)
                    .IsRequired();
            });
        }
    }
}