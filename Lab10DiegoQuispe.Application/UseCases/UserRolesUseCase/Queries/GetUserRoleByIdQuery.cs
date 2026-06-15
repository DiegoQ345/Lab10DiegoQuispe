using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Lab10DiegoQuispe.Application.Interfaces;
using Lab10DiegoQuispe.Persistence.Models;
using MediatR;

namespace Lab10DiegoQuispe.Application.UseCases.UserRolesUseCase.Queries;

public sealed record GetUserRoleByIdQuery : IRequest<UserRole?>
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}

internal sealed class GetUserRoleByIdHandler : IRequestHandler<GetUserRoleByIdQuery, UserRole?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserRoleByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UserRole?> Handle(GetUserRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var userRoles = await _unitOfWork.Repository<UserRole>().GetAllAsync();
        return userRoles.FirstOrDefault(ur => ur.UserId == request.UserId && ur.RoleId == request.RoleId);
    }
}
