using System;
using System.Threading.Tasks;
using Lab10DiegoQuispe.Application.UseCases.ResponsesUseCase.Commands;
using Lab10DiegoQuispe.Application.UseCases.ResponsesUseCase.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab10DiegoQuispe.Controllers;

[ApiController]
[Authorize]
[Route("api/responses")]
public class ResponsesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ResponsesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var responses = await _mediator.Send(new GetAllResponsesQuery());
        return Ok(responses);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var response = await _mediator.Send(new GetResponseByIdQuery { ResponseId = id });
        if (response == null)
        {
            return NotFound();
        }

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateResponseCommand command)
    {
        var created = await _mediator.Send(command);
        return Ok(created);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateResponseCommand command)
    {
        var updated = await _mediator.Send(command);
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _mediator.Send(new DeleteResponseCommand { ResponseId = id });
        return deleted ? Ok() : NotFound();
    }
}
