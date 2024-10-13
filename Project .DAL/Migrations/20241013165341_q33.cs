using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_.DAL.Migrations
{
    /// <inheritdoc />
    public partial class q33 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Latests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CommentsCount",
                table: "Latests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Latests");

            migrationBuilder.DropColumn(
                name: "CommentsCount",
                table: "Latests");
        }
    }
}
