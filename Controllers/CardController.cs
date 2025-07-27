using MadiffTechnicalAssignment.Services;
using Microsoft.AspNetCore.Mvc;

namespace MadiffTechnicalAssignment.Controllers
{
    [ApiController]
    [Route("v1/api/[controller]")]
    public class CardController : ControllerBase
    {
        private readonly ILogger<CardController> _logger;
        private readonly ICardService _cardService;

        public CardController(ILogger<CardController> logger, ICardService cardService)
        {
            _logger = logger;
            _cardService = cardService;
        }

        [HttpGet("getAllowedCardActions/{userId}/{cardNumber}")]
        public async Task<ActionResult> GetAllowedCardActionsAsync(string userId, string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(cardNumber))
            {
                _logger.LogWarning("BadRequest: userId or cardNumber is missing");
                return BadRequest("userId and cardNumber are required.");
            }

            var cardDetails = await _cardService.GetCardDetails(userId, cardNumber);
            if(cardDetails == null)
            {
                _logger.LogWarning("NotFound: Card not found for userId='{userId}', cardNumber='{cardNumber}'", userId, cardNumber);
                return NotFound("Card not found.");
            }

            var allowedActions = _cardService.GetAllowedCardActions(cardDetails);
            _logger.LogInformation("Allowed actions retrieved for userId='{userId}', cardNumber='{cardNumber}'", userId, cardNumber);

            return Ok(allowedActions);
        }
    }
}
