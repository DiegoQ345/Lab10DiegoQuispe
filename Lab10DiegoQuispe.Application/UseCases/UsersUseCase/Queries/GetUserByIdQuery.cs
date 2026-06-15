using System;
using System.Threading;
using System.Threading.Tasks;
using Lab10DiegoQuispe.Application.Interfaces;
using Lab10DiegoQuispe.Persistence.Models;
using MediatR;

namespace Lab10DiegoQuispe.Application.UseCases.UsersUseCase.Queries;

public sealed record GetUserByIdQuery : IRequest<User?>
{
    public Guid UserId { get; set; }
}

internal sealed class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, User?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<User?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        return _unitOfWork.Repository<User>().GetByIdAsync(request.UserId);
    }
}
