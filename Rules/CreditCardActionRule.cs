using MadiffTechnicalAssignment.Enums;
using MadiffTechnicalAssignment.Records;

namespace MadiffTechnicalAssignment.Rules
{
    public class CreditCardActionRule : ICardActionRule
    {
        public bool IsApplicable(CardDetails cardDetails)
        {
            return cardDetails.CardType == CardType.Credit;
        }

        public IReadOnlyList<CardAction> GetAllowedActions(CardDetails cardDetails)
        {
            return new List<CardAction> { CardAction.ACTION1, CardAction.ACTION2, CardAction.ACTION3, CardAction.ACTION4, CardAction.ACTION5, CardAction.ACTION6, CardAction.ACTION7,
                CardAction.ACTION8, CardAction.ACTION9, CardAction.ACTION10, CardAction.ACTION11, CardAction.ACTION12, CardAction.ACTION13 }.AsReadOnly();
        }
    }
}
