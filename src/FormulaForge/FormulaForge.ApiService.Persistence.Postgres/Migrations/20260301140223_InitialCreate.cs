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
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BooleanScalarValueEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<bool>(type: "boolean", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooleanScalarValueEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BooleanScalarValueEntity_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BooleanTimeSeriesEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooleanTimeSeriesEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BooleanTimeSeriesEntity_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DecimalScalarValueEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DecimalScalarValueEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DecimalScalarValueEntity_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DecimalTimeSeriesEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DecimalTimeSeriesEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DecimalTimeSeriesEntity_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IntegerScalarValueEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegerScalarValueEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IntegerScalarValueEntity_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IntegerTimeSeriesEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegerTimeSeriesEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IntegerTimeSeriesEntity_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BooleanTimeSeriesEntry",
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
                    table.PrimaryKey("PK_BooleanTimeSeriesEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BooleanTimeSeriesEntry_BooleanTimeSeriesEntity_TimeSeriesId",
                        column: x => x.TimeSeriesId,
                        principalTable: "BooleanTimeSeriesEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DecimalTimeSeriesEntry",
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
                    table.PrimaryKey("PK_DecimalTimeSeriesEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DecimalTimeSeriesEntry_DecimalTimeSeriesEntity_TimeSeriesId",
                        column: x => x.TimeSeriesId,
                        principalTable: "DecimalTimeSeriesEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IntegerTimeSeriesEntry",
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
                    table.PrimaryKey("PK_IntegerTimeSeriesEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IntegerTimeSeriesEntry_IntegerTimeSeriesEntity_TimeSeriesId",
                        column: x => x.TimeSeriesId,
                        principalTable: "IntegerTimeSeriesEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BooleanScalarValueEntity_ProjectId",
                table: "BooleanScalarValueEntity",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_BooleanTimeSeriesEntity_ProjectId",
                table: "BooleanTimeSeriesEntity",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_BooleanTimeSeriesEntry_TimeSeriesId",
                table: "BooleanTimeSeriesEntry",
                column: "TimeSeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_DecimalScalarValueEntity_ProjectId",
                table: "DecimalScalarValueEntity",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_DecimalTimeSeriesEntity_ProjectId",
                table: "DecimalTimeSeriesEntity",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_DecimalTimeSeriesEntry_TimeSeriesId",
                table: "DecimalTimeSeriesEntry",
                column: "TimeSeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_IntegerScalarValueEntity_ProjectId",
                table: "IntegerScalarValueEntity",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_IntegerTimeSeriesEntity_ProjectId",
                table: "IntegerTimeSeriesEntity",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_IntegerTimeSeriesEntry_TimeSeriesId",
                table: "IntegerTimeSeriesEntry",
                column: "TimeSeriesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BooleanScalarValueEntity");

            migrationBuilder.DropTable(
                name: "BooleanTimeSeriesEntry");

            migrationBuilder.DropTable(
                name: "DecimalScalarValueEntity");

            migrationBuilder.DropTable(
                name: "DecimalTimeSeriesEntry");

            migrationBuilder.DropTable(
                name: "IntegerScalarValueEntity");

            migrationBuilder.DropTable(
                name: "IntegerTimeSeriesEntry");

            migrationBuilder.DropTable(
                name: "BooleanTimeSeriesEntity");

            migrationBuilder.DropTable(
                name: "DecimalTimeSeriesEntity");

            migrationBuilder.DropTable(
                name: "IntegerTimeSeriesEntity");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
