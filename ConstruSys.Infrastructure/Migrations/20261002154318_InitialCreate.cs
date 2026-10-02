using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConstruSys.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoPessoa = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NomeRazaoSocial = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NomeFantasia = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CpfCnpj = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    RgIe = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    WhatsApp = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Cep = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Endereco = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Numero = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Complemento = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Bairro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    LimiteCredito = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fornecedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RazaoSocial = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NomeFantasia = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Cnpj = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    InscricaoEstadual = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    NomeContato = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    WhatsApp = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Cep = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Endereco = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Numero = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Complemento = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Bairro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    Representante = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Observacoes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CodigoBarras = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Categoria = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Subcategoria = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Marca = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UnidadeMedida = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PrecoCusto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PrecoVenda = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    EstoqueAtual = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    EstoqueMinimo = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    EstoqueMaximo = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: true),
                    LocalizacaoEstoque = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Peso = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Login = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SenhaHash = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Perfil = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UltimoAcesso = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_CpfCnpj",
                table: "Clientes",
                column: "CpfCnpj");

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedores_Cnpj",
                table: "Fornecedores",
                column: "Cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_Codigo",
                table: "Produtos",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CodigoBarras",
                table: "Produtos",
                column: "CodigoBarras");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Login",
                table: "Usuarios",
                column: "Login",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Fornecedores");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
