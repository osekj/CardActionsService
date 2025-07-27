using MadiffTechnicalAssignment.Enums;
using MadiffTechnicalAssignment.Records;

namespace MadiffTechnicalAssignment.Rules
{
    public class BlockedCardActionRule : ICardActionRule
    {
        public bool IsApplicable(CardDetails cardDetails)
        {
            return cardDetails.CardStatus == CardStatus.Blocked;
        }

        public IReadOnlyList<CardAction> GetAllowedActions(CardDetails cardDetails)
        {
            var allowedActions = new List<CardAction>();
            allowedActions.AddRange([CardAction.ACTION3, CardAction.ACTION4, CardAction.ACTION5, CardAction.ACTION8, CardAction.ACTION9]);

            if (cardDetails.IsPinSet)
            {
                allowedActions.Add(CardAction.ACTION6);
                allowedActions.Add(CardAction.ACTION7);
            }

            return allowedActions.AsReadOnly();
        }
    }
}
