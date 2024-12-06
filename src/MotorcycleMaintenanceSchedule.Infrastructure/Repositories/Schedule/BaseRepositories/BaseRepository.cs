using Microsoft.EntityFrameworkCore;
using MotorcycleMaintenanceSchedule.Domain.Repositories.Schedule.BaseRepositories;
using MotorcycleMaintenanceSchedule.Infrastructure.Database;

namespace MotorcycleMaintenanceSchedule.Infrastructure.Repositories.Schedule.BaseRepositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    public readonly ApplicationDbContext _context;

    protected BaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<T> Create(T entity)
    {
        _context.Set<T>().Add(entity)
            .State = EntityState.Added;

        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<T> Delete(string? id)
    {
        if (string.IsNullOrEmpty(id) || id == ":id")
        {
            return Activator.CreateInstance<T>();
        }

        var entity = await _context.Set<T>()
            .FindAsync(id);

        if (entity == null)
        {
            return Activator.CreateInstance<T>();
        }

        _context.Set<T>().Remove(entity).State = EntityState.Deleted;

        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<T> GetById(string? id)
    {
        if (string.IsNullOrEmpty(id) || id == ":id")
        {
            return Activator.CreateInstance<T>();
        }

        var entity = await _context
            .Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(e => EF.Property<string>(e, "Id") == id);

        return entity ?? Activator.CreateInstance<T>();
    }
    public async Task<T> Update(T entity)
    {
        _context.Set<T>().Update(entity)
            .State = EntityState.Modified;

        await _context.SaveChangesAsync();
        return entity;
    }
}
