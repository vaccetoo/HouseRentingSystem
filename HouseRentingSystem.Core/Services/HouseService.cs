using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Enumerations;
using HouseRentingSystem.Core.Models.Home;
using HouseRentingSystem.Core.Models.House;
using HouseRentingSystem.Infrastructure.Data.Common;
using HouseRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HouseRentingSystem.Core.Services
{
	public class HouseService : IHouseService
	{
		private readonly IUnitOfWork _unitOfWork;

		public HouseService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<HouseQueryServiceModel> AllAsync(AllHousesQueryModel model)
		{
			var houses = _unitOfWork.AllAsNoTracking<House>();

			if (model.Category != null)
			{
				houses = houses
					.Where(h => h.Category.Name == model.Category);
			}

			if (model.SearchTerm != null)
			{
				string normalizedSearchTerm = model.SearchTerm.ToLower().Trim();

				houses = houses
					.Where(h => (h.Title.Contains(normalizedSearchTerm)) ||
								 h.Description.Contains(normalizedSearchTerm) ||
								 h.Address.Contains(normalizedSearchTerm));
			}

			houses = model.Sorting switch
			{
				HouseSorting.Price => houses
				.OrderBy(h => h.PricePerMonth),

				HouseSorting.NotRentedFirst => houses
				.OrderBy(H => H.RenterId == null)
				.ThenByDescending(h => h.Id),

				_ => houses
				.OrderByDescending(h => h.Id)
			};

			int totalHouses = await houses.CountAsync();

			var housesServiceModel = await houses
				.Skip((model.CurrentPage - 1) * model.HousesPerPage)
				.Take(model.HousesPerPage)
				.Select(h => new HouseServiceModel()
				{
					Id = h.Id,
					Address = h.Address,
					ImageURL = h.ImageURL,
					IsRented = h.RenterId != null,
					PricePerMonth = h.PricePerMonth,
					Title = h.Title
				})
				.ToListAsync();

			return new HouseQueryServiceModel()
			{
				Houses = housesServiceModel,
				TotalHousesCount = totalHouses
			};
		}

		public async Task<IEnumerable<HouseCategoryServiceModel>> AllCategoriesAsync()
		{
			return await _unitOfWork.AllAsNoTracking<Category>()
				.Select(c => new HouseCategoryServiceModel()
				{
					Id = c.Id,
					Name = c.Name
				})
				.ToListAsync();
		}

		public async Task<IEnumerable<string>> AllCategoriesNamesAsync()
		{
			return await _unitOfWork.AllAsNoTracking<Category>()
				.Select(c => c.Name)
				.ToListAsync();
		}

		public async Task<bool> CategoryExcistAsync(int categoryId)
		{
			return await _unitOfWork.AllAsNoTracking<Category>()
				.AnyAsync(c => c.Id == categoryId);
		}

		public async Task<int> CreateAsync(HouseFormModel model, int agentId)
		{
			var entity = new House()
			{
				Title = model.Title,
				CategoryId = model.CategoryId,
				Address = model.Address,
				AgentId = agentId,
				Description = model.Description,
				PricePerMonth = model.PricePerMonth,
				ImageURL = model.ImageURL
			};

			await _unitOfWork.AddAsync(entity);
			await _unitOfWork.SaveChangesAsync();

			return entity.Id;
		}

		public async Task<IEnumerable<HouseIndexServiceModel>> LastThreeHousesAsync()
		{
			return await _unitOfWork.AllAsNoTracking<House>()
				.OrderByDescending(h => h.Id)
				.Take(3)
				.Select(h => new HouseIndexServiceModel()
				{
					Id = h.Id,
					Title = h.Title,
					ImageURL = h.ImageURL
				})
				.ToListAsync();
				
		}


	}
}
