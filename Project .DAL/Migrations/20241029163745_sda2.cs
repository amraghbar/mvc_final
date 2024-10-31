using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_.DAL.Migrations
{
    /// <inheritdoc />
    public partial class sda2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.AddColumn<string>(
                name: "Item_Af_Price",
                table: "ProductBases",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Item_Be_Price",
                table: "ProductBases",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Item_Description",
                table: "ProductBases",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductBases",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductBases_ProductId",
                table: "ProductBases",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBases_Products_ProductId",
                table: "ProductBases",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductBases_Products_ProductId",
                table: "ProductBases");

            migrationBuilder.DropIndex(
                name: "IX_ProductBases_ProductId",
                table: "ProductBases");

            migrationBuilder.DropColumn(
                name: "Item_Af_Price",
                table: "ProductBases");

            migrationBuilder.DropColumn(
                name: "Item_Be_Price",
                table: "ProductBases");

            migrationBuilder.DropColumn(
                name: "Item_Description",
                table: "ProductBases");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductBases");

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    Af_Price = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Be_Price = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_ProductId",
                table: "Items",
                column: "ProductId");
        }
    }
}
