using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Migrations
{
    public partial class oneToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assortments_Studia_AssortmentId",
                table: "Assortments");

            migrationBuilder.DropIndex(
                name: "IX_Assortments_AssortmentId",
                table: "Assortments");

            migrationBuilder.RenameColumn(
                name: "AssortmentId",
                table: "Assortments",
                newName: "StudiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Assortments_StudiaId",
                table: "Assortments",
                column: "StudiaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assortments_Studia_StudiaId",
                table: "Assortments",
                column: "StudiaId",
                principalTable: "Studia",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assortments_Studia_StudiaId",
                table: "Assortments");

            migrationBuilder.DropIndex(
                name: "IX_Assortments_StudiaId",
                table: "Assortments");

            migrationBuilder.RenameColumn(
                name: "StudiaId",
                table: "Assortments",
                newName: "AssortmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Assortments_AssortmentId",
                table: "Assortments",
                column: "AssortmentId",
                unique: true,
                filter: "[AssortmentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Assortments_Studia_AssortmentId",
                table: "Assortments",
                column: "AssortmentId",
                principalTable: "Studia",
                principalColumn: "Id");
        }
    }
}
