using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Lab10DiegoQuispe.Application.Interfaces;
using Lab10DiegoQuispe.Persistence.Models;
using MediatR;

namespace Lab10DiegoQuispe.Application.UseCases.ResponsesUseCase.Queries;

public sealed record GetAllResponsesQuery : IRequest<IEnumerable<Response>>;

internal sealed class GetAllResponsesHandler : IRequestHandler<GetAllResponsesQuery, IEnumerable<Response>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllResponsesHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<IEnumerable<Response>> Handle(GetAllResponsesQuery request, CancellationToken cancellationToken)
    {
        return _unitOfWork.Repository<Response>().GetAllAsync();
    }
}
