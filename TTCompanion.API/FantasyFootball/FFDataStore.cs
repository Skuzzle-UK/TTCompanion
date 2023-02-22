using TTCompanion.API.FantasyFootball.Models;

namespace TTCompanion.API.FantasyFootball
{
    public class FFDataStore
    {
        public List<FFRaceDto> Races { get; set; }
        public static FFDataStore Instance { get; } = new FFDataStore();

        public FFDataStore()
        {
            Races = new List<FFRaceDto>()
            {
                new FFRaceDto()
                {
                    Id = 1,
                    Name = "Shambling Undead",
                    CostOfReRolls = 70000,
                    Players = new List<FFPlayerDto>()
                    {
                        new FFPlayerDto()
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
                        new FFPlayerDto()
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
                new FFRaceDto()
                {
                    Id = 2,
                    Name = "Snotling",
                    CostOfReRolls = 60000
                }
            };
        }
    }
}
