using CardActionsService.Domain.Enums;

namespace CardActionsService.Domain.Entities
{
    public record CardDetails(string CardNumber, CardType CardType, CardStatus CardStatus, bool IsPinSet)
    {

    }
}
