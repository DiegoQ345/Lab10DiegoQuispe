using System;
using System.Threading;
using System.Threading.Tasks;
using Lab10DiegoQuispe.Application.Interfaces;
using Lab10DiegoQuispe.Persistence.Models;
using MediatR;

namespace Lab10DiegoQuispe.Application.UseCases.TicketsUseCase.Commands;

public sealed record CreateTicketCommand : IRequest<Ticket>
{
    public Guid? TicketId { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string Status { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }
    public DateTime? ClosedAt { get; set; }
}

internal sealed class CreateTicketHandler : IRequestHandler<CreateTicketCommand, Ticket>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBackgroundJobService _backgroundJobService;

    public CreateTicketHandler(
        IUnitOfWork unitOfWork,
        IBackgroundJobService backgroundJobService)
    {
        _unitOfWork = unitOfWork;
        _backgroundJobService = backgroundJobService;
    }

    public async Task<Ticket> Handle(
        CreateTicketCommand request,
        CancellationToken cancellationToken)
    {
        var ticketId =
            request.TicketId.HasValue &&
            request.TicketId.Value != Guid.Empty
                ? request.TicketId.Value
                : Guid.NewGuid();

        var ticket = new Ticket
        {
            TicketId = ticketId,
            UserId = request.UserId,
            Title = request.Title,
            Description = request.Description,
            Status = request.Status,
            CreatedAt = request.CreatedAt,
            ClosedAt = request.ClosedAt
        };

        await _unitOfWork.Repository<Ticket>()
            .AddAsync(ticket);

        await _unitOfWork.SaveChangesAsync();

        _backgroundJobService.NotifyTicketCreated(ticket.TicketId);

        return ticket;
    }
}
