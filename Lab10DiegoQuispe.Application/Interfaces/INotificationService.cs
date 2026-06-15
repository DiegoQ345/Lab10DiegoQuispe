namespace Lab10DiegoQuispe.Application.Interfaces;

public interface INotificationService
{
    void SendTicketCreatedNotification(Guid ticketId);
    
    void SimulateFailure();

    void CleanupClosedTickets();

    void ExportTicketsReport(); 
}