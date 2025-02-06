using PosTech.Hackathon.Appointments.Domain.Entities;

namespace PosTech.Hackathon.Appointments.Infra.Interfaces;
public interface IPatientRepository
{
    Task<Patient> GetByIdAsync(Guid patientId);
}
