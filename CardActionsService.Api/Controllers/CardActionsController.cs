using CardActionsService.Api.DTOs.Responses;
using CardActionsService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CardActionsService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardActionsController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IAllowedActionsService _cardActionsService;

        public CardActionsController(ILogger<CardActionsController> logger, IAllowedActionsService cardActionsService)
        {
            _logger = logger;
            _cardActionsService = cardActionsService;
        }

        [HttpGet("{userId}/{cardNumber}")]
        public async Task<ActionResult> GetAllowedActions([FromRoute] string userId, [FromRoute] string cardNumber, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Processing allowed actions request for user {UserId} and card {CardNumber}", userId, cardNumber);

            var allowedActions = await _cardActionsService.GetAllowedActionsAsync(
                userId,
                cardNumber,
                cancellationToken);

            var response = new GetAllowedActionsResponse
            {
                AllowedActions = allowedActions.ToList()
            };

            _logger.LogInformation("Returning {ActionCount} allowed actions for user {UserId} and card {CardNumber}",
                allowedActions.Count(), userId, cardNumber);

            return Ok(response);
        }
    }
}
