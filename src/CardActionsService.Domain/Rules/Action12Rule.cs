using CardActionsService.Domain.Entities;
using CardActionsService.Domain.Enums;
using CardActionsService.Domain.Interfaces;

namespace CardActionsService.Domain.Rules
{
    public class Action12Rule : IActionRule
    {
        public string ActionName => "ACTION12";

        public bool IsApplicable(CardDetails card)
        {
            return card.CardStatus switch
            {
                CardStatus.Ordered
                or CardStatus.Inactive
                or CardStatus.Active => true,
                _ => false
            };
        }
    }
}
