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
            var cardDetails = await _cardService.GetCardDetails(userId, cardNumber);
            if(cardDetails == null)
            {
                return NotFound("Card not found.");
            }

            var allowedActions = _cardService.GetAllowedCardActionsAsync(cardDetails);

            return Ok(allowedActions);
        }
    }
}
