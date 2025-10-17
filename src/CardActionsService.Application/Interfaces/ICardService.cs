using CardActionsService.Domain.Entities;

namespace CardActionsService.Application.Interfaces
{
    public interface ICardService
    {
        Task<CardDetails?> GetCardDetails(string userId, string cardNumber, CancellationToken cancellationToken);
    }
}
