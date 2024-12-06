using MediatR;
using MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Repositories.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Repositories.Schedule.BaseRepositories;
using MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse;
using MotorcycleMaintenanceSchedule.Domain.Schedule;

namespace MotorcycleMaintenanceSchedule.Application.Services.Internal.Schedule.Queries.List;

public class ScheduleListHandler : IRequestHandler<ScheduleListParamsQuery, ActionResult>
{
    private readonly IScheduleListRepository _scheduleListRepository;

    public ScheduleListHandler(
        IScheduleListRepository scheduleListRepository, 
        IBaseRepository<ScheduleEntity> baseRepository
        )
    {
        _scheduleListRepository = scheduleListRepository;
    }

    public async Task<ActionResult> Handle(ScheduleListParamsQuery request, CancellationToken cancellationToken)
    {
        var queryParams = new ScheduleListParams
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            Ascending = request.Ascending,
            OrderBy = request.OrderBy,
            Status = request.Status,
            SearchField = request.SearchField,
            SearchValue = request.SearchValue
        };

        var result = await _scheduleListRepository.List(queryParams);

        var apiReponse = new ActionResult();

        apiReponse.SetData(result.Items);

        apiReponse.SetPaginationMetadata(
            currentPage: result.Pagination!.CurrentPage,
            pageSize: result.Pagination.PageSize,
            totalRecords: result.Pagination.TotalRecords);

        return apiReponse;
    }
}
