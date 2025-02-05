using PosTech.Hackathon.Appointments.Domain.Entities;

namespace PosTech.Hackathon.Appointments.Infra.Interfaces;
public interface IDoctorRepository
{
    Task<Doctor?> GetByCrmAsync(string crm);
}
