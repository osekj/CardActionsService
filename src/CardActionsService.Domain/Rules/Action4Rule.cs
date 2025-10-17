using CardActionsService.Domain.Entities;
using CardActionsService.Domain.Interfaces;

namespace CardActionsService.Domain.Rules
{
    public class Action4Rule : IActionRule
    {
        public string ActionName => "ACTION4";

        public bool IsApplicable(CardDetails card) => true;
    }
}
