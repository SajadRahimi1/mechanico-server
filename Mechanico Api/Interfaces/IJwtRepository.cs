using System.Security.Claims;
using Mechanico_Api.Models;

namespace Mechanico_Api.Interfaces;

public interface IJwtRepository
{
    public string generateUserJwt(JwtModel jwtModel);
    public JwtModel Authorize(ClaimsIdentity claimsIdentity);
}