using PosTech.Hackathon.Appointments.Application.DTOs;

namespace PosTech.Hackathon.Appointments.Application.Interfaces.UseCases;

public interface IGetAppointmentsUseCase : IUseCase<GetAppointmentsDTO, List<AppointmentDTO>>
{
}