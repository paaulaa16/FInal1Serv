using Microsoft.EntityFrameworkCore.Migrations;

namespace apiRecuBase.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Influencer",
                columns: table => new
                {
                    InfluencerId = table.Column<int>(type: "int", nullable: false),
                    Manager = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Influencer", x => x.InfluencerId);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    ProductoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ValorEconomico = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.ProductoId);
                });

            migrationBuilder.CreateTable(
                name: "Contrato",
                columns: table => new
                {
                    ContratoId = table.Column<int>(type: "int", nullable: false),
                    InfluencerId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contrato", x => x.ContratoId);
                    table.ForeignKey(
                        name: "FK_Contrato_Influencer_InfluencerId",
                        column: x => x.InfluencerId,
                        principalTable: "Influencer",
                        principalColumn: "InfluencerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contrato_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "ProductoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_InfluencerId",
                table: "Contrato",
                column: "InfluencerId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_ProductoId",
                table: "Contrato",
                column: "ProductoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contrato");

            migrationBuilder.DropTable(
                name: "Influencer");

            migrationBuilder.DropTable(
                name: "Producto");
        }
    }
}
