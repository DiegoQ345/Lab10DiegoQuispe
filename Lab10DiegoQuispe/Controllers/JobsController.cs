using Lab10DiegoQuispe.Application.UseCases.JobsUseCase.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lab10DiegoQuispe.Controllers;

[ApiController]
[Route("api/jobs")]
public class JobsController : ControllerBase
{
    private readonly IMediator _mediator;

    public JobsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("simulate-failure")]
    public async Task<IActionResult> SimulateFailure()
    {
        var result =
            await _mediator.Send(new SimulateFailureCommand());

        return Ok(result);
    }

    [HttpPost("cleanup")]
    public async Task<IActionResult> Cleanup()
    {
        var result =
            await _mediator.Send(new RegisterCleanupJobCommand());

        return Ok(result);
    }

    [HttpPost("export")]
    public async Task<IActionResult> Export()
    {
        var result =
            await _mediator.Send(new RegisterExportReportJobCommand());

        return Ok(result);
    }
}