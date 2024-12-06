using Microsoft.EntityFrameworkCore;
using MotorcycleMaintenanceSchedule.Infrastructure.Database;

namespace MotorcycleMaintenanceSchedule.Infrastructure.Repositories.BaseRepositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
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

    public async Task<T> Delete(int? id)
    {
        if (id == null)
        {
            throw new ArgumentNullException(nameof(id));
        }

        var entity = await _context.Set<T>()
            .FindAsync(id);

        if (entity == null)
        {
            throw new KeyNotFoundException($"Entity with id {id} not found.");
        }

        _context.Set<T>().Remove(entity).State = EntityState.Deleted;

        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<T> GetById(int? id)
    {
        if (id == null)
        {
            throw new ArgumentNullException(nameof(id));
        }

        var entity = await _context
            .Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);

        if (entity == null)
        {
            throw new KeyNotFoundException($"Entity with id {id} not found.");
        }

        return entity;
    }
    public async Task<T> Update(T entity)
    {
        _context.Set<T>().Update(entity)
            .State = EntityState.Modified;

        await _context.SaveChangesAsync();
        return entity;
    }
}
