using System;
using System.Threading;
using System.Threading.Tasks;
using Lab10DiegoQuispe.Application.Interfaces;
using Lab10DiegoQuispe.Persistence.Models;
using MediatR;

namespace Lab10DiegoQuispe.Application.UseCases.RolesUseCase.Commands;

public sealed record DeleteRoleCommand : IRequest<bool>
{
    public Guid RoleId { get; set; }
}

internal sealed class DeleteRoleHandler : IRequestHandler<DeleteRoleCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRoleHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _unitOfWork.Repository<Role>().GetByIdAsync(request.RoleId);
        if (role == null)
        {
            return false;
        }

        _unitOfWork.Repository<Role>().Delete(role);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
