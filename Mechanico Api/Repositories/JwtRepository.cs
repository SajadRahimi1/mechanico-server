using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Mechanico_Api.Interfaces;
using Mechanico_Api.Models;
using Microsoft.IdentityModel.Tokens;

namespace Mechanico_Api.Repositories;

public class JwtRepository : IJwtRepository
{
    private readonly IConfiguration _config;

    public JwtRepository(IConfiguration config)
    {
        _config = config;
    }

    public string generateUserJwt(JwtModel jwtModel)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.PrimarySid, jwtModel.Id),
            new Claim(ClaimTypes.Role, jwtModel.Role)
        };

        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Audience"], claims, expires: DateTime.Now.AddDays(2), signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    
    public JwtModel Authorize(ClaimsIdentity claimsIdentity)
    {
        var userClaims = claimsIdentity.Claims;
        return new JwtModel
        {
            Id = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid)?.Value ?? "",
            Role = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? "User",
        };
    }
}