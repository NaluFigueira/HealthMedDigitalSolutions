using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PosTech.Hackathon.Users.Domain.Entities;

namespace PosTech.Hackathon.Users.Infra.Context;

public class PatientUsersDBContext(DbContextOptions<PatientUsersDBContext> options) : IdentityDbContext<PatientUser>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("PatientUserSchema");
    }
}
