using TTCompanion.API.FantasyFootball.Models;

namespace TTCompanion.API.FantasyFootball
{
    public class FFDataStore
    {
        public List<FFTeam> Teams { get; set; }
        public static FFDataStore Instance { get; } = new FFDataStore();

        public FFDataStore()
        {
            Teams = new List<FFTeam>()
            {
                new FFTeam()
                {
                    Id = 1,
                    Name = "Shambling Undead",
                    CostOfReRolls = 70000,
                    Players = new List<FFPlayer>()
                    {
                        new FFPlayer()
                        {
                            Id = 1,
                            Name = "Ghoul Runner",
                            MA = 7,
                            ST = 3,
                            AG = 3,
                            PA = 4,
                            AV = 8,
                            Cost = 75000
                        },
                        new FFPlayer()
                        {
                            Id = 2,
                            Name = "Mummy",
                            MA = 3,
                            ST = 5,
                            AG = 5,
                            PA = null,
                            AV = 10,
                            Cost = 125000
                        }
                    }
                },
                new FFTeam()
                {
                    Id = 2,
                    Name = "Snotling",
                    CostOfReRolls = 60000
                }
            };
        }
    }
}
