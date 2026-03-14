using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormulaForge.ApiService.Persistence.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class RebuildDatabase : Migration
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
                    Code = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BooleanScalarValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<bool>(type: "boolean", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooleanScalarValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BooleanScalarValues_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BooleanTimeSeriesValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooleanTimeSeriesValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BooleanTimeSeriesValues_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DecimalScalarValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DecimalScalarValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DecimalScalarValues_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DecimalTimeSeriesValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DecimalTimeSeriesValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DecimalTimeSeriesValues_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IntegerScalarValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegerScalarValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IntegerScalarValues_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IntegerTimeSeriesValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegerTimeSeriesValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IntegerTimeSeriesValues_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BooleanTimeSeriesEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<bool>(type: "boolean", nullable: false),
                    Start = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    End = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    TimeSeriesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooleanTimeSeriesEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BooleanTimeSeriesEntries_BooleanTimeSeriesValues_TimeSeries~",
                        column: x => x.TimeSeriesId,
                        principalTable: "BooleanTimeSeriesValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DecimalTimeSeriesEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false),
                    Start = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    End = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    TimeSeriesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DecimalTimeSeriesEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DecimalTimeSeriesEntries_DecimalTimeSeriesValues_TimeSeries~",
                        column: x => x.TimeSeriesId,
                        principalTable: "DecimalTimeSeriesValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IntegerTimeSeriesEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    Start = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    End = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    TimeSeriesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegerTimeSeriesEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IntegerTimeSeriesEntries_IntegerTimeSeriesValues_TimeSeries~",
                        column: x => x.TimeSeriesId,
                        principalTable: "IntegerTimeSeriesValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BooleanScalarValues_ProjectId",
                table: "BooleanScalarValues",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_BooleanTimeSeriesEntries_TimeSeriesId",
                table: "BooleanTimeSeriesEntries",
                column: "TimeSeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_BooleanTimeSeriesValues_ProjectId",
                table: "BooleanTimeSeriesValues",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_DecimalScalarValues_ProjectId",
                table: "DecimalScalarValues",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_DecimalTimeSeriesEntries_TimeSeriesId",
                table: "DecimalTimeSeriesEntries",
                column: "TimeSeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_DecimalTimeSeriesValues_ProjectId",
                table: "DecimalTimeSeriesValues",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_IntegerScalarValues_ProjectId",
                table: "IntegerScalarValues",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_IntegerTimeSeriesEntries_TimeSeriesId",
                table: "IntegerTimeSeriesEntries",
                column: "TimeSeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_IntegerTimeSeriesValues_ProjectId",
                table: "IntegerTimeSeriesValues",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BooleanScalarValues");

            migrationBuilder.DropTable(
                name: "BooleanTimeSeriesEntries");

            migrationBuilder.DropTable(
                name: "DecimalScalarValues");

            migrationBuilder.DropTable(
                name: "DecimalTimeSeriesEntries");

            migrationBuilder.DropTable(
                name: "IntegerScalarValues");

            migrationBuilder.DropTable(
                name: "IntegerTimeSeriesEntries");

            migrationBuilder.DropTable(
                name: "BooleanTimeSeriesValues");

            migrationBuilder.DropTable(
                name: "DecimalTimeSeriesValues");

            migrationBuilder.DropTable(
                name: "IntegerTimeSeriesValues");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
