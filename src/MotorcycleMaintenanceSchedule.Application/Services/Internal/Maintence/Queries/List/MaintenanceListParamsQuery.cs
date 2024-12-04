using MediatR;
using MotorcycleMaintenanceSchedule.Domain.Maintence;
using MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse;

namespace MotorcycleMaintenanceSchedule.Application.Services.Internal.Maintence.Queries.List;

public class MaintenanceListParamsQuery : IRequest<ActionResult>
{
    public StatusMaintenanceEnum? Status { get; set; }
}
