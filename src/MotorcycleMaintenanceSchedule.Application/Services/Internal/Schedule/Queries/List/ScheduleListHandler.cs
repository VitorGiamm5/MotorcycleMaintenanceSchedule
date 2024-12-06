using MediatR;
using MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse;
using MotorcycleMaintenanceSchedule.Domain.Schedule;

namespace MotorcycleMaintenanceSchedule.Application.Services.Internal.Schedule.Queries.List;

public class ScheduleListHandler : IRequestHandler<ScheduleListParamsQuery, ActionResult>
{
    public async Task<ActionResult> Handle(ScheduleListParamsQuery request, CancellationToken cancellationToken)
    {
        var apiReponse = new ActionResult();

        var result = new List<ScheduleEntity>
        {
            new ()
            {
                Id = "1",
                Name = "John",
                Observation = "test",
                Email = "a@a.com",
                Status = ScheduleStatusEnum.AwaitingForSchedule,
                Phone = 11111111,
                PhoneDDD = 11,
                CreatedBy = "admin",
                DateCreated = DateTime.UtcNow,
                DateLastUpdate = DateTime.UtcNow,
            },
            new ()
            {
                Id = "2",
                Name = "Wick",
                Observation = "test",
                ScheduleDate = DateTime.UtcNow,
                Email = "b@b.com",
                Status = ScheduleStatusEnum.InProgress,
                Phone = 22222222,
                PhoneDDD = 11,
                CreatedBy = "admin",
                DateCreated = DateTime.UtcNow,
                DateLastUpdate = DateTime.UtcNow,
            }
        };

        apiReponse.SetData(result);

        apiReponse.SetPaginationMetadata(currentPage: (int)request.PageNumber!, pageSize: (int)request.PageSize!, totalRecords: result.Count);

        return apiReponse;
    }
}
