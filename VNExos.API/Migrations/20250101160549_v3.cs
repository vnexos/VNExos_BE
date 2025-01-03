using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VNExos.API.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Translations_LanguageId",
                table: "Translations");

            migrationBuilder.CreateIndex(
                name: "IX_Translations_LanguageId_Origin",
                table: "Translations",
                columns: new[] { "LanguageId", "Origin" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Translations_LanguageId_Origin",
                table: "Translations");

            migrationBuilder.CreateIndex(
                name: "IX_Translations_LanguageId",
                table: "Translations",
                column: "LanguageId");
        }
    }
}
