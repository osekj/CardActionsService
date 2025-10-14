using CardActionsService.Domain.Entities;
using CardActionsService.Domain.Enums;
using CardActionsService.Domain.Interfaces;

namespace CardActionsService.Domain.Rules
{
    public class Action6Rule : IActionRule
    {
        public string ActionName => "ACTION6";

        public bool IsApplicable(CardDetails card)
        {
            return card.CardStatus switch
            {
                CardStatus.Ordered
                or CardStatus.Inactive
                or CardStatus.Active
                or CardStatus.Blocked => card.IsPinSet,
                _ => false
            };
        }
    }
}
