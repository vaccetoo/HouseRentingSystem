using HouseRentingSystem.Core.Contracts;
using HouseRentingSystem.Infrastructure.Data.Common;
using HouseRentingSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace HouseRentingSystem.Core.Services
{
	public class AgentService : IAgentService
	{
		private readonly IUnitOfWork _unitOfWork;

		public AgentService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;	
		}

		public async Task CreateAsync(string? userId, string phoneNumber)
		{
			CheckUserId(userId);

			await _unitOfWork.AddAsync(new Agent()
			{
				UserId = userId,
				PhoneNumber = phoneNumber
			});

			await _unitOfWork.SaveChangesAsync();
		}

		public async Task<bool> ExcistByIdAsync(string? userId)
		{
			return await _unitOfWork.AllAsNoTracking<Agent>()
				.AnyAsync(a => a.UserId == userId);
		}

		public async Task<bool> ExcistByPhoneNumberAsync(string phoneNumber)
		{
			return await _unitOfWork.AllAsNoTracking<Agent>()
				.AnyAsync(a => a.PhoneNumber == phoneNumber);
		}

		public async Task<bool> HasRentsAsync(string? userId)
		{
			CheckUserId(userId);

			return await _unitOfWork.AllAsNoTracking<House>()
				.AnyAsync(h => h.RenterId == userId);
		}

		private static void CheckUserId([NotNull]string? userId)
		{
			if (userId == null)
			{
				throw new ArgumentNullException(nameof(userId), "User ID can not be null!");
			}
		}
	}
}
