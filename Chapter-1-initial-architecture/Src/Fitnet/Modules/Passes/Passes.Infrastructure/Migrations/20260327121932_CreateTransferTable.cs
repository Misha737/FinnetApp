#nullable disable

namespace EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Infrastructure.Migrations;

using System;
using Microsoft.EntityFrameworkCore.Migrations;

/// <inheritdoc />
public partial class CreateTransferTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "Passes");

        migrationBuilder.CreateTable(
            name: "Passes",
            schema: "Passes",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                From = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                To = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Passes", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Transfers",
            schema: "Passes",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Type = table.Column<string>(type: "text", nullable: false),
                Payload = table.Column<string>(type: "text", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                ProcessedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Transfers", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Passes",
            schema: "Passes");

        migrationBuilder.DropTable(
            name: "Transfers",
            schema: "Passes");
    }
}
