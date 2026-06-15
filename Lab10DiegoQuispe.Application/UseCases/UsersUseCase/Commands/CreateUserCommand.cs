using System;
using System.Threading;
using System.Threading.Tasks;
using Lab10DiegoQuispe.Application.Interfaces;
using Lab10DiegoQuispe.Persistence.Models;
using MediatR;

namespace Lab10DiegoQuispe.Application.UseCases.UsersUseCase.Commands;

public sealed record CreateUserCommand : IRequest<User>
{
    public Guid? UserId { get; set; }
    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string? Email { get; set; }
}

internal sealed class CreateUserHandler : IRequestHandler<CreateUserCommand, User>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userId = request.UserId.HasValue && request.UserId.Value != Guid.Empty
            ? request.UserId.Value
            : Guid.NewGuid();

        var user = new User
        {
            UserId = userId,
            Username = request.Username,
            PasswordHash = request.PasswordHash,
            Email = request.Email
        };

        await _unitOfWork.Repository<User>().AddAsync(user);
        await _unitOfWork.SaveChangesAsync();
        return user;
    }
}
