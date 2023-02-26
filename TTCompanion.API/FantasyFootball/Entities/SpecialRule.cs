using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TTCompanion.API.FantasyFootball.Entities
{
    public class SpecialRule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public bool Modifiable { get; set; } = true;
        
        public ICollection<Race> Races { get; } = new List<Race>();

        internal SpecialRule(int id, string name)
        {
            Id = id;
            Name = name;
            Modifiable = false;
        }
    }
}
