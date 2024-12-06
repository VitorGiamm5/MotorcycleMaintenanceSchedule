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

        apiReponse.SetData(new List<ScheduleEntity>
        {
            new ()
            {
                Id = "1",
                Name = "John",
                Observation = "test",
                Email = "a@a.com",
                Status = StatusMaintenanceEnum.AwaitingForSchedule,
                Phone = 11111111,
                PhoneDDD = 11,
            },
            new ()
            {
                Id = "2",
                Name = "Wick",
                Observation = "test",
                ScheduleDate = DateTime.UtcNow,
                Email = "b@b.com",
                Status = StatusMaintenanceEnum.InProgress,
                Phone = 22222222,
                PhoneDDD = 11,
            }
        });

        apiReponse.SetPaginationMetadata(currentPage: 1, pageSize: 100, totalRecords: 2);

        return apiReponse;
    }
}
