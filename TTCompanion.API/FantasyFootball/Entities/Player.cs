using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TTCompanion.API.FantasyFootball.Entities
{
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public int? MA { get; set; }
        public int? ST { get; set; }
        public int? AG { get; set; }
        public int? PA { get; set; }
        public int? AV { get; set; }
        public int? Cost { get; set; }
        public ICollection<Skill> Skills { get; set; } = new List<Skill>();
        public bool Modifiable { get; } = true;
        
        public ICollection<Race> Races { get; set; } = new List<Race>();


        internal Player(int id, string name)
        {
            Id = id;
            Name = name;
            Modifiable = false;
        }
    }
}
