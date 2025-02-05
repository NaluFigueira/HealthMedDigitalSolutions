using Microsoft.AspNetCore.Identity;

namespace PosTech.Hackathon.Users.Application.Interfaces.Services;

public interface ITokenService
{
    string GenerateToken(IdentityUser user);
}
