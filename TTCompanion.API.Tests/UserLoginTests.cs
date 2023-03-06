using TTCompanion.API.Utils;

namespace TTCompanion.API.Tests
{
    public class Argon2HashingTests
    {
        [Theory]
        [InlineData("2023 - 03 - 06 15:53:24.6767572", "48544748514750485051324953585351585052")]
        public void CreateSalt_ReturnsCorrectValue(string input, string expected)
        {
            DateTime RegistrationDateTime = DateTime.Parse(input);
            var byteArray = Argon2Hashing.CreateSalt(RegistrationDateTime);

            string actual = "";
            foreach(var b in byteArray)
            {
                actual += b;
            }

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("password", "2023 - 03 - 06 15:53:24.6767572", "214632487910611991161223601035532169126148")]
        public void HashPassword_MatchesHash(string password, string datetime, string expected)
        {
            string actual = Argon2Hashing.HashPassword(password, DateTime.Parse(datetime));
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("passw0rd", "2023 - 03 - 06 15:53:24.6767572", "214632487910611991161223601035532169126148")]
        [InlineData("password", "2023 - 03 - 06 15:53:23.6767572", "214632487910611991161223601035532169126148")]
        [InlineData("Password", "2023 - 03 - 06 15:53:24.6767572", "214632487910611991161223601035532169126148")]
        public void HashPassword_DoesNotMatchHash(string password, string datetime, string expected)
        {
            string actual = Argon2Hashing.HashPassword(password, DateTime.Parse(datetime));
            Assert.NotEqual(expected, actual);
        }
    }
}