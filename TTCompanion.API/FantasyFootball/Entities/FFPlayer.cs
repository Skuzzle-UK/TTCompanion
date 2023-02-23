using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TTCompanion.API.FantasyFootball.Entities
{
    public class FFPlayer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public int MA { get; set; }
        public int ST { get; set; }
        public int AG { get; set; }
        public int? PA { get; set; }
        public int AV { get; set; }
        public int Cost { get; set; }
        public ICollection<FFSkill> Skills { get; set; } = new List<FFSkill>();

        [ForeignKey("RaceId")]
        public FFRace? Race { get; set; }
        public int RaceId { get; set; }


        public FFPlayer(string name)
        {
            Name = name;
        }
    }
}
