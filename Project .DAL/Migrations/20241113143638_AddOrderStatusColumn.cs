using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderStatusColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "OrderStatus",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "AspNetUsers");
        }
    }
}
