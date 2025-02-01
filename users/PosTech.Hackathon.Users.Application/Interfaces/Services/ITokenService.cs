using System;
using PosTech.Hackathon.Users.Domain.Entities;

namespace PosTech.Hackathon.Users.Application.Interfaces.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}
