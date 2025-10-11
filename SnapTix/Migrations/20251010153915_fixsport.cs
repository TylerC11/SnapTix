using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SnapTix.Migrations
{
    /// <inheritdoc />
    public partial class fixsport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Sport");

            migrationBuilder.DropColumn(
                name: "Owner",
                table: "Sport");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Sport",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "Sport",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
