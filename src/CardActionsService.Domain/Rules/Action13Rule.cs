using CardActionsService.Domain.Entities;
using CardActionsService.Domain.Enums;
using CardActionsService.Domain.Interfaces;

namespace CardActionsService.Domain.Rules
{
    public class Action13Rule : IActionRule
    {
        public string ActionName => "ACTION13";

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
