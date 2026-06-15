using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Lab10DiegoQuispe.Application.Interfaces;
using Lab10DiegoQuispe.Persistence.Models;
using MediatR;

namespace Lab10DiegoQuispe.Application.UseCases.UserRolesUseCase.Queries;

public sealed record GetAllUserRolesQuery : IRequest<IEnumerable<UserRole>>;

internal sealed class GetAllUserRolesHandler : IRequestHandler<GetAllUserRolesQuery, IEnumerable<UserRole>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllUserRolesHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<IEnumerable<UserRole>> Handle(GetAllUserRolesQuery request, CancellationToken cancellationToken)
    {
        return _unitOfWork.Repository<UserRole>().GetAllAsync();
    }
}
