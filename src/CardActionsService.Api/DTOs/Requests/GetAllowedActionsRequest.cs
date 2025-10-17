using System.ComponentModel.DataAnnotations;

namespace CardActionsService.Api.DTOs.Requests
{
    public class GetAllowedActionRequest
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string CardNumber { get; set; }
    }
}
