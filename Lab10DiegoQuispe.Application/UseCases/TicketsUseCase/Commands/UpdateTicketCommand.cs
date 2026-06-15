using System;
using System.Threading;
using System.Threading.Tasks;
using Lab10DiegoQuispe.Application.Interfaces;
using Lab10DiegoQuispe.Persistence.Models;
using MediatR;

namespace Lab10DiegoQuispe.Application.UseCases.TicketsUseCase.Commands;

public sealed record UpdateTicketCommand : IRequest<Ticket>
{
    public Guid TicketId { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public string Status { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }
    public DateTime? ClosedAt { get; set; }
}

internal sealed class UpdateTicketHandler : IRequestHandler<UpdateTicketCommand, Ticket>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTicketHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Ticket> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = new Ticket
        {
            TicketId = request.TicketId,
            UserId = request.UserId,
            Title = request.Title,
            Description = request.Description,
            Status = request.Status,
            CreatedAt = request.CreatedAt,
            ClosedAt = request.ClosedAt
        };

        _unitOfWork.Repository<Ticket>().Update(ticket);
        await _unitOfWork.SaveChangesAsync();
        return ticket;
    }
}
