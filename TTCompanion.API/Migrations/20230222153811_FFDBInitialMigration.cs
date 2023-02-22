using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTCompanion.API.Migrations
{
    /// <inheritdoc />
    public partial class FFDBInitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Races",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CostOfReRolls = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxReRolls = table.Column<int>(type: "INTEGER", nullable: false),
                    CostOfBribes = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxBribes = table.Column<int>(type: "INTEGER", nullable: false),
                    CostOfCheerleader = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxCheerleaders = table.Column<int>(type: "INTEGER", nullable: false),
                    CostOfAssistantCoach = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxAssistantCoachs = table.Column<int>(type: "INTEGER", nullable: false),
                    CostOfDedicatedFan = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxDedicatedFans = table.Column<int>(type: "INTEGER", nullable: false),
                    ApothecaryAvailable = table.Column<bool>(type: "INTEGER", nullable: false),
                    CostOfApothercary = table.Column<int>(type: "INTEGER", nullable: true),
                    CostOfBloodweiserKeg = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxBloodweiserKegs = table.Column<int>(type: "INTEGER", nullable: false),
                    CostOfMasterChef = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxMasterChefs = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Races", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    MA = table.Column<int>(type: "INTEGER", nullable: false),
                    ST = table.Column<int>(type: "INTEGER", nullable: false),
                    AG = table.Column<int>(type: "INTEGER", nullable: false),
                    PA = table.Column<int>(type: "INTEGER", nullable: true),
                    AV = table.Column<int>(type: "INTEGER", nullable: false),
                    Cost = table.Column<int>(type: "INTEGER", nullable: false),
                    CityId = table.Column<int>(type: "INTEGER", nullable: true),
                    RaceId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Races_CityId",
                        column: x => x.CityId,
                        principalTable: "Races",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SpecialRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    FFRaceId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialRules_Races_FFRaceId",
                        column: x => x.FFRaceId,
                        principalTable: "Races",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    FFPlayerId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skills_Players_FFPlayerId",
                        column: x => x.FFPlayerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_CityId",
                table: "Players",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_FFPlayerId",
                table: "Skills",
                column: "FFPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialRules_FFRaceId",
                table: "SpecialRules",
                column: "FFRaceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "SpecialRules");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Races");
        }
    }
}
