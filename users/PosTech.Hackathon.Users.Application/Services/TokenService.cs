using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PosTech.Hackathon.Users.Application.Interfaces.Services;
using PosTech.Hackathon.Users.Domain.Entities;

namespace PosTech.Hackathon.Users.Application.Services;

public class TokenService : ITokenService
{
    public string GenerateToken(User user)
    {
        Claim[] claims =
        [
            new Claim("username", user.UserName),
            new Claim("id", user.Id),
            new Claim("loginTimestamp", DateTime.UtcNow.ToString())
        ];

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("c2d4a61141f0616bef9eac3c6cd539c454509dddfed9d0df54a6a17bfbe9172b"));

        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddMinutes(10),
            claims: claims,
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
