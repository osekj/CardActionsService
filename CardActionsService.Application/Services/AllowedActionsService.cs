using CardActionsService.Application.Exceptions;
using CardActionsService.Application.Interfaces;
using CardActionsService.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace CardActionsService.Application.Services
{
    public class AllowedActionsService : IAllowedActionsService
    {
        private readonly ILogger _logger;
        private readonly ICardService _cardService;
        private readonly ICardActionRulesService _actionRulesService;

        public AllowedActionsService(ILogger<AllowedActionsService> logger, ICardService cardService, ICardActionRulesService cardActionRulesService)
        {
            _logger = logger;
            _cardService = cardService;
            _actionRulesService = cardActionRulesService;
        }

        public async Task<IEnumerable<string>> GetAllowedActionsAsync(string userId, string cardNumber, CancellationToken cancellationToken)
        {
            var cardDetails = await _cardService.GetCardDetails(userId, cardNumber, cancellationToken);
            if(cardDetails is null)
            {
                _logger.LogWarning("Card with number {CardNumber} for user {UserId} was not found.", cardNumber, userId);
                throw new CardNotFoundException(userId, cardNumber);
            }
            _logger.LogDebug("Card details retrieved for {CardNumber}", cardNumber);

            var allowedActions = _actionRulesService.GetAllowedActions(cardDetails);
            return allowedActions;
        }
    }
}
