using System;
using System.Threading;
using System.Threading.Tasks;
using Lab10DiegoQuispe.Application.Interfaces;
using Lab10DiegoQuispe.Persistence.Models;
using MediatR;

namespace Lab10DiegoQuispe.Application.UseCases.ResponsesUseCase.Commands;

public sealed record UpdateResponseCommand : IRequest<Response>
{
    public Guid ResponseId { get; set; }
    public Guid TicketId { get; set; }
    public Guid ResponderId { get; set; }
    public string Message { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }
}

internal sealed class UpdateResponseHandler : IRequestHandler<UpdateResponseCommand, Response>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateResponseHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Response> Handle(UpdateResponseCommand request, CancellationToken cancellationToken)
    {
        var response = new Response
        {
            ResponseId = request.ResponseId,
            TicketId = request.TicketId,
            ResponderId = request.ResponderId,
            Message = request.Message,
            CreatedAt = request.CreatedAt
        };

        _unitOfWork.Repository<Response>().Update(response);
        await _unitOfWork.SaveChangesAsync();
        return response;
    }
}
