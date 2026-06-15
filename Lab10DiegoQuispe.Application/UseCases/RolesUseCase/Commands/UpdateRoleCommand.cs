using System;
using System.Threading;
using System.Threading.Tasks;
using Lab10DiegoQuispe.Application.Interfaces;
using Lab10DiegoQuispe.Persistence.Models;
using MediatR;

namespace Lab10DiegoQuispe.Application.UseCases.RolesUseCase.Commands;

public sealed record UpdateRoleCommand : IRequest<Role>
{
    public Guid RoleId { get; set; }
    public string RoleName { get; set; } = null!;
}

internal sealed class UpdateRoleHandler : IRequestHandler<UpdateRoleCommand, Role>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateRoleHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Role> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = new Role
        {
            RoleId = request.RoleId,
            RoleName = request.RoleName
        };

        _unitOfWork.Repository<Role>().Update(role);
        await _unitOfWork.SaveChangesAsync();
        return role;
    }
}
