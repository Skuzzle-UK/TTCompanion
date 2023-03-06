using System.Security.Cryptography.X509Certificates;

namespace TTCompanion.API.Models
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime LastRequest { get; set; }
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
