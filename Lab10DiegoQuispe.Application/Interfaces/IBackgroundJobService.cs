namespace Lab10DiegoQuispe.Application.Interfaces;

public interface IBackgroundJobService
{
    void NotifyTicketCreated(Guid ticketId);
}