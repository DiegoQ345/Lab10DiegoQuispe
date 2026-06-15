using Lab10DiegoQuispe.Persistence.Models;

namespace Lab10DiegoQuispe.Application.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(User user);
}