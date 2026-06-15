using Hangfire;
using Lab10DiegoQuispe.Application.Interfaces;

namespace Lab10DiegoQuispe.Infrastructure.Services;

public class NotificationService : INotificationService
{
    public void SendTicketCreatedNotification(Guid ticketId)
    {
        Console.WriteLine(
            $"[Hangfire] Ticket creado: {ticketId} - {DateTime.UtcNow}");
    }

    [AutomaticRetry(Attempts = 3)]
    public void SimulateFailure()
    {
        throw new Exception("Error simulado para Hangfire");
    }

    public void CleanupClosedTickets()
    {
        Console.WriteLine(
            $"Limpieza de tickets ejecutada - {DateTime.Now}");
    }

    public void ExportTicketsReport()
    {
        var fileName =
            $"Reporte_{DateTime.Now:yyyyMMddHHmmss}.txt";

        File.WriteAllText(
            fileName,
            $"Reporte generado el {DateTime.Now}");

        Console.WriteLine(
            $"Archivo exportado: {fileName}");
    }
}