using MediatR;
using MotorcycleMaintenanceSchedule.Domain.Entities.Schedule;
using MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse;

namespace MotorcycleMaintenanceSchedule.Application.Services.Internal.Maintence.Queries.List;

public class MaintenanceListHandler : IRequestHandler<MaintenanceListParamsQuery, ActionResult>
{
    public async Task<ActionResult> Handle(MaintenanceListParamsQuery request, CancellationToken cancellationToken)
    {
        var apiReponse = new ActionResult();

        apiReponse.SetData(new List<ScheduleEntity>
        {
            new ()
            {
                Id = "1",
                Name = "John",
                Observation = "test",
                SchedulingDate = DateTime.UtcNow,
                Email = "a@a.com",
                Phone = 11111111,
                PhoneDDD = 11,
            },
            new ()
            {
                Id = "2",
                Name = "Wick",
                Observation = "test",
                SchedulingDate = DateTime.UtcNow,
                Email = "b@b.com",
                Phone = 22222222,
                PhoneDDD = 11,
            }
        });

        apiReponse.SetPaginationMetadata(currentPage: 0, pageSize: 10, totalRecords: 2);

        return apiReponse;
    }
}
