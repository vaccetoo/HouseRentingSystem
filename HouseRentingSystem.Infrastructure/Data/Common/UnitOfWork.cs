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

		public IQueryable<TEntity> All<TEntity>() where TEntity : class
			=> _context.Set<TEntity>();

		public IQueryable<TEntity> AllAsNoTracking<TEntity>() where TEntity : class
			=> _context.Set<TEntity>().AsNoTracking();

		public void Dispose()
			=> _context.Dispose();
	}
}
