using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TTCompanion.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrations : Migration
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
                    RaceId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Races",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpecialRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    RaceId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialRules_Races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Races",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    PlayerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skills_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Races",
                columns: new[] { "Id", "ApothecaryAvailable", "CostOfApothercary", "CostOfAssistantCoach", "CostOfBloodweiserKeg", "CostOfBribes", "CostOfCheerleader", "CostOfDedicatedFan", "CostOfMasterChef", "CostOfReRolls", "MaxAssistantCoachs", "MaxBloodweiserKegs", "MaxBribes", "MaxCheerleaders", "MaxDedicatedFans", "MaxMasterChefs", "MaxReRolls", "Name" },
                values: new object[,]
                {
                    { 1, false, null, 0, 0, 0, 0, 0, 0, 70000, 0, 0, 0, 0, 0, 0, 0, "Shambling Undead" },
                    { 2, false, null, 0, 0, 0, 0, 0, 0, 60000, 0, 0, 0, 0, 0, 0, 0, "Snotling" }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "AG", "AV", "Cost", "MA", "Name", "PA", "RaceId", "ST" },
                values: new object[,]
                {
                    { 1, 3, 8, 75000, 7, "Ghoul Runner", 4, 1, 3 },
                    { 2, 5, 10, 125000, 3, "Mummy", null, 1, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_RaceId",
                table: "Players",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_PlayerId",
                table: "Skills",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialRules_RaceId",
                table: "SpecialRules",
                column: "RaceId");
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
