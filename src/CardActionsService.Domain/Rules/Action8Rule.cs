using CardActionsService.Domain.Entities;
using CardActionsService.Domain.Enums;
using CardActionsService.Domain.Interfaces;

namespace CardActionsService.Domain.Rules
{
    public class Action8Rule : IActionRule
    {
        public string ActionName => "ACTION8";

        public bool IsApplicable(CardDetails card)
        {
            return card.CardStatus switch
            {
                CardStatus.Ordered
                or CardStatus.Inactive
                or CardStatus.Active
                or CardStatus.Blocked => true,
                _ => false
            };
        }
    }
}
