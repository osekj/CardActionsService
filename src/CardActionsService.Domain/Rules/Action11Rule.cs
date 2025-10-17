using CardActionsService.Domain.Entities;
using CardActionsService.Domain.Enums;
using CardActionsService.Domain.Interfaces;

namespace CardActionsService.Domain.Rules
{
    public class Action11Rule : IActionRule
    {
        public string ActionName => "ACTION11";

        public bool IsApplicable(CardDetails card)
        {
            return card.CardStatus switch
            {
                CardStatus.Inactive
                or CardStatus.Active => true,
                _ => false
            };
        }
    }
}
