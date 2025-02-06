using FluentResults;

using PosTech.Hackathon.Appointments.Application.DTOs;
using PosTech.Hackathon.Appointments.Application.Interfaces.UseCases;
using PosTech.Hackathon.Appointments.Infra.DTO;
using PosTech.Hackathon.Appointments.Infra.Interfaces;
using PosTech.Hackathon.Appointments.Infra.Queues;

namespace PosTech.Hackathon.Appointments.Application.UseCases;

public class ScheduleAppointmentUseCase(
    IProducer producer,
    IPatientRepository patientRepository,
    IDoctorRepository doctorRepository)
    : IScheduleAppointmentUseCase
{
    public async Task<Result> ExecuteAsync(Guid patientId, ScheduleAppointmentDTO request)
    {
        if (request.Date <= DateTime.UtcNow)
            return Result.Fail("The appointment time must be in the future.");

        var patient = await patientRepository.GetByIdAsync(patientId);
        if (patient == null)
            return Result.Fail("Patient not found.");


        var doctor = await doctorRepository.GetByCrmAsync(request.CRM);
        if (doctor == null)
            return Result.Fail("Doctor not found.");

        var message = new AppointmentMessage
        {
            Id = Guid.NewGuid(),
            PatientId = patient.Id,
            PatientName = patient.Name,
            PatientEmail = patient.Email,
            DoctorId = doctor.Id,
            DoctorName = doctor.Name,
            DoctorEmail = doctor.Email,
            Date = request.Date
        };

        producer.PublishMessageOnQueue(message, AppointmentsQueues.Appointments);

        return Result.Ok();
    }
}