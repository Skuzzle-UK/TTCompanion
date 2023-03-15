using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TTCompanion.API.Models;
using TTCompanion.API.Services;
using TTCompanion.API.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TTCompanion.API.Controllers
{
    [Route("ttcompanion/api/v{version:apiVersion}/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public class AuthenticationRequestBody
        {
            public string? Username { get; set; }
            public string? Password { get; set; }
        }

        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuthenticationController(IConfiguration configuration, IUserRepository userRepository, IMapper mapper)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<string>> Authenticate(AuthenticationRequestBody authenticationRequestBody)
        {
            if(authenticationRequestBody == null || authenticationRequestBody.Username == null || authenticationRequestBody.Password == null)
            {
                return BadRequest("Username and password are required");
            }

            //Step 1: Validate the user/password
            var user = await ValidateUserCredentials(authenticationRequestBody.Username!, authenticationRequestBody.Password!);

            if (user == null)
            {
                return Unauthorized("No user with those credentials exists");
            }

            //Step 2: Create a token
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]!));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //The claims that
            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.Id.ToString()));
            claimsForToken.Add(new Claim("given_name", user.FirstName));
            claimsForToken.Add(new Claim("family_name", user.LastName));
            claimsForToken.Add(new Claim("email", user.EmailAddress));

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials);

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return Ok(tokenToReturn);
        }

        private async Task<UserDto?> ValidateUserCredentials(string username, string password)
        {
            var user = await _userRepository.GetUser(username, password);
            if (user == null)
            {
                return null;
            }

            string calculatedHash = Argon2Hashing.HashPassword(password, user.RegistrationDateTime);
            if (calculatedHash != user.PasswordHash)
            {
                return null;
            }

            return _mapper.Map<UserDto>(user);
        }
    }
}
