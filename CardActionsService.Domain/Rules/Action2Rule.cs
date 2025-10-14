using CardActionsService.Domain.Entities;
using CardActionsService.Domain.Enums;
using CardActionsService.Domain.Interfaces;

namespace CardActionsService.Domain.Rules
{
    public class Action2Rule : IActionRule
    {
        public string ActionName => "ACTION2";

        public bool IsApplicable(CardDetails card) => card.CardStatus == CardStatus.Inactive;
    }
}
