using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Core.Models.Home;
using HouseRentingSystem.Core.Models.House;
using HouseRentingSystem.Infrastructure.Data.Common;
using HouseRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Core.Services
{
	public class HouseService : IHouseService
	{
		private readonly IUnitOfWork _unitOfWork;

		public HouseService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
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
