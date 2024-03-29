﻿using TTCompanion.API.FantasyFootball.Models.Skill;

namespace TTCompanion.API.FantasyFootball.Models.Player
{
    public class PlayerOnlyDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int MA { get; set; }
        public int ST { get; set; }
        public int AG { get; set; }
        public int? PA { get; set; }
        public int AV { get; set; }
        public int Cost { get; set; }
    }
}
