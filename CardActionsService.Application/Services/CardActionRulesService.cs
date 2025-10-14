using CardActionsService.Domain.Entities;
using CardActionsService.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace CardActionsService.Application.Services
{
    public class CardActionRulesService : ICardActionRulesService
    {
        private readonly ILogger _logger;
        private readonly IEnumerable<IActionRule> _rules;

        public CardActionRulesService(ILogger<CardActionRulesService> logger, IEnumerable<IActionRule> rules)
        {
            _logger = logger;
            _rules = rules;
        }

        public IEnumerable<string> GetAllowedActions(CardDetails cardDetails)
        {
            _logger.LogDebug("Evaluating rules for card {CardNumber} (Type: {CardType}, Status: {CardStatus}, PIN: {HasPin})",
                cardDetails.CardNumber, cardDetails.CardType, cardDetails.CardStatus, cardDetails.IsPinSet);

            var allowedActions = _rules.Where(rule => rule.IsApplicable(cardDetails))
                .Select(rule => rule.ActionName)
                .ToList();

            _logger.LogInformation("Card {CardNumber} qualified for {ActionCount} actions: {Actions}",
                cardDetails.CardNumber, allowedActions.Count, string.Join(", ", allowedActions));

            return allowedActions;
        }
    }
}
