using FluentValidation;

using PosTech.Hackathon.Appointments.Application.DTOs;

namespace PosTech.Hackathon.Appointments.Application.Validators;

public class AddAvailabilitySlotsDTOValidator : AbstractValidator<AddAvailabilitySlotsDTO>
{
    public AddAvailabilitySlotsDTOValidator()
    {
        RuleFor(dto => dto.DoctorId)
            .NotEmpty()
            .WithMessage("DoctorId is required.");

        RuleFor(dto => dto.AvailabilitySlots)
            .NotEmpty()
            .WithMessage("AvailabilitySlots are required.")
            .Must(HaveNoOverlappingSlots)
            .WithMessage("Availability slots must be at least 1 hour apart.")
            .Must(HaveValidTimeSlots)
            .WithMessage("Availability slots must be on the hour or half-hour (e.g., 13:00, 13:30).");
    }

    private bool HaveNoOverlappingSlots(List<AvailabilitySlotDTO> slots)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            for (int j = i + 1; j < slots.Count; j++)
            {
                if (Math.Abs((slots[i].Slot - slots[j].Slot).TotalHours) < 1)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private bool HaveValidTimeSlots(List<AvailabilitySlotDTO> slots)
    {
        foreach (var slot in slots)
        {
            if (slot.Slot.Minute % 30 != 0)
            {
                return false;
            }
        }
        return true;
    }
}
