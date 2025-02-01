using FluentResults;

namespace PosTech.TechChallenge.Users.Application.Interfaces.UseCases;

public interface IUseCase<TRequest, TResponse>
{
    public Task<Result<TResponse>> ExecuteAsync(TRequest request);
}

public interface IUseCase<TRequest>
{
    public Task<Result> ExecuteAsync(TRequest request);
}