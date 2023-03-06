using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TTCompanion.API.Models;
using System.ComponentModel;

namespace TTCompanion.API.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        [MaxLength(20)]
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        public DateTime RegistrationDateTime { get; set; }
        public DateTime LastRequestDateTime { get; set; }
        public int AccessTokens { get; set; }
        public PricePlans PricePlan { get; set; } = PricePlans.FREE;
    }
}