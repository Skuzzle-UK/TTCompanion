﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TTCompanion.API.FantasyFootball.Entities
{
    public class FFSkill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [ForeignKey("PlayerId")]
        public FFPlayer? Player { get; set; }
        public int PlayerId { get; set; }

        public FFSkill(string name)
        {
            Name = name;
        }
    }
}