using PosTech.Hackathon.Appointments.Domain.Entities;

namespace PosTech.Hackathon.Appointments.Infra.Interfaces;

public interface IAppointmentRepository
{
    Task<Appointment?> GetAppointmentAsync(Guid appointmentId, Guid patientId);
    Task RemoveAppointmentAsync(Appointment appointment);
    Task<List<Appointment>> GetAppointmentsByPatientIdAsync(Guid patientId);
}
