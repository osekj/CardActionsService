using CardActionsService.Domain.Entities;

namespace CardActionsService.Domain.Interfaces
{
    public interface IActionRule
    {
        string ActionName { get; }
        bool IsApplicable(CardDetails card);
    }
}
