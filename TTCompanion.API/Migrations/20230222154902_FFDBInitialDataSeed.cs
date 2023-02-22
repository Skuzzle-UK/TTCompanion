using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TTCompanion.API.Migrations
{
    /// <inheritdoc />
    public partial class FFDBInitialDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "AG", "AV", "CityId", "Cost", "MA", "Name", "PA", "RaceId", "ST" },
                values: new object[,]
                {
                    { 1, 3, 8, null, 75000, 7, "Ghoul Runner", 4, 1, 3 },
                    { 2, 5, 10, null, 125000, 3, "Mummy", null, 1, 5 }
                });

            migrationBuilder.InsertData(
                table: "Races",
                columns: new[] { "Id", "ApothecaryAvailable", "CostOfApothercary", "CostOfAssistantCoach", "CostOfBloodweiserKeg", "CostOfBribes", "CostOfCheerleader", "CostOfDedicatedFan", "CostOfMasterChef", "CostOfReRolls", "MaxAssistantCoachs", "MaxBloodweiserKegs", "MaxBribes", "MaxCheerleaders", "MaxDedicatedFans", "MaxMasterChefs", "MaxReRolls", "Name" },
                values: new object[,]
                {
                    { 1, false, null, 0, 0, 0, 0, 0, 0, 70000, 0, 0, 0, 0, 0, 0, 0, "Shambling Undead" },
                    { 2, false, null, 0, 0, 0, 0, 0, 0, 60000, 0, 0, 0, 0, 0, 0, 0, "Snotling" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Races",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Races",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
