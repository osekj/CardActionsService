using CardActionsService.Domain.Entities;
using CardActionsService.Domain.Interfaces;

namespace CardActionsService.Domain.Rules
{
    public class Action9Rule : IActionRule
    {
        public string ActionName => "ACTION9";

        public bool IsApplicable(CardDetails card) => true;
    }
}
