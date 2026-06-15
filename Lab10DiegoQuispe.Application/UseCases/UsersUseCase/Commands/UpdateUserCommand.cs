using System;
using System.Threading;
using System.Threading.Tasks;
using Lab10DiegoQuispe.Application.Interfaces;
using Lab10DiegoQuispe.Persistence.Models;
using MediatR;

namespace Lab10DiegoQuispe.Application.UseCases.UsersUseCase.Commands;

public sealed record UpdateUserCommand : IRequest<User>
{
    public Guid UserId { get; set; }
    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string? Email { get; set; }
}

internal sealed class UpdateUserHandler : IRequestHandler<UpdateUserCommand, User>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            UserId = request.UserId,
            Username = request.Username,
            PasswordHash = request.PasswordHash,
            Email = request.Email
        };

        _unitOfWork.Repository<User>().Update(user);
        await _unitOfWork.SaveChangesAsync();
        return user;
    }
}
