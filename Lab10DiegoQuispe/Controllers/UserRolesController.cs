using System;
using System.Threading.Tasks;
using Lab10DiegoQuispe.Application.UseCases.UserRolesUseCase.Commands;
using Lab10DiegoQuispe.Application.UseCases.UserRolesUseCase.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab10DiegoQuispe.Controllers;

[ApiController]
[Authorize]
[Route("api/user-roles")]
public class UserRolesController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserRolesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userRoles = await _mediator.Send(new GetAllUserRolesQuery());
        return Ok(userRoles);
    }

    [HttpGet("{userId:guid}/{roleId:guid}")]
    public async Task<IActionResult> GetById(Guid userId, Guid roleId)
    {
        var userRole = await _mediator.Send(new GetUserRoleByIdQuery { UserId = userId, RoleId = roleId });
        if (userRole == null)
        {
            return NotFound();
        }

        return Ok(userRole);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRoleCommand command)
    {
        var created = await _mediator.Send(command);
        return Ok(created);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUserRoleCommand command)
    {
        var updated = await _mediator.Send(command);
        return Ok(updated);
    }

    [HttpDelete("{userId:guid}/{roleId:guid}")]
    public async Task<IActionResult> Delete(Guid userId, Guid roleId)
    {
        var deleted = await _mediator.Send(new DeleteUserRoleCommand { UserId = userId, RoleId = roleId });
        return deleted ? Ok() : NotFound();
    }
}
