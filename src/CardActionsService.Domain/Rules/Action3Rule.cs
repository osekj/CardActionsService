using CardActionsService.Domain.Entities;
using CardActionsService.Domain.Interfaces;

namespace CardActionsService.Domain.Rules
{
    public class Action3Rule : IActionRule
    {
        public string ActionName => "ACTION3";

        public bool IsApplicable(CardDetails card) => true;
    }
}
