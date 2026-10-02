using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConstruSys.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDeleteClientesProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataExclusao",
                table: "Produtos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Excluido",
                table: "Produtos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataExclusao",
                table: "Clientes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Excluido",
                table: "Clientes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataExclusao",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "Excluido",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "DataExclusao",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Excluido",
                table: "Clientes");
        }
    }
}
