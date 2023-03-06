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
        [InlineData("password", "2023-03-06 16:35:24.5184729", "0uk6wJyPKGbax/lgsvBaHQ==")]
        public void HashPassword_MatchesHash(string password, string datetime, string expected)
        {
            string actual = Argon2Hashing.HashPassword(password, DateTime.Parse(datetime));
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("passw0rd", "2023-03-06 16:35:24.5184729", "0uk6wJyPKGbax/lgsvBaHQ==")]
        [InlineData("password", "2023-03-06 16:35:23.5184729", "0uk6wJyPKGbax/lgsvBaHQ==")] //Wrong DateTime
        [InlineData("Password", "2023-03-06 16:35:24.5184729", "0uk6wJyPKGbax/lgsvBaHQ==")]
        public void HashPassword_DoesNotMatchHash(string password, string datetime, string expected)
        {
            string actual = Argon2Hashing.HashPassword(password, DateTime.Parse(datetime));
            Assert.NotEqual(expected, actual);
        }
    }
}