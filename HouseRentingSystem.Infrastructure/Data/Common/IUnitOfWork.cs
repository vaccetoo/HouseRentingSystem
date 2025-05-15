namespace HouseRentingSystem.Infrastructure.Data.Common
{
	public interface IUnitOfWork : IDisposable
	{
		IQueryable<TEntity> All<TEntity>() where TEntity : class;
		IQueryable<TEntity> AllAsNoTracking<TEntity>() where TEntity : class;
	}
}
