using System;
using System.Threading;
using System.Threading.Tasks;
using Lab10DiegoQuispe.Application.Interfaces;
using Lab10DiegoQuispe.Persistence.Models;
using MediatR;

namespace Lab10DiegoQuispe.Application.UseCases.ResponsesUseCase.Commands;

public sealed record DeleteResponseCommand : IRequest<bool>
{
    public Guid ResponseId { get; set; }
}

internal sealed class DeleteResponseHandler : IRequestHandler<DeleteResponseCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteResponseHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteResponseCommand request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.Repository<Response>().GetByIdAsync(request.ResponseId);
        if (response == null)
        {
            return false;
        }

        _unitOfWork.Repository<Response>().Delete(response);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
