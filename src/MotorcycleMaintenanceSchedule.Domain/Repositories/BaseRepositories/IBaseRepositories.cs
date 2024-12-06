using MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse;

namespace MotorcycleMaintenanceSchedule.Domain.Repositories.BaseRepositories;

public interface IBaseRepositories<T> where T : class
{
    Task<PaginatedResult<T>> List(T maintenanceParams);
    Task<T?> GetById(int id);
    Task<T> Create(T entity);
    Task<T?> Update(T entity);
    Task<T?> Delete(int id);
}
