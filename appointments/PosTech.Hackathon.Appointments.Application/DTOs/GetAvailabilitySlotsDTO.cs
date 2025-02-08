using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosTech.Hackathon.Appointments.Application.DTOs;
public class GetAvailabilitySlotsDTO
{
    public Guid Id { get; set; }
    public required string CRM { get; set; }

    public required double AppointmentValue { get; set; }
    public List<AvailabilitySlotDTO> AvailableSlots { get; set; } = new List<AvailabilitySlotDTO>();

}
