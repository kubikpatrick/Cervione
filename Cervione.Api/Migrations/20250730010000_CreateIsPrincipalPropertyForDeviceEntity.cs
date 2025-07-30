using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cervione.Api.Migrations
{
    /// <inheritdoc />
    public partial class CreateIsPrincipalPropertyForDeviceEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPrincipal",
                table: "Devices",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrincipal",
                table: "Devices");
        }
    }
}
