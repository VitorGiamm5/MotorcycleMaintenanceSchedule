using MediatR;
using MotorcycleMaintenanceSchedule.Application.Cache.Interfaces;
using MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse;

namespace MotorcycleMaintenanceSchedule.Application.Services.Internal.Schedule.Queries.Summary;

public class ScheduleSummaryHandler : IRequestHandler<ScheduleSummaryParamsQuery, ActionResult>
{
    private readonly IMotorcycleCacheService _motorcycleCacheService;

    public ScheduleSummaryHandler(
        IMotorcycleCacheService motorcycleCacheService
        )
    {
        _motorcycleCacheService = motorcycleCacheService;
    }

    public Task<ActionResult> Handle(ScheduleSummaryParamsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
