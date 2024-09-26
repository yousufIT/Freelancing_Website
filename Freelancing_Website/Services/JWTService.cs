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
            _config["Authentivation:SecretKey"]));
    }

    public string CreateJWT(UserForCreate user)
    {
        var userClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(ClaimTypes.Name, user.Name),
        };

        if (user is FreelancerForCreate freelancer)
        {
            userClaims.Add(new Claim("Hourlysalary", freelancer.Hourlysalary.ToString()));
            // إضافة المزيد من الخصائص هنا حسب الحاجة
        }
        else if (user is ClientForCreate client)
        {
            userClaims.Add(new Claim("CompanyName", client.CompanyName));
            // إضافة المزيد من الخصائص هنا حسب الحاجة
        }

        var credentials = new SigningCredentials(_jwtkey,
            SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(userClaims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = credentials,
            Issuer = _config["Authentivation:Issuer"],
            Audience = _config["Authentivation:Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var jwt = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(jwt);
    }

}
