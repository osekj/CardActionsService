namespace CardActionsService.Application.Interfaces
{
    public interface IAllowedActionsService
    {
        Task<IEnumerable<string>> GetAllowedActionsAsync(string userId, string cardNumber, CancellationToken cancellationToken);
    }
}
