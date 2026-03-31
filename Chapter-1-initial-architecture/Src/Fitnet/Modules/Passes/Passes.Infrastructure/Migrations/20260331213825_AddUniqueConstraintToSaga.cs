using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvolutionaryArchitecture.Fitnet.Modules.Passes.Passes.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintToSaga : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Sagas_CorrelationId",
                schema: "Passes",
                table: "Sagas",
                column: "CorrelationId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sagas_CorrelationId",
                schema: "Passes",
                table: "Sagas");
        }
    }
}
