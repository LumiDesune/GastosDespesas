using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GastosDespesas.API.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Pessoas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Despesas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_CategoriaId",
                table: "Despesas",
                column: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_Categorias_CategoriaId",
                table: "Despesas",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_Categorias_CategoriaId",
                table: "Despesas");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropIndex(
                name: "IX_Despesas_CategoriaId",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Despesas");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Pessoas",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
