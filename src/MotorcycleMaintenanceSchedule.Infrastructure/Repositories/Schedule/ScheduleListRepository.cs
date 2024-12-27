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

    public async Task<ActionResult> List(ScheduleListParams queryParams)
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
                ScheduleSearchFieldEnum.Id => query.Where(s => s.Id == queryParams.SearchValue),
                ScheduleSearchFieldEnum.Name => query.Where(s => s.Name.Contains(queryParams.SearchValue)),
                ScheduleSearchFieldEnum.Email => query.Where(s => s.Email == queryParams.SearchValue),
                ScheduleSearchFieldEnum.Phone => query.Where(s => s.Phone.ToString() == queryParams.SearchValue),
                ScheduleSearchFieldEnum.PhoneDDD => query.Where(s => s.PhoneDDD.ToString() == queryParams.SearchValue),
                ScheduleSearchFieldEnum.MotorcycleId => query.Where(s => s.MotorcycleId == queryParams.SearchValue),
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
            int skip = (queryParams.PageNumber.Value - 1) * queryParams.PageSize.Value;
            int take = queryParams.PageSize.Value;

            query = query.Skip(skip)
                         .Take(take);
        }

        var items = await query.ToListAsync() ?? [];

        var result = new ActionResult();

        result.SetData(items);
        result.SetPaginationMetadata(
            new PaginationMetadata(
                currentPage: queryParams.PageNumber!.Value,
                pageSize: queryParams.PageSize!.Value,
                totalRecords: totalRecords)
            );

        return result;
    }
}
