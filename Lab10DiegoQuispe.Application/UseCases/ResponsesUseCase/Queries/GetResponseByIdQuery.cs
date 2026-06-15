using System;
using System.Threading;
using System.Threading.Tasks;
using Lab10DiegoQuispe.Application.Interfaces;
using Lab10DiegoQuispe.Persistence.Models;
using MediatR;

namespace Lab10DiegoQuispe.Application.UseCases.ResponsesUseCase.Queries;

public sealed record GetResponseByIdQuery : IRequest<Response?>
{
    public Guid ResponseId { get; set; }
}

internal sealed class GetResponseByIdHandler : IRequestHandler<GetResponseByIdQuery, Response?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetResponseByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<Response?> Handle(GetResponseByIdQuery request, CancellationToken cancellationToken)
    {
        return _unitOfWork.Repository<Response>().GetByIdAsync(request.ResponseId);
    }
}
