using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_.DAL.Migrations
{
    /// <inheritdoc />
    public partial class asdsa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_ProductBase_ProductId",
                table: "CartItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductBase",
                table: "ProductBase");

            migrationBuilder.RenameTable(
                name: "ProductBase",
                newName: "ProductBases");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductBases",
                table: "ProductBases",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_ProductBases_ProductId",
                table: "CartItems",
                column: "ProductId",
                principalTable: "ProductBases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_ProductBases_ProductId",
                table: "CartItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductBases",
                table: "ProductBases");

            migrationBuilder.RenameTable(
                name: "ProductBases",
                newName: "ProductBase");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductBase",
                table: "ProductBase",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_ProductBase_ProductId",
                table: "CartItems",
                column: "ProductId",
                principalTable: "ProductBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
