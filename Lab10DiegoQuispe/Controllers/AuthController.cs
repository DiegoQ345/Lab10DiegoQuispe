
using Lab10DiegoQuispe.Application.UseCases.AuthUseCase.Commands;
using Lab10DiegoQuispe.Application.UseCases.AuthUseCase.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab10DiegoQuispe.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterAuthCommand command)
    {
        var token = await _mediator.Send(command);
        return Ok(new { token });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginAuthQuery query)
    {
        var token = await _mediator.Send(query);
        return Ok(new { token });
    }
    
}
