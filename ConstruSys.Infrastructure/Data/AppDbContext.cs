using ConstruSys.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConstruSys.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(
            DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Fornecedor> Fornecedores { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<MovimentacaoEstoque> MovimentacoesEstoque { get; set; }

        public DbSet<CategoriaProduto> CategoriasProduto { get; set; }

        public DbSet<MarcaProduto> MarcasProduto { get; set; }

        public DbSet<LocalizacaoEstoqueCadastro> LocalizacoesEstoque { get; set; }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigurarProduto(modelBuilder);
            ConfigurarCliente(modelBuilder);
            ConfigurarFornecedor(modelBuilder);
            ConfigurarUsuario(modelBuilder);
            ConfigurarMovimentacaoEstoque(modelBuilder);
            ConfigurarCadastrosAuxiliaresProduto(modelBuilder);
        }

        private static void ConfigurarCadastrosAuxiliaresProduto(
    ModelBuilder modelBuilder)
        {
            DateTime dataSeed =
                new DateTime(
                    2026,
                    1,
                    1,
                    0,
                    0,
                    0,
                    DateTimeKind.Unspecified);

            // =========================================================
            // CATEGORIAS
            // =========================================================

            modelBuilder.Entity<CategoriaProduto>(
                entity =>
                {
                    entity.ToTable(
                        "CategoriasProduto");

                    entity.HasKey(
                        x => x.Id);

                    entity.Property(
                            x => x.Nome)
                        .HasMaxLength(100)
                        .IsRequired();

                    entity.Property(
                            x => x.Ativo)
                        .HasDefaultValue(true);

                    entity.Property(
                            x => x.DataCadastro)
                        .IsRequired();

                    entity.HasIndex(
                            x => x.Nome)
                        .IsUnique();

                    entity.HasData(
                        new CategoriaProduto
                        {
                            Id = 1,
                            Nome = "CIMENTO",
                            Ativo = true,
                            DataCadastro = dataSeed
                        },
                        new CategoriaProduto
                        {
                            Id = 2,
                            Nome = "ARGAMASSA",
                            Ativo = true,
                            DataCadastro = dataSeed
                        },
                        new CategoriaProduto
                        {
                            Id = 3,
                            Nome = "TIJOLOS E BLOCOS",
                            Ativo = true,
                            DataCadastro = dataSeed
                        },
                        new CategoriaProduto
                        {
                            Id = 4,
                            Nome = "AREIA E BRITA",
                            Ativo = true,
                            DataCadastro = dataSeed
                        },
                        new CategoriaProduto
                        {
                            Id = 5,
                            Nome = "TINTAS",
                            Ativo = true,
                            DataCadastro = dataSeed
                        },
                        new CategoriaProduto
                        {
                            Id = 6,
                            Nome = "HIDRÁULICA",
                            Ativo = true,
                            DataCadastro = dataSeed
                        },
                        new CategoriaProduto
                        {
                            Id = 7,
                            Nome = "ELÉTRICA",
                            Ativo = true,
                            DataCadastro = dataSeed
                        },
                        new CategoriaProduto
                        {
                            Id = 8,
                            Nome = "FERRAMENTAS",
                            Ativo = true,
                            DataCadastro = dataSeed
                        },
                        new CategoriaProduto
                        {
                            Id = 9,
                            Nome = "PISOS E REVESTIMENTOS",
                            Ativo = true,
                            DataCadastro = dataSeed
                        },
                        new CategoriaProduto
                        {
                            Id = 10,
                            Nome = "TELHAS",
                            Ativo = true,
                            DataCadastro = dataSeed
                        });
                });

            // =========================================================
            // MARCAS
            // =========================================================

            modelBuilder.Entity<MarcaProduto>(
                entity =>
                {
                    entity.ToTable(
                        "MarcasProduto");

                    entity.HasKey(
                        x => x.Id);

                    entity.Property(
                            x => x.Nome)
                        .HasMaxLength(100)
                        .IsRequired();

                    entity.Property(
                            x => x.Ativo)
                        .HasDefaultValue(true);

                    entity.Property(
                            x => x.DataCadastro)
                        .IsRequired();

                    entity.HasIndex(
                            x => x.Nome)
                        .IsUnique();
                });

            // =========================================================
            // LOCALIZAÇÕES
            // =========================================================

            modelBuilder.Entity<LocalizacaoEstoqueCadastro>(
                entity =>
                {
                    entity.ToTable(
                        "LocalizacoesEstoque");

                    entity.HasKey(
                        x => x.Id);

                    entity.Property(
                            x => x.Nome)
                        .HasMaxLength(100)
                        .IsRequired();

                    entity.Property(
                            x => x.Ativo)
                        .HasDefaultValue(true);

                    entity.Property(
                            x => x.DataCadastro)
                        .IsRequired();

                    entity.HasIndex(
                            x => x.Nome)
                        .IsUnique();

                    entity.HasData(
                        new LocalizacaoEstoqueCadastro
                        {
                            Id = 1,
                            Nome = "LOJA",
                            Ativo = true,
                            DataCadastro = dataSeed
                        },
                        new LocalizacaoEstoqueCadastro
                        {
                            Id = 2,
                            Nome = "DEPÓSITO",
                            Ativo = true,
                            DataCadastro = dataSeed
                        },
                        new LocalizacaoEstoqueCadastro
                        {
                            Id = 3,
                            Nome = "PÁTIO",
                            Ativo = true,
                            DataCadastro = dataSeed
                        });
                });
        }
        private static void ConfigurarProduto(
            ModelBuilder modelBuilder)
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

                entity.Property(x => x.Excluido)
                    .HasDefaultValue(false);

                entity.Ignore(x => x.MargemLucro);

                entity.Ignore(x => x.StatusEstoque);

                entity.Ignore(x => x.StatusCadastro);

                entity.HasQueryFilter(
                    x => !x.Excluido);
            });
        }

        private static void ConfigurarCliente(
            ModelBuilder modelBuilder)
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

                entity.Property(x => x.Excluido)
                    .HasDefaultValue(false);

                entity.HasQueryFilter(
                    x => !x.Excluido);
            });
        }

        private static void ConfigurarFornecedor(
            ModelBuilder modelBuilder)
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

        private static void ConfigurarUsuario(
            ModelBuilder modelBuilder)
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

        private static void ConfigurarMovimentacaoEstoque(
            ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovimentacaoEstoque>(entity =>
            {
                entity.ToTable("MovimentacoesEstoque");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Tipo)
                    .IsRequired();

                entity.Property(x => x.Quantidade)
                    .HasPrecision(18, 3)
                    .IsRequired();

                entity.Property(x => x.EstoqueAnterior)
                    .HasPrecision(18, 3)
                    .IsRequired();

                entity.Property(x => x.EstoquePosterior)
                    .HasPrecision(18, 3)
                    .IsRequired();

                entity.Property(x => x.Observacao)
                    .HasMaxLength(500);

                entity.Property(x => x.DataMovimentacao)
                    .IsRequired();

                entity.HasOne(x => x.Produto)
                    .WithMany()
                    .HasForeignKey(x => x.ProdutoId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(x => x.ProdutoId);

                entity.HasIndex(x => x.DataMovimentacao);
            });
        }
    }
}