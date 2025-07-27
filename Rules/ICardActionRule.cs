using MadiffTechnicalAssignment.Enums;
using MadiffTechnicalAssignment.Records;

namespace MadiffTechnicalAssignment.Rules
{
    public interface ICardActionRule
    {
        public bool IsApplicable(CardDetails cardDetails);
        IReadOnlyList<CardAction> GetAllowedActions(CardDetails cardDetails);
    }
}
