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

		// Returns specific DBSet with change tracker
		public IQueryable<TEntity> All<TEntity>() where TEntity : class
			=> GetDbSet<TEntity>();

		// eturns specific DBSet without change tracker
		public IQueryable<TEntity> AllAsNoTracking<TEntity>() where TEntity : class
			=> GetDbSet<TEntity>().AsNoTracking();

		// Dispose context when needed
		public void Dispose()
			=> _context.Dispose();

		// Returns specific DbSet from context
		private DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
			=> _context.Set<TEntity>();
	}
}
