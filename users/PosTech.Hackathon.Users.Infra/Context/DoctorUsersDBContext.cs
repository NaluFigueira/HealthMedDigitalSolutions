using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PosTech.Hackathon.Users.Domain.Entities;

namespace PosTech.Hackathon.Users.Infra.Context;
public class DoctorUserDbContext(DbContextOptions<DoctorUserDbContext> options) : IdentityDbContext<DoctorUser>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("DoctorUserSchema");
    }
}