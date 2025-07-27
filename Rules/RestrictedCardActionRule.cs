using MadiffTechnicalAssignment.Enums;
using MadiffTechnicalAssignment.Records;

namespace MadiffTechnicalAssignment.Rules
{
    public class RestrictedCardActionRule : ICardActionRule
    {
        public bool IsApplicable(CardDetails cardDetails)
        {
            return cardDetails.CardStatus == CardStatus.Restricted;
        }

        public IReadOnlyList<CardAction> GetAllowedActions(CardDetails cardDetails)
        {
            var allowedActions = new List<CardAction>();
            allowedActions.AddRange([CardAction.ACTION3, CardAction.ACTION4, CardAction.ACTION5, CardAction.ACTION9]);

            return allowedActions.AsReadOnly();
        }
    }
}
