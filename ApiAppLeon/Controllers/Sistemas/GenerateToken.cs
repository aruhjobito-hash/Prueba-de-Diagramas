using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiAppLeon.Controllers.Sistemas
{
    public class GenerateToken
    {
        public class JwtTokenRequest
        {
            public string User { get; set; }
            public List<JwtTokenProfile> JwtToken { get; set; }
        }

        public class JwtTokenProfile
        {
            public string jwtTokenProfile { get; set; }
            public string timeOfExpiration { get; set; }
        }

        public class JwtTokenModel
        {
            public string Mensaje { get; set; }
            public List<JwtToken> jwtToken { get; set; }
        }

        public class JwtToken
        {
            public string jwtTokenProfile { get; set; }
            public string timeOfExpiration { get; set; }
        }

        public JwtTokenModel GeneraToken(JwtTokenRequest request)
        {
            // Define the response model
            var jwtTokenModel = new JwtTokenModel
            {
                Mensaje = "Tokens generated successfully",
                jwtToken = new List<JwtToken>()
            };

            // Iterate over each profile and expiration time from the input
            foreach (var profileData in request.JwtToken)
            {
                // Parse the expiration time to DateTime
                if (!DateTime.TryParse(profileData.timeOfExpiration, out DateTime expirationTime))
                {
                    throw new ArgumentException($"Invalid expiration time format for profile: {profileData.jwtTokenProfile}");
                }

                // Create JWT token for the profile
                var jwtToken = CreateJwtToken(request.User, profileData.jwtTokenProfile, expirationTime);

                // Add the generated token and its expiration time to the response
                jwtTokenModel.jwtToken.Add(new JwtToken
                {
                    jwtTokenProfile = jwtToken,
                    timeOfExpiration = expirationTime.ToString("yyyy-MM-ddTHH:mm:ssZ")
                });
            }

            return jwtTokenModel;
        }

        private string CreateJwtToken(string user, string profile, DateTime expirationTime)
        {
            var jwtIssuer = "JWT:Issuer"; // Get from your configuration or environment
            var jwtKey = "JWT:Key"; // Get from your configuration or environment

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user),
        new Claim("Profile", profile)
    };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expirationTime,
                Issuer = jwtIssuer,
                Audience = jwtIssuer,
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
