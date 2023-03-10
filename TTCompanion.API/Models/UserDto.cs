using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace TTCompanion.API.Models
{
    public class UserDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime RegistrationDateTime { get; set; }
        public DateTime LastRequestDateTime { get; set; }
        public int AccessTokens { get; set; }
        public PricePlans PricePlan { get; set; } = PricePlans.FREE;
    }

    public enum PricePlans
    {
        FREE,
        BASIC,
        ADVANCED,
        RESELLER,
        SUPERUSER
    }
}
