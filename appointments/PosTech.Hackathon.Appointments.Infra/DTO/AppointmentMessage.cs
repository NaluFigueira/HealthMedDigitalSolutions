namespace PosTech.Hackathon.Appointments.Infra.DTO;
public class AppointmentMessage
{
    public Guid Id { get; set; }
    public string PatientId { get; set; }
    public string PatientName { get; set; }
    public string PatientEmail { get; set; }
    public string DoctorId { get; set; }
    public string DoctorName { get; set; }
    public string DoctorEmail { get; set; }
    public DateTime Date { get; set; }
}

