using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CityInfo.APi.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenicationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AuthenicationController(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        }

        //use inside of the controller
        public class AuthenticateRequestBody
        {
            public string? UserName { get; set; }
            public string? Password { get; set; }   
        }

        private class CityUserInfo
        {
            public int UserId { get; set; }
            public string UserName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string City { get; set; }

            public CityUserInfo(
                int userId,
                string userName,
                string firstName,
                string lastName,
                string city)
            {
                UserId = userId;
                UserName = userName;
                FirstName = firstName;
                LastName = lastName;
                City = city;
            }
        }


        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(
            AuthenticateRequestBody authenticateRequestBody)
        {
            //step1: Validate the userName & Password

            var user = ValidateUserCredentials(
                authenticateRequestBody.UserName,
                authenticateRequestBody.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            //step 2 Create a token
            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));

            var signingCredentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256);

            //the claims that
            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.UserId.ToString()));
            claimsForToken.Add(new Claim("given_name", user.FirstName));
            claimsForToken.Add(new Claim("family_name", user.LastName));
            claimsForToken.Add(new Claim("city", user.City));

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials);

            var tokenToReturn = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);

        }

        private CityUserInfo ValidateUserCredentials(string? userName, string? password)
        {

            return new CityUserInfo(
                1,
                userName ?? "",
                "Olaide",
                "Adebanjo",
                "Nigeria");
        }
    }
}
