using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crypto.Data.Migrations
{
    /// <inheritdoc />
    public partial class SoftableDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Removed",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Removed",
                table: "Currencies",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Removed",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Removed",
                table: "Currencies");
        }
    }
}
