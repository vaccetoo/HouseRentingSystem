using HouseRentingSystem.Core.Models.Home;
using HouseRentingSystem.Core.Models.House;

namespace HouseRentingSystem.Core.Contracts
{
	public interface IHouseService
	{
		Task<IEnumerable<HouseIndexServiceModel>> LastThreeHousesAsync();
		Task<IEnumerable<HouseCategoryServiceModel>> AllCategoriesAsync();
		Task<bool> CategoryExcistAsync(int categoryId);
		Task<int> CreateAsync(HouseFormModel model, int agentId);
		Task<HouseQueryServiceModel> AllAsync(AllHousesQueryModel model);
		Task<IEnumerable<string>> AllCategoriesNamesAsync();
		Task<IEnumerable<HouseServiceModel>> AllHousesByAgentIdAsync(int agentId);
		Task<IEnumerable<HouseServiceModel>> AllHousesByUserIdAsync(string useerId);
		Task<bool> ExcistByIdAsync(int id);
		Task<HouseDetailsServiceModel?> DetailsByIdAsync(int id);
		Task EditAsync(int houseId, HouseFormModel model);
		Task<bool> HasAgentWithIdAsync(int houseId, string? userId);
		Task<HouseFormModel?> GetHouseFormModelByIdAsync(int id);
		Task DeleteAsync(int houseId);
	}
}
