using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Lab10DiegoQuispe.Application.Interfaces;
using Lab10DiegoQuispe.Persistence.Models;
using MediatR;

namespace Lab10DiegoQuispe.Application.UseCases.UserRolesUseCase.Commands;

public sealed record DeleteUserRoleCommand : IRequest<bool>
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}

internal sealed class DeleteUserRoleHandler : IRequestHandler<DeleteUserRoleCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserRoleHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteUserRoleCommand request, CancellationToken cancellationToken)
    {
        var userRoles = await _unitOfWork.Repository<UserRole>().GetAllAsync();
        var userRole = userRoles.FirstOrDefault(ur => ur.UserId == request.UserId && ur.RoleId == request.RoleId);
        if (userRole == null)
        {
            return false;
        }

        _unitOfWork.Repository<UserRole>().Delete(userRole);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
