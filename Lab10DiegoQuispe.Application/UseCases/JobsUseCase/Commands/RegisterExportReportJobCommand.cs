using Hangfire;
using Lab10DiegoQuispe.Application.Interfaces;
using MediatR;

namespace Lab10DiegoQuispe.Application.UseCases.JobsUseCase.Commands;

public sealed record RegisterExportReportJobCommand : IRequest<string>;

internal sealed class RegisterExportReportJobHandler
    : IRequestHandler<RegisterExportReportJobCommand, string>
{
    public Task<string> Handle(
        RegisterExportReportJobCommand request,
        CancellationToken cancellationToken)
    {
        RecurringJob.AddOrUpdate<INotificationService>(
            "export-ticket-report",
            service => service.ExportTicketsReport(),
            Cron.Daily);

        return Task.FromResult(
            "Job de exportación registrado correctamente.");
    }
}