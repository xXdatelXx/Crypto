using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crypto.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFieldCurrenciesUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserCurrencyId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserCurrencyId",
                table: "Currencies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserCurrencyId",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserCurrencyId",
                table: "Currencies",
                type: "uuid",
                nullable: true);
        }
    }
}
