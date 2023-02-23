using System.ComponentModel.DataAnnotations;

namespace TTCompanion.API.FantasyFootball.Models
{
    public class FFPlayerForUpdateDto
    {
        [Required(ErrorMessage = "You should provide a name value.")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        public int MA { get; set; }
        public int ST { get; set; }
        public int AG { get; set; }
        public int? PA { get; set; }
        public int AV { get; set; }
        public int Cost { get; set; }
    }
}
