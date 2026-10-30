using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GastosDespesas.API.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarPessoa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PessoaId",
                table: "Despesas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataNascimento = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_PessoaId",
                table: "Despesas",
                column: "PessoaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_Pessoas_PessoaId",
                table: "Despesas",
                column: "PessoaId",
                principalTable: "Pessoas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_Pessoas_PessoaId",
                table: "Despesas");

            migrationBuilder.DropTable(
                name: "Pessoas");

            migrationBuilder.DropIndex(
                name: "IX_Despesas_PessoaId",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "PessoaId",
                table: "Despesas");
        }
    }
}
