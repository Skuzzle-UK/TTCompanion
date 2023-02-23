﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TTCompanion.API.FantasyFootball.Entities
{
    public class FFSpecialRule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [ForeignKey("RaceId")]
        public FFRace? Race { get; set; }
        public int RaceId { get; set; }


        public FFSpecialRule(string name)
        {
            Name = name;
        }
    }
}