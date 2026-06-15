using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Lab10DiegoQuispe.Application.Interfaces;
using Lab10DiegoQuispe.Persistence.Models;
using MediatR;

namespace Lab10DiegoQuispe.Application.UseCases.TicketsUseCase.Queries;

public sealed record GetAllTicketsQuery : IRequest<IEnumerable<Ticket>>;

internal sealed class GetAllTicketsHandler : IRequestHandler<GetAllTicketsQuery, IEnumerable<Ticket>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllTicketsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<IEnumerable<Ticket>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken)
    {
        return _unitOfWork.Repository<Ticket>().GetAllAsync();
    }
}
