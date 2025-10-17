using CardActionsService.Domain.Entities;

namespace CardActionsService.Domain.Interfaces
{
    public interface ICardActionRulesService
    {
        IEnumerable<string> GetAllowedActions(CardDetails cardDetails);
    }
}
