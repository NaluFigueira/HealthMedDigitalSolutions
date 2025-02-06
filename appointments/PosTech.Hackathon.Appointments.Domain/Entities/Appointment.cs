namespace PosTech.Hackathon.Appointments.Domain.Entities;
public class Appointment
{
    public required Guid Id { get; set; }
    public required string DoctorId { get; set; }
    public required string PatientId { get; set; }
    public required DateTime Date { get; set; }
    public bool DoctorConfirmationPending { get; set; }
    public bool Rejected { get; set; }
    public string? RejectedBy { get; set; }
    public string? RejectionJustification { get; set; }

    public Doctor? Doctor { get; set; }
    public Patient? Patient { get; set; }
}