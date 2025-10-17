using CardActionsService.Api.DTOs.Requests;
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

        [HttpGet]
        public async Task<ActionResult> GetAllowedActions([FromQuery] GetAllowedActionRequest getAllowedActionRequest, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Processing allowed actions request for user {UserId} and card {CardNumber}", getAllowedActionRequest.UserId, getAllowedActionRequest.CardNumber);

            var allowedActions = await _cardActionsService.GetAllowedActionsAsync(
                getAllowedActionRequest.UserId,
                getAllowedActionRequest.CardNumber,
                cancellationToken);

            var response = new GetAllowedActionsResponse
            {
                AllowedActions = allowedActions.ToList()
            };

            _logger.LogInformation("Returning {ActionCount} allowed actions for user {UserId} and card {CardNumber}",
                allowedActions.Count(), getAllowedActionRequest.UserId, getAllowedActionRequest.CardNumber);

            return Ok(response);
        }
    }
}
