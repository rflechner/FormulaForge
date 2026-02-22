using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormulaForge.ApiService.Persistence.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DecimalScalarValues = table.Column<string>(type: "text", nullable: false),
                    IntegerScalarValues = table.Column<string>(type: "text", nullable: false),
                    BooleanScalarValues = table.Column<string>(type: "text", nullable: false),
                    DecimalTimeSeriesValues = table.Column<string>(type: "text", nullable: false),
                    IntegerTimeSeriesValues = table.Column<string>(type: "text", nullable: false),
                    BooleanTimeSeriesValues = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
