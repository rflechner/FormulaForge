using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormulaForge.ApiService.Persistence.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Projects",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Projects");
        }
    }
}
