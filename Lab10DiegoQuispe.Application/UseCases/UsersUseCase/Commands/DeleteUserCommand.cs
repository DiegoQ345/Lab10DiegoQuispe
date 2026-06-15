using System;
using System.Threading;
using System.Threading.Tasks;
using Lab10DiegoQuispe.Application.Interfaces;
using Lab10DiegoQuispe.Persistence.Models;
using MediatR;

namespace Lab10DiegoQuispe.Application.UseCases.UsersUseCase.Commands;

public sealed record DeleteUserCommand : IRequest<bool>
{
    public Guid UserId { get; set; }
}

internal sealed class DeleteUserHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Repository<User>().GetByIdAsync(request.UserId);
        if (user == null)
        {
            return false;
        }

        _unitOfWork.Repository<User>().Delete(user);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
