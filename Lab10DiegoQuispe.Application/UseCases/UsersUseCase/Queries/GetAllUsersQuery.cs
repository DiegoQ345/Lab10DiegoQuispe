using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Lab10DiegoQuispe.Application.Interfaces;
using Lab10DiegoQuispe.Persistence.Models;
using MediatR;

namespace Lab10DiegoQuispe.Application.UseCases.UsersUseCase.Queries;

public sealed record GetAllUsersQuery : IRequest<IEnumerable<User>>;

internal sealed class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<User>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllUsersHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<IEnumerable<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        return _unitOfWork.Repository<User>().GetAllAsync();
    }
}
