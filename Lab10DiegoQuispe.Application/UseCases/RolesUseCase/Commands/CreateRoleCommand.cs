using System;
using System.Threading;
using System.Threading.Tasks;
using Lab10DiegoQuispe.Application.Interfaces;
using Lab10DiegoQuispe.Persistence.Models;
using MediatR;

namespace Lab10DiegoQuispe.Application.UseCases.RolesUseCase.Commands;

public sealed record CreateRoleCommand : IRequest<Role>
{
    public Guid? RoleId { get; set; }
    public string RoleName { get; set; } = null!;
}

internal sealed class CreateRoleHandler : IRequestHandler<CreateRoleCommand, Role>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoleHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Role> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var roleId = request.RoleId.HasValue && request.RoleId.Value != Guid.Empty
            ? request.RoleId.Value
            : Guid.NewGuid();

        var role = new Role
        {
            RoleId = roleId,
            RoleName = request.RoleName
        };

        await _unitOfWork.Repository<Role>().AddAsync(role);
        await _unitOfWork.SaveChangesAsync();
        return role;
    }
}
