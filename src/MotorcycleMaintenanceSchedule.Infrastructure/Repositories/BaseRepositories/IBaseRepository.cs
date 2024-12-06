using Microsoft.EntityFrameworkCore.ChangeTracking;
using MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Repositories.BaseRepositories;
using MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse;

namespace MotorcycleMaintenanceSchedule.Infrastructure.Repositories.BaseRepositories;

public interface IBaseRepository<T> where T : class
{
    Task<PaginatedResult<T>> List(object? queryParams);
    Task<T> GetById(int? id);
    Task<T> Create(T entity);
    Task<T> Update(T entity);
    Task<T> Delete(int? id);
}
