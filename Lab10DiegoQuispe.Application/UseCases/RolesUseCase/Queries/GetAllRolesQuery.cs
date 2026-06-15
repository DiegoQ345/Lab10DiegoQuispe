using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Lab10DiegoQuispe.Application.Interfaces;
using Lab10DiegoQuispe.Persistence.Models;
using MediatR;

namespace Lab10DiegoQuispe.Application.UseCases.RolesUseCase.Queries;

public sealed record GetAllRolesQuery : IRequest<IEnumerable<Role>>;

internal sealed class GetAllRolesHandler : IRequestHandler<GetAllRolesQuery, IEnumerable<Role>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllRolesHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<IEnumerable<Role>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        return _unitOfWork.Repository<Role>().GetAllAsync();
    }
}
