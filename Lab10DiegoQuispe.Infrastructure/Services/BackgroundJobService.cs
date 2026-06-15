using Hangfire;
using Lab10DiegoQuispe.Application.Interfaces;

namespace Lab10DiegoQuispe.Infrastructure.Services;

public class BackgroundJobService : IBackgroundJobService
{
    public void NotifyTicketCreated(Guid ticketId)
    {
        BackgroundJob.Enqueue<INotificationService>(
            service => service.SendTicketCreatedNotification(ticketId));
    }
}