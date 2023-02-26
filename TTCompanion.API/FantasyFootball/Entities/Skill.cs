using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TTCompanion.API.FantasyFootball.Entities
{
    public class Skill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public bool CanDelete { get; set; } = true;

        public ICollection<Player> Players { get; set; } = new List<Players>();

        public Skill(string name)
        {
            Name = name;
        }
    }
}
