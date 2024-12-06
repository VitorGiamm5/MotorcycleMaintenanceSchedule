using MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse;
using MotorcycleMaintenanceSchedule.Infrastructure.Database;

namespace MotorcycleMaintenanceSchedule.Infrastructure.Repositories.BaseRepositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
{
    public readonly ApplicationDbContext _context;

    protected BaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<T> Create(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<T> Delete(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<T> GetById(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<PaginatedResult<T>> List(object? queryParams)
    {
        throw new NotImplementedException();
    }

    public Task<T> Update(T entity)
    {
        throw new NotImplementedException();
    }
}
