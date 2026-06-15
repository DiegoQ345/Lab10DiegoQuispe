using System;
using System.Threading.Tasks;
using Lab10DiegoQuispe.Application.UseCases.RolesUseCase.Commands;
using Lab10DiegoQuispe.Application.UseCases.RolesUseCase.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab10DiegoQuispe.Controllers;

[ApiController]
[Authorize]
[Route("api/roles")]
public class RolesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RolesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var roles = await _mediator.Send(new GetAllRolesQuery());
        return Ok(roles);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var role = await _mediator.Send(new GetRoleByIdQuery { RoleId = id });
        if (role == null)
        {
            return NotFound();
        }

        return Ok(role);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoleCommand command)
    {
        var created = await _mediator.Send(command);
        return Ok(created);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateRoleCommand command)
    {
        var updated = await _mediator.Send(command);
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _mediator.Send(new DeleteRoleCommand { RoleId = id });
        return deleted ? Ok() : NotFound();
    }
}
