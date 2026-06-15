using System;
using System.Threading;
using System.Threading.Tasks;
using Lab10DiegoQuispe.Application.Interfaces;
using Lab10DiegoQuispe.Persistence.Models;
using MediatR;

namespace Lab10DiegoQuispe.Application.UseCases.ResponsesUseCase.Commands;

public sealed record CreateResponseCommand : IRequest<Response>
{
    public Guid? ResponseId { get; set; }
    public Guid TicketId { get; set; }
    public Guid ResponderId { get; set; }
    public string Message { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }
}

internal sealed class CreateResponseHandler : IRequestHandler<CreateResponseCommand, Response>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateResponseHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Response> Handle(CreateResponseCommand request, CancellationToken cancellationToken)
    {
        var responseId = request.ResponseId.HasValue && request.ResponseId.Value != Guid.Empty
            ? request.ResponseId.Value
            : Guid.NewGuid();

        var response = new Response
        {
            ResponseId = responseId,
            TicketId = request.TicketId,
            ResponderId = request.ResponderId,
            Message = request.Message,
            CreatedAt = request.CreatedAt
        };

        await _unitOfWork.Repository<Response>().AddAsync(response);
        await _unitOfWork.SaveChangesAsync();
        return response;
    }
}
