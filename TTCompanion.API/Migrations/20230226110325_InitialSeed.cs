using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TTCompanion.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    CanDelete = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

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
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CanDelete = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpecialRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    CanDelete = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerRace",
                columns: table => new
                {
                    PlayersId = table.Column<int>(type: "INTEGER", nullable: false),
                    RacesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerRace", x => new { x.PlayersId, x.RacesId });
                    table.ForeignKey(
                        name: "FK_PlayerRace_Players_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerRace_Races_RacesId",
                        column: x => x.RacesId,
                        principalTable: "Races",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerSkill",
                columns: table => new
                {
                    PlayersId = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerSkill", x => new { x.PlayersId, x.SkillsId });
                    table.ForeignKey(
                        name: "FK_PlayerSkill_Players_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerSkill_Skills_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RaceSpecialRule",
                columns: table => new
                {
                    RacesId = table.Column<int>(type: "INTEGER", nullable: false),
                    SpecialRulesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceSpecialRule", x => new { x.RacesId, x.SpecialRulesId });
                    table.ForeignKey(
                        name: "FK_RaceSpecialRule_Races_RacesId",
                        column: x => x.RacesId,
                        principalTable: "Races",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RaceSpecialRule_SpecialRules_SpecialRulesId",
                        column: x => x.SpecialRulesId,
                        principalTable: "SpecialRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "AG", "AV", "CanDelete", "Cost", "MA", "Name", "PA", "ST" },
                values: new object[,]
                {
                    { 1, 3, 8, false, 75000, 7, "Ghoul Runner", 4, 3 },
                    { 2, 5, 10, false, 125000, 3, "Mummy", null, 5 }
                });

            migrationBuilder.InsertData(
                table: "Races",
                columns: new[] { "Id", "CanDelete", "CostOfApothecary", "CostOfAssistantCoach", "CostOfBloodweiserKeg", "CostOfBribes", "CostOfCheerleader", "CostOfDedicatedFan", "CostOfMasterChef", "CostOfReRolls", "CostOfRiotousRookies", "MaxApothecarys", "MaxAssistantCoachs", "MaxBloodweiserKegs", "MaxBribes", "MaxCheerleaders", "MaxDedicatedFans", "MaxMasterChefs", "MaxReRolls", "MaxRiotousRookies", "Name" },
                values: new object[,]
                {
                    { 1, false, 50000, 10000, 50000, 100000, 10000, 10000, 300000, 70000, 100000, 0, 6, 2, 3, 12, 6, 1, 8, 0, "Shambling Undead" },
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
                table: "Skills",
                columns: new[] { "Id", "CanDelete", "Name" },
                values: new object[] { 1, true, "Dodge" });

            migrationBuilder.InsertData(
                table: "SpecialRules",
                columns: new[] { "Id", "CanDelete", "Description", "Name" },
                values: new object[] { 1, true, null, "Masters of Undeath" });

            migrationBuilder.InsertData(
                table: "PlayerRace",
                columns: new[] { "PlayersId", "RacesId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "PlayerSkill",
                columns: new[] { "PlayersId", "SkillsId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "RaceSpecialRule",
                columns: new[] { "RacesId", "SpecialRulesId" },
                values: new object[] { 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerRace_RacesId",
                table: "PlayerRace",
                column: "RacesId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerSkill_SkillsId",
                table: "PlayerSkill",
                column: "SkillsId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceSpecialRule_SpecialRulesId",
                table: "RaceSpecialRule",
                column: "SpecialRulesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerRace");

            migrationBuilder.DropTable(
                name: "PlayerSkill");

            migrationBuilder.DropTable(
                name: "RaceSpecialRule");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Races");

            migrationBuilder.DropTable(
                name: "SpecialRules");
        }
    }
}
