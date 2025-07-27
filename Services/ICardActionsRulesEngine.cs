using MadiffTechnicalAssignment.Enums;
using MadiffTechnicalAssignment.Records;
using MadiffTechnicalAssignment.Rules;

namespace MadiffTechnicalAssignment.Services
{
    public interface ICardActionsRulesEngine
    {
        public IReadOnlyList<CardAction> GetAllowedActions(CardDetails cardDetails);
    }
}
