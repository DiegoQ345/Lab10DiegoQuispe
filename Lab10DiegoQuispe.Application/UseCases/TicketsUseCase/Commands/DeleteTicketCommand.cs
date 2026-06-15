using System;
using System.Threading;
using System.Threading.Tasks;
using Lab10DiegoQuispe.Application.Interfaces;
using Lab10DiegoQuispe.Persistence.Models;
using MediatR;

namespace Lab10DiegoQuispe.Application.UseCases.TicketsUseCase.Commands;

public sealed record DeleteTicketCommand : IRequest<bool>
{
    public Guid TicketId { get; set; }
}

internal sealed class DeleteTicketHandler : IRequestHandler<DeleteTicketCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTicketHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _unitOfWork.Repository<Ticket>().GetByIdAsync(request.TicketId);
        if (ticket == null)
        {
            return false;
        }

        _unitOfWork.Repository<Ticket>().Delete(ticket);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
