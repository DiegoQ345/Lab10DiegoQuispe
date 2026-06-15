using Lab10DiegoQuispe.Application.Interfaces;
using Lab10DiegoQuispe.Persistence.Models;
using MediatR;


namespace Lab10DiegoQuispe.Application.UseCases.AuthUseCase.Commands;

public sealed record RegisterAuthCommand : IRequest<string>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

internal sealed class RegisterAuthHandler : IRequestHandler<RegisterAuthCommand, string>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenService _jwt;

    public RegisterAuthHandler(IUnitOfWork unitOfWork, IJwtTokenService jwt)
    {
        _unitOfWork = unitOfWork;
        _jwt = jwt;
    }

    public async Task<string> Handle(RegisterAuthCommand request, CancellationToken cancellationToken)
    {
        var normalizedEmail = request.Email.Trim();

        var users = await _unitOfWork.Repository<User>().GetAllAsync();

        if (users.Any(u =>
                string.Equals(u.Email, normalizedEmail, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException("User already exists.");
        }

        var newUser = new User
        {
            UserId = Guid.NewGuid(),
            Email = normalizedEmail,
            Username = normalizedEmail,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        await _unitOfWork.Repository<User>().AddAsync(newUser);
        await _unitOfWork.SaveChangesAsync();

        return _jwt.GenerateToken(newUser);
    }
}
