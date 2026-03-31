namespace EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Infrastructure.Migrations;

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable


/// <inheritdoc />
public partial class CreateSagaTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "Passes");

        migrationBuilder.CreateTable(
            name: "Sagas",
            schema: "Passes",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                Status = table.Column<int>(type: "integer", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Sagas", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder) => migrationBuilder.DropTable(
            name: "Sagas",
            schema: "Passes");
}
