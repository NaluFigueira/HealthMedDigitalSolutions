using Microsoft.EntityFrameworkCore;

using PosTech.Hackathon.Appointments.Domain.Entities;
using PosTech.Hackathon.Appointments.Infra.Context;
using PosTech.Hackathon.Appointments.Infra.Interfaces;

namespace PosTech.Hackathon.Appointments.Infra.Repositories;
public class DoctorRepository(AppointmentsDBContext context) : IDoctorRepository
{
    public async Task<Doctor?> GetByCrmAsync(string crm)
    {
        return await context.Doctors
            .Where(d => d.CRM == crm)
            .FirstOrDefaultAsync();
    }
}

