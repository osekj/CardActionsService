using MadiffTechnicalAssignment.Enums;
using MadiffTechnicalAssignment.Records;

namespace MadiffTechnicalAssignment.Services
{
    public interface ICardService
    {
        Task<CardDetails?> GetCardDetails(string userId, string cardNumber);
        IReadOnlyList<CardAction> GetAllowedCardActionsAsync(CardDetails cardDetails);
    }
}