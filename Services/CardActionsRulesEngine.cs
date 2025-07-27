using MadiffTechnicalAssignment.Enums;
using MadiffTechnicalAssignment.Records;
using MadiffTechnicalAssignment.Rules;

namespace MadiffTechnicalAssignment.Services
{
    public class CardActionsRulesEngine : ICardActionsRulesEngine
    {
        private readonly IEnumerable<ICardActionRule> _rules;

        public CardActionsRulesEngine(IEnumerable<ICardActionRule> rules)
        {
            _rules = rules;
        }

        public IReadOnlyList<CardAction> GetAllowedActions(CardDetails cardDetails)
        {
            var applicableRules = _rules.Where(rule => rule.IsApplicable(cardDetails)).ToList();
            if (!applicableRules.Any())
            {
                return Array.Empty<CardAction>();
            }

            var allowedActions = new HashSet<CardAction>(_rules.First().GetAllowedActions(cardDetails));
            foreach(var rule in applicableRules.Skip(1))
            {
                allowedActions.IntersectWith(rule.GetAllowedActions(cardDetails));
            }

            return allowedActions.ToList().AsReadOnly();
        }
    }
}
