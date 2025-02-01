using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PosTech.Hackathon.Users.Domain.Entities;

namespace PosTech.Hackathon.Users.Infra.Context;
public class UserDbContext(DbContextOptions<UserDbContext> options) : IdentityDbContext<User>(options)
{
}