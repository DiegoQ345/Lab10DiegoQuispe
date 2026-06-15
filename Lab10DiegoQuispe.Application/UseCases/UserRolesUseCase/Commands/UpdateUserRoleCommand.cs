using System;
using System.Threading;
using System.Threading.Tasks;
using Lab10DiegoQuispe.Application.Interfaces;
using Lab10DiegoQuispe.Persistence.Models;
using MediatR;

namespace Lab10DiegoQuispe.Application.UseCases.UserRolesUseCase.Commands;

public sealed record UpdateUserRoleCommand : IRequest<UserRole>
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    public DateTime? AssignedAt { get; set; }
}

internal sealed class UpdateUserRoleHandler : IRequestHandler<UpdateUserRoleCommand, UserRole>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserRoleHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UserRole> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
    {
        var userRole = new UserRole
        {
            UserId = request.UserId,
            RoleId = request.RoleId,
            AssignedAt = request.AssignedAt
        };

        _unitOfWork.Repository<UserRole>().Update(userRole);
        await _unitOfWork.SaveChangesAsync();
        return userRole;
    }
}
