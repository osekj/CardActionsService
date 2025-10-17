using CardActionsService.Domain.Entities;
using CardActionsService.Domain.Enums;
using CardActionsService.Domain.Interfaces;

namespace CardActionsService.Domain.Rules
{
    public class Action7Rule : IActionRule
    {
        public string ActionName => "ACTION7";

        public bool IsApplicable(CardDetails card)
        {
            return card.CardStatus switch
            {
                CardStatus.Ordered
                or CardStatus.Inactive
                or CardStatus.Active => !card.IsPinSet,
                CardStatus.Blocked => card.IsPinSet,
                _ => false
            };
        }
    }
}
