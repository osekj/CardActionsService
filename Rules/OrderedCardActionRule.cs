using MadiffTechnicalAssignment.Enums;
using MadiffTechnicalAssignment.Records;

namespace MadiffTechnicalAssignment.Rules
{
    public class OrderedCardActionRule : ICardActionRule
    {
        public bool IsApplicable(CardDetails cardDetails)
        {
            return cardDetails.CardStatus == CardStatus.Ordered;
        }

        public IReadOnlyList<CardAction> GetAllowedActions(CardDetails cardDetails)
        {
            var allowedActions = new List<CardAction>();
            allowedActions.AddRange( [CardAction.ACTION3, CardAction.ACTION4, CardAction.ACTION5, CardAction.ACTION8, CardAction.ACTION9, CardAction.ACTION10,
                CardAction.ACTION12, CardAction.ACTION13]);
            
            if(cardDetails.IsPinSet)
            {
                allowedActions.Add(CardAction.ACTION6);
            }
            else
            {
                allowedActions.Add(CardAction.ACTION7);
            }

            return allowedActions.AsReadOnly();
        }
    }
}
