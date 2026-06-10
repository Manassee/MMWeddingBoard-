using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MMWeddingBoard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameTitleToBrideGroom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BrideName",
                table: "Weddings",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GroomName",
                table: "Weddings",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrideName",
                table: "Weddings");

            migrationBuilder.DropColumn(
                name: "GroomName",
                table: "Weddings");
        }
    }
}
