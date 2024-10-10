using Freelancing_Website.Models.ForCreate;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class JWTService
{
    private readonly IConfiguration _config;
    private readonly SymmetricSecurityKey _jwtkey;
    public JWTService(IConfiguration config)
    {
        _config = config;
        _jwtkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _config["Authentication:SecretKey"]));
    }

    public string CreateJWT(UserForCreate user)
    {
        var userClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("user-name", user.UserName),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(ClaimTypes.Name, user.Name),
        };

        var credentials = new SigningCredentials(_jwtkey,
            SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(userClaims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = credentials,
            Issuer = _config["Authentication:Issuer"],
            Audience = _config["Authentication:Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var jwt = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(jwt);
    }

}
