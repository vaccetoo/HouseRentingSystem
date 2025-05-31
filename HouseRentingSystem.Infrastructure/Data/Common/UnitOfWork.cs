using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Infrastructure.Data.Common
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly DbContext _context;

		public UnitOfWork(ApplicationDbContext context)
		{
			_context = context;
		}

		// Add entity to DbContext
		public async Task AddAsync<TEntity>(TEntity entity) where TEntity : class
		{
			await GetDbSet<TEntity>().AddAsync(entity);
		}

		// Returns specific DBSet with change tracker
		public IQueryable<TEntity> All<TEntity>() where TEntity : class
		{
			return GetDbSet<TEntity>();
		}

		// eturns specific DBSet without change tracker
		public IQueryable<TEntity> AllAsNoTracking<TEntity>() where TEntity : class
		{
			return GetDbSet<TEntity>().AsNoTracking();
		}

		// Deletes specific entity
		public async Task DeleteAsync<TEntity>(object id) where TEntity : class
		{
			TEntity? entity = await GetByIdAsync<TEntity>(id);

			if (entity != null)
			{
				GetDbSet<TEntity>().Remove(entity);
			}
		}

		// Dispose context when needed
		public void Dispose()
		{
			_context.Dispose();
		}

		// Returns specific entity by its id
		public async Task<TEntity?> GetByIdAsync<TEntity>(object id) where TEntity : class
		{
			return await GetDbSet<TEntity>().FindAsync(id);
		}

		public async Task<int> SaveChangesAsync()
		{
			return await _context.SaveChangesAsync();
		}

		// Returns specific DbSet from context
		private DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
		{
			return _context.Set<TEntity>();
		}
	}
}
