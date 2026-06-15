using System;
using System.Threading;
using System.Threading.Tasks;
using Lab10DiegoQuispe.Application.Interfaces;
using Lab10DiegoQuispe.Persistence.Models;
using MediatR;

namespace Lab10DiegoQuispe.Application.UseCases.RolesUseCase.Queries;

public sealed record GetRoleByIdQuery : IRequest<Role?>
{
    public Guid RoleId { get; set; }
}

internal sealed class GetRoleByIdHandler : IRequestHandler<GetRoleByIdQuery, Role?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetRoleByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<Role?> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        return _unitOfWork.Repository<Role>().GetByIdAsync(request.RoleId);
    }
}
