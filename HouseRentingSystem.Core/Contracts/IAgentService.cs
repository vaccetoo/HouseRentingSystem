namespace HouseRentingSystem.Core.Contracts
{
	public interface IAgentService
	{
		Task<bool> ExcistByIdAsync(string? userId);
		Task<bool> ExcistByPhoneNumberAsync(string phoneNumber);
		Task<bool> HasRentsAsync (string? userId);
		Task CreateAsync (string? userId, string phoneNumber);
		Task<int?> GetAgentIdAsync(string userId);
	}
}
