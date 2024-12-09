using Microsoft.EntityFrameworkCore;
using MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Repositories.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse;
using MotorcycleMaintenanceSchedule.Domain.Services.Iternal.Schedule.Queries.List;
using MotorcycleMaintenanceSchedule.Infrastructure.Database;
using MotorcycleMaintenanceSchedule.Infrastructure.Repositories.BaseRepositories;

namespace MotorcycleMaintenanceSchedule.Infrastructure.Repositories.Schedule;

public class ScheduleListRepository : BaseRepository<ScheduleEntity>, IScheduleListRepository
{
    public ScheduleListRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<PaginatedResult<ScheduleEntity>> ListAll()
    {
        var query = _context.Set<ScheduleEntity>().AsQueryable();

        var items = await query.ToListAsync() ?? [];

        var result = new PaginatedResult<ScheduleEntity>()
        {
            Items = items,
            Pagination = new PaginationMetadata(
                currentPage: 1,
                pageSize: 100000,
                totalRecords: items.Count)
        };

        return result;
    }

    public async Task<PaginatedResult<ScheduleEntity>> List(ScheduleListParams queryParams)
    {
        var query = _context.Set<ScheduleEntity>().AsQueryable();

        var totalRecords = await query.CountAsync();

        if (queryParams.Status.HasValue)
        {
            query = query.Where(s => s.Status == queryParams.Status.Value);
        }

        if (!string.IsNullOrEmpty(queryParams.SearchValue))
        {
            query = queryParams.SearchField switch
            {
                ScheduleSearchFieldEnum.Id => query.Where(s => s.Id.Contains(queryParams.SearchValue)),
                ScheduleSearchFieldEnum.Name => query.Where(s => s.Name.Contains(queryParams.SearchValue)),
                ScheduleSearchFieldEnum.Email => query.Where(s => s.Email.Contains(queryParams.SearchValue)),
                ScheduleSearchFieldEnum.Phone => query.Where(s => s.Phone.ToString().Contains(queryParams.SearchValue)),
                ScheduleSearchFieldEnum.PhoneDDD => query.Where(s => s.PhoneDDD.ToString().Contains(queryParams.SearchValue)),
                ScheduleSearchFieldEnum.MotorcycleId => query.Where(s => s.MotorcycleId.Contains(queryParams.SearchValue)),
                _ => query.Where(s => s.Name.Contains(queryParams.SearchValue)),
            };
        }

        if (queryParams.ScheduleDateStart.HasValue && queryParams.ScheduleDateEnd.HasValue)
        {
            query = query.Where(s => s.ScheduleDate >= queryParams.ScheduleDateStart.Value && s.ScheduleDate <= queryParams.ScheduleDateEnd.Value);
        }

        if (queryParams.OrderBy.HasValue)
        {
            query = queryParams.OrderBy.Value switch
            {
                ScheduleOrderByEnum.Name => queryParams.Ascending.GetValueOrDefault() ? query.OrderBy(s => s.Name) : query.OrderByDescending(s => s.Name),
                ScheduleOrderByEnum.ScheduleDate => queryParams.Ascending.GetValueOrDefault() ? query.OrderBy(s => s.ScheduleDate) : query.OrderByDescending(s => s.ScheduleDate),
                ScheduleOrderByEnum.Phone => queryParams.Ascending.GetValueOrDefault() ? query.OrderBy(s => s.Phone) : query.OrderByDescending(s => s.Phone),
                ScheduleOrderByEnum.PhoneDDD => queryParams.Ascending.GetValueOrDefault() ? query.OrderBy(s => s.PhoneDDD) : query.OrderByDescending(s => s.PhoneDDD),
                ScheduleOrderByEnum.Status => queryParams.Ascending.GetValueOrDefault() ? query.OrderBy(s => s.Status) : query.OrderByDescending(s => s.Status),
                ScheduleOrderByEnum.MotorcycleId => queryParams.Ascending.GetValueOrDefault() ? query.OrderBy(s => s.MotorcycleId) : query.OrderByDescending(s => s.MotorcycleId),
                ScheduleOrderByEnum.StartBusinessHour => queryParams.Ascending.GetValueOrDefault() ? query.OrderBy(s => s.StartBusinessHour) : query.OrderByDescending(s => s.StartBusinessHour),
                ScheduleOrderByEnum.EndBusinessHour => queryParams.Ascending.GetValueOrDefault() ? query.OrderBy(s => s.EndBusinessHour) : query.OrderByDescending(s => s.EndBusinessHour),
                ScheduleOrderByEnum.DateCreated => queryParams.Ascending.GetValueOrDefault() ? query.OrderBy(s => s.DateCreated) : query.OrderByDescending(s => s.DateCreated),
                _ => queryParams.Ascending.GetValueOrDefault() ? query.OrderBy(s => s.DateCreated) : query.OrderByDescending(s => s.DateCreated),
            };
        }


        if (queryParams.PageNumber.HasValue && queryParams.PageSize.HasValue)
        {
            query = query.Skip((queryParams.PageNumber.Value - 1) * queryParams.PageSize.Value)
                         .Take(queryParams.PageSize.Value);
        }

        var items = await query.ToListAsync() ?? [];

        var result = new PaginatedResult<ScheduleEntity>()
        {
            Items = items,
            Pagination = new PaginationMetadata(
                currentPage: queryParams.PageNumber!.Value,
                pageSize: queryParams.PageSize!.Value,
                totalRecords: totalRecords)
        };

        return result;
    }
}
