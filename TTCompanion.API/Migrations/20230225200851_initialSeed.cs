using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TTCompanion.API.Migrations
{
    /// <inheritdoc />
    public partial class initialSeed : Migration
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
                    CostOfApothecary = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxApothecarys = table.Column<int>(type: "INTEGER", nullable: false),
                    CostOfBloodweiserKeg = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxBloodweiserKegs = table.Column<int>(type: "INTEGER", nullable: false),
                    CostOfMasterChef = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxMasterChefs = table.Column<int>(type: "INTEGER", nullable: false),
                    CostOfRiotousRookies = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxRiotousRookies = table.Column<int>(type: "INTEGER", nullable: false),
                    CanDelete = table.Column<bool>(type: "INTEGER", nullable: false)
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
                    MA = table.Column<int>(type: "INTEGER", nullable: true),
                    ST = table.Column<int>(type: "INTEGER", nullable: true),
                    AG = table.Column<int>(type: "INTEGER", nullable: true),
                    PA = table.Column<int>(type: "INTEGER", nullable: true),
                    AV = table.Column<int>(type: "INTEGER", nullable: true),
                    Cost = table.Column<int>(type: "INTEGER", nullable: true),
                    CanDelete = table.Column<bool>(type: "INTEGER", nullable: true),
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
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    CanDelete = table.Column<bool>(type: "INTEGER", nullable: false),
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
                    CanDelete = table.Column<bool>(type: "INTEGER", nullable: false),
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
                columns: new[] { "Id", "CanDelete", "CostOfApothecary", "CostOfAssistantCoach", "CostOfBloodweiserKeg", "CostOfBribes", "CostOfCheerleader", "CostOfDedicatedFan", "CostOfMasterChef", "CostOfReRolls", "CostOfRiotousRookies", "MaxApothecarys", "MaxAssistantCoachs", "MaxBloodweiserKegs", "MaxBribes", "MaxCheerleaders", "MaxDedicatedFans", "MaxMasterChefs", "MaxReRolls", "MaxRiotousRookies", "Name" },
                values: new object[,]
                {
                    { 1, false, 50000, 10000, 50000, 100000, 10000, 10000, 300000, 70000, 100000, 0, 6, 2, 3, 12, 6, 1, 8, 0, "Shambling Undead" },
                    { 2, false, 50000, 10000, 50000, 50000, 10000, 10000, 300000, 60000, 100000, 1, 6, 2, 3, 12, 6, 1, 8, 1, "Snotling" },
                    { 3, false, 50000, 10000, 50000, 100000, 10000, 10000, 300000, 60000, 100000, 1, 6, 2, 3, 12, 6, 1, 8, 0, "Amazon" },
                    { 4, false, 50000, 10000, 50000, 50000, 10000, 10000, 300000, 60000, 100000, 1, 6, 2, 3, 12, 6, 1, 8, 0, "Black Orc" },
                    { 5, false, 50000, 10000, 50000, 100000, 10000, 10000, 300000, 60000, 100000, 1, 6, 2, 3, 12, 6, 1, 8, 0, "Choas Chosen" },
                    { 6, false, 50000, 10000, 50000, 100000, 10000, 10000, 300000, 70000, 100000, 1, 6, 2, 3, 12, 6, 1, 8, 0, "Chaos Dwarf" },
                    { 7, false, 50000, 10000, 50000, 100000, 10000, 10000, 300000, 70000, 100000, 1, 6, 2, 3, 12, 6, 1, 8, 0, "Chaos Renegade" },
                    { 8, false, 50000, 10000, 50000, 100000, 10000, 10000, 300000, 70000, 100000, 1, 6, 2, 3, 12, 6, 1, 8, 0, "Daemons of Khorne" },
                    { 9, false, 50000, 10000, 50000, 100000, 10000, 10000, 300000, 50000, 100000, 1, 6, 2, 3, 12, 6, 1, 8, 0, "Dark Elf" },
                    { 10, false, 50000, 10000, 50000, 100000, 10000, 10000, 300000, 50000, 100000, 1, 6, 2, 3, 12, 6, 1, 8, 0, "Dwarf" },
                    { 11, false, 50000, 10000, 50000, 100000, 10000, 10000, 300000, 50000, 100000, 1, 6, 2, 3, 12, 6, 1, 8, 0, "Elven Union" },
                    { 12, false, 50000, 10000, 50000, 50000, 10000, 10000, 300000, 60000, 100000, 1, 6, 2, 3, 12, 6, 1, 8, 0, "Goblin" },
                    { 13, false, 50000, 10000, 50000, 100000, 10000, 10000, 100000, 60000, 100000, 1, 6, 2, 3, 12, 6, 1, 8, 0, "Halfling" },
                    { 14, false, 50000, 10000, 50000, 100000, 10000, 10000, 300000, 50000, 100000, 1, 6, 2, 3, 12, 6, 1, 8, 0, "High Elf" },
                    { 15, false, 50000, 10000, 50000, 100000, 10000, 10000, 300000, 50000, 100000, 1, 6, 2, 3, 12, 6, 1, 8, 0, "Human" },
                    { 16, false, 50000, 10000, 50000, 100000, 10000, 10000, 300000, 70000, 100000, 1, 6, 2, 3, 12, 6, 1, 8, 0, "Imperial Nobility" },
                    { 17, false, 50000, 10000, 50000, 100000, 10000, 10000, 300000, 60000, 100000, 1, 6, 2, 3, 12, 6, 1, 8, 0, "Khorne" },
                    { 18, false, 50000, 10000, 50000, 100000, 10000, 10000, 300000, 70000, 100000, 1, 6, 2, 3, 12, 6, 1, 8, 0, "Lizardmen" },
                    { 19, false, 50000, 10000, 50000, 100000, 10000, 10000, 300000, 70000, 100000, 0, 6, 2, 3, 12, 6, 1, 8, 0, "Necromantic Horror" },
                    { 20, false, 50000, 10000, 50000, 100000, 10000, 10000, 300000, 60000, 100000, 1, 6, 2, 3, 12, 6, 1, 8, 0, "Norse" },
                    { 21, false, 50000, 10000, 50000, 100000, 10000, 10000, 300000, 70000, 100000, 0, 6, 2, 3, 12, 6, 1, 8, 0, "Nurgle" },
                    { 22, false, 50000, 10000, 50000, 100000, 10000, 10000, 300000, 70000, 100000, 1, 6, 2, 3, 12, 6, 1, 8, 1, "Ogre" },
                    { 23, false, 50000, 10000, 50000, 100000, 10000, 10000, 300000, 70000, 100000, 1, 6, 2, 3, 12, 6, 1, 8, 0, "Old World Alliance" },
                    { 24, false, 50000, 10000, 50000, 100000, 10000, 10000, 300000, 60000, 100000, 1, 6, 2, 3, 12, 6, 1, 8, 0, "Orc" },
                    { 25, false, 50000, 10000, 50000, 100000, 10000, 10000, 300000, 50000, 100000, 1, 6, 2, 3, 12, 6, 1, 8, 0, "Skaven" },
                    { 26, false, 50000, 10000, 50000, 100000, 10000, 10000, 300000, 50000, 100000, 1, 6, 2, 3, 12, 6, 1, 8, 0, "Slann" },
                    { 27, false, 50000, 10000, 50000, 50000, 10000, 10000, 300000, 60000, 100000, 1, 6, 2, 3, 12, 6, 1, 8, 1, "Snotling" },
                    { 29, false, 50000, 10000, 50000, 100000, 10000, 10000, 300000, 70000, 100000, 0, 6, 2, 3, 12, 6, 1, 8, 0, "Tomb Kings" },
                    { 30, false, 50000, 10000, 50000, 50000, 10000, 10000, 300000, 70000, 100000, 1, 6, 2, 3, 12, 6, 1, 8, 0, "Underworld Denizens" },
                    { 31, false, 50000, 10000, 50000, 100000, 10000, 10000, 300000, 70000, 100000, 1, 6, 2, 3, 12, 6, 1, 8, 0, "Vampire" },
                    { 32, false, 50000, 10000, 50000, 100000, 10000, 10000, 300000, 60000, 100000, 1, 6, 2, 3, 12, 6, 1, 8, 0, "Wood Elf" }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "AG", "AV", "CanDelete", "Cost", "MA", "Name", "PA", "RaceId", "ST" },
                values: new object[,]
                {
                    { 1, 3, 8, false, 75000, 7, "Ghoul Runner", 4, 1, 3 },
                    { 2, 5, 10, false, 125000, 3, "Mummy", null, 1, 5 }
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
