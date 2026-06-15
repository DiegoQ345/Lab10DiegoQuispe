using System;
using System.Threading.Tasks;
using Lab10DiegoQuispe.Application.UseCases.TicketsUseCase.Commands;
using Lab10DiegoQuispe.Application.UseCases.TicketsUseCase.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab10DiegoQuispe.Controllers;

[ApiController]
[Authorize]
[Route("api/tickets")]
public class TicketsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TicketsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tickets = await _mediator.Send(new GetAllTicketsQuery());
        return Ok(tickets);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var ticket = await _mediator.Send(new GetTicketByIdQuery { TicketId = id });
        if (ticket == null)
        {
            return NotFound();
        }

        return Ok(ticket);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTicketCommand command)
    {
        var created = await _mediator.Send(command);
        return Ok(created);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateTicketCommand command)
    {
        var updated = await _mediator.Send(command);
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _mediator.Send(new DeleteTicketCommand { TicketId = id });
        return deleted ? Ok() : NotFound();
    }
}
