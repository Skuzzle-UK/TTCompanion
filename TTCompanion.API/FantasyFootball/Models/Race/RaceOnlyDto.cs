namespace TTCompanion.API.FantasyFootball.Models.Race
{
    /// <summary>
    /// DTO model for a Race without special rule or player data
    /// </summary>
    public class RaceOnlyDto
    {
        /// <summary>
        /// The Id of the Race
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The name of the race
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// How much it costs for the race to purchase a reroll
        /// </summary>
        public int CostOfReRolls { get; set; } = 60000;
        /// <summary>
        /// Maximum number of rerolls that the race can have
        /// </summary>
        public int MaxReRolls { get; set; } = 8;
        /// <summary>
        /// How much it costs for the race to purchase a bribe
        /// </summary>
        public int CostOfBribes { get; set; } = 100000;
        public int MaxBribes { get; set; } = 3;
        public int CostOfCheerleader { get; set; } = 10000;
        public int MaxCheerleaders { get; set; } = 12;
        public int CostOfAssistantCoach { get; set; } = 10000;
        public int MaxAssistantCoachs { get; set; } = 6;
        public int CostOfDedicatedFan { get; set; } = 10000;
        public int MaxDedicatedFans { get; set; } = 6;
        public int CostOfApothecary { get; set; } = 50000;
        public int MaxApothecarys { get; set; } = 1;
        public int CostOfBloodweiserKeg { get; set; } = 50000;
        public int MaxBloodweiserKegs { get; set; } = 2;
        public int CostOfMasterChef { get; set; } = 300000;
        public int MaxMasterChefs { get; set; } = 1;
        public int CostOfRiotousRookies { get; set; } = 100000;
        public int MaxRiotousRookies { get; set; } = 0;
    }
}
