namespace CardActionsService.Application.Exceptions
{
    public class CardNotFoundException : Exception
    {
        public CardNotFoundException(string userId, string cardNumber)
        : base($"Card with number '{cardNumber}' was not found for user '{userId}'.")
        {
        }
    }
}
