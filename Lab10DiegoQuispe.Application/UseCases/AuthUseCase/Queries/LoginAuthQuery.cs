using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lab10DiegoQuispe.Application.Interfaces;
using Lab10DiegoQuispe.Persistence.Models;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Lab10DiegoQuispe.Application.UseCases.AuthUseCase.Queries;

public sealed record LoginAuthQuery : IRequest<string>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

internal sealed class LoginAuthHandler : IRequestHandler<LoginAuthQuery, string>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenService _jwt;

    public LoginAuthHandler(IUnitOfWork unitOfWork, IJwtTokenService jwt)
    {
        _unitOfWork = unitOfWork;
        _jwt = jwt;
    }

    public async Task<string> Handle(LoginAuthQuery request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.Repository<User>().GetAllAsync();

        var user = users.FirstOrDefault(u =>
            u.Email == request.Email);

        if (user is null)
            throw new InvalidOperationException("Invalid credentials.");

        var isValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

        if (!isValid)
            throw new InvalidOperationException("Invalid credentials.");

        return _jwt.GenerateToken(user);
    }
}
