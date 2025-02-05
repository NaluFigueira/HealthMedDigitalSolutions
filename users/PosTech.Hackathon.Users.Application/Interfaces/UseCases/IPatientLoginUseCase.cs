using System;
using PosTech.Hackathon.Users.Application.DTOs;
using PosTech.TechChallenge.Users.Application.Interfaces.UseCases;

namespace PosTech.Hackathon.Users.Application.Interfaces.UseCases;

public interface IPatientLoginUseCase : IUseCase<PatientLoginDTO, string>
{

}
