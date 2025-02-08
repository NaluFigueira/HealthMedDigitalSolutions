using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PosTech.Hackathon.Appointments.Application.DTOs;

namespace PosTech.Hackathon.Appointments.Application.Interfaces.UseCases;
public interface IGetDoctorsUseCase : IUseCase<string, List<GetDoctorsDTO>>
{
}
