using Hangfire;
using Lab10DiegoQuispe.Application.Interfaces;
using MediatR;

namespace Lab10DiegoQuispe.Application.UseCases.JobsUseCase.Commands;

public sealed record SimulateFailureCommand : IRequest<string>;

internal sealed class SimulateFailureHandler
    : IRequestHandler<SimulateFailureCommand, string>
{
    public Task<string> Handle(
        SimulateFailureCommand request,
        CancellationToken cancellationToken)
    {
        BackgroundJob.Enqueue<INotificationService>(
            service => service.SimulateFailure());

        return Task.FromResult(
            "Job de simulación de error registrado.");
    }
}