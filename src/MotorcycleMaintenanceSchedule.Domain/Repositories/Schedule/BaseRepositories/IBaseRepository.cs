namespace MotorcycleMaintenanceSchedule.Domain.Repositories.Schedule.BaseRepositories;

public interface IBaseRepository<T> where T : class
{
    Task<T> GetById(string? id);
    Task<T> Create(T entity);
    Task<T> Update(T entity);
    Task<T> Delete(string? id);
}
