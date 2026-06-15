using System;
using System.Threading;
using System.Threading.Tasks;
using Lab10DiegoQuispe.Application.Interfaces;
using Lab10DiegoQuispe.Persistence.Models;
using MediatR;

namespace Lab10DiegoQuispe.Application.UseCases.TicketsUseCase.Queries;

public sealed record GetTicketByIdQuery : IRequest<Ticket?>
{
    public Guid TicketId { get; set; }
}

internal sealed class GetTicketByIdHandler : IRequestHandler<GetTicketByIdQuery, Ticket?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTicketByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<Ticket?> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
    {
        return _unitOfWork.Repository<Ticket>().GetByIdAsync(request.TicketId);
    }
}
