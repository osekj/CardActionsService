using CardActionsService.Domain.Entities;
using CardActionsService.Domain.Enums;
using CardActionsService.Domain.Interfaces;

namespace CardActionsService.Domain.Rules
{
    public class Action5Rule : IActionRule
    {
        public string ActionName => "ACTION5";

        public bool IsApplicable(CardDetails card) => card.CardType == CardType.Credit;
    }
}
