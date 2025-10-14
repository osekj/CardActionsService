using CardActionsService.Domain.Entities;
using CardActionsService.Domain.Enums;
using CardActionsService.Domain.Interfaces;

namespace CardActionsService.Domain.Rules
{
    public class Action1Rule : IActionRule
    {
        public string ActionName => "ACTION1";

        public bool IsApplicable(CardDetails card) => card.CardStatus == CardStatus.Active;
    }
}
