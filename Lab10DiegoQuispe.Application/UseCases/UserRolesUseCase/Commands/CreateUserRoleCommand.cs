using System;
using System.Threading;
using System.Threading.Tasks;
using Lab10DiegoQuispe.Application.Interfaces;
using Lab10DiegoQuispe.Persistence.Models;
using MediatR;

namespace Lab10DiegoQuispe.Application.UseCases.UserRolesUseCase.Commands;

public sealed record CreateUserRoleCommand : IRequest<UserRole>
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    public DateTime? AssignedAt { get; set; }
}

internal sealed class CreateUserRoleHandler : IRequestHandler<CreateUserRoleCommand, UserRole>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserRoleHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UserRole> Handle(CreateUserRoleCommand request, CancellationToken cancellationToken)
    {
        var userRole = new UserRole
        {
            UserId = request.UserId,
            RoleId = request.RoleId,
            AssignedAt = request.AssignedAt
        };

        await _unitOfWork.Repository<UserRole>().AddAsync(userRole);
        await _unitOfWork.SaveChangesAsync();
        return userRole;
    }
}
