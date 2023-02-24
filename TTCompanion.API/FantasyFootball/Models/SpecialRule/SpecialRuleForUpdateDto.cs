using System.ComponentModel.DataAnnotations;

namespace TTCompanion.API.FantasyFootball.Models.SpecialRule
{
    public class SpecialRuleForUpdateDto
    {
        [Required(ErrorMessage = "You should provide a name value.")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
    }
}
