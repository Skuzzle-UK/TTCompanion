namespace TTCompanion.API.FantasyFootball.Models
{
    public class FFTeam
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CostOfReRolls { get; set; }
        public ICollection<FFPlayer> Players { get; set; } = new List<FFPlayer>();
    }
}
