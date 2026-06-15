using Hangfire;
using Lab10DiegoQuispe.Application.Interfaces;
using MediatR;

namespace Lab10DiegoQuispe.Application.UseCases.JobsUseCase.Commands;

public sealed record RegisterCleanupJobCommand : IRequest<string>;

internal sealed class RegisterCleanupJobHandler
    : IRequestHandler<RegisterCleanupJobCommand, string>
{
    public Task<string> Handle(
        RegisterCleanupJobCommand request,
        CancellationToken cancellationToken)
    {
        RecurringJob.AddOrUpdate<INotificationService>(
            "cleanup-tickets",
            service => service.CleanupClosedTickets(),
            Cron.Daily);

        return Task.FromResult(
            "Job de limpieza registrado correctamente.");
    }
}