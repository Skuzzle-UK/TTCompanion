using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTCompanion.API.Migrations
{
    /// <inheritdoc />
    public partial class FFFixForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Races_CityId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Players_FFPlayerId",
                table: "Skills");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecialRules_Races_FFRaceId",
                table: "SpecialRules");

            migrationBuilder.DropIndex(
                name: "IX_SpecialRules_FFRaceId",
                table: "SpecialRules");

            migrationBuilder.DropIndex(
                name: "IX_Skills_FFPlayerId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Players_CityId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "FFRaceId",
                table: "SpecialRules");

            migrationBuilder.DropColumn(
                name: "FFPlayerId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Players");

            migrationBuilder.AddColumn<int>(
                name: "RaceId",
                table: "SpecialRules",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "Skills",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SpecialRules_RaceId",
                table: "SpecialRules",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_PlayerId",
                table: "Skills",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_RaceId",
                table: "Players",
                column: "RaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Races_RaceId",
                table: "Players",
                column: "RaceId",
                principalTable: "Races",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Players_PlayerId",
                table: "Skills",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialRules_Races_RaceId",
                table: "SpecialRules",
                column: "RaceId",
                principalTable: "Races",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Races_RaceId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Players_PlayerId",
                table: "Skills");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecialRules_Races_RaceId",
                table: "SpecialRules");

            migrationBuilder.DropIndex(
                name: "IX_SpecialRules_RaceId",
                table: "SpecialRules");

            migrationBuilder.DropIndex(
                name: "IX_Skills_PlayerId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Players_RaceId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "RaceId",
                table: "SpecialRules");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Skills");

            migrationBuilder.AddColumn<int>(
                name: "FFRaceId",
                table: "SpecialRules",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FFPlayerId",
                table: "Skills",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "Players",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1,
                column: "CityId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 2,
                column: "CityId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_SpecialRules_FFRaceId",
                table: "SpecialRules",
                column: "FFRaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_FFPlayerId",
                table: "Skills",
                column: "FFPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_CityId",
                table: "Players",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Races_CityId",
                table: "Players",
                column: "CityId",
                principalTable: "Races",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Players_FFPlayerId",
                table: "Skills",
                column: "FFPlayerId",
                principalTable: "Players",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialRules_Races_FFRaceId",
                table: "SpecialRules",
                column: "FFRaceId",
                principalTable: "Races",
                principalColumn: "Id");
        }
    }
}
