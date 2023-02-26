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
        [MaxLength(500)]
        public string? Description { get; set; }
        public bool CanDelete { get; set; } = true;
        
        public ICollection<Race> Races { get; set; } = new List<Race>();

        public SpecialRule(string name)
        {
            Name = name;
        }
    }
}
