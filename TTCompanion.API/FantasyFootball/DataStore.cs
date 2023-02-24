using TTCompanion.API.FantasyFootball.Models.Player;
using TTCompanion.API.FantasyFootball.Models.Race;

namespace TTCompanion.API.FantasyFootball
{
    public class DataStore
    {
        public List<RaceDto> Races { get; set; }
        public static DataStore Instance { get; } = new DataStore();

        public DataStore()
        {
            Races = new List<RaceDto>()
            {
                new RaceDto()
                {
                    Id = 1,
                    Name = "Shambling Undead",
                    CostOfReRolls = 70000,
                    Players = new List<PlayerDto>()
                    {
                        new PlayerDto()
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
                        new PlayerDto()
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
                new RaceDto()
                {
                    Id = 2,
                    Name = "Snotling",
                    CostOfReRolls = 60000
                }
            };
        }
    }
}
