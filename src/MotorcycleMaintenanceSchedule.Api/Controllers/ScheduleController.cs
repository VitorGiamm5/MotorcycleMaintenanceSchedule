using MediatR;
using Microsoft.AspNetCore.Mvc;
using MotorcycleMaintenanceSchedule.Api.Controllers.BaseController;
using MotorcycleMaintenanceSchedule.Application.Services.Internal.Schedule.Queries.List;
using MotorcycleMaintenanceSchedule.Domain.Schedule;

namespace MotorcycleMaintenanceSchedule.Api.Controllers;

[Route("api/schedule")]
[ApiController]
public class ScheduleController(IMediator _mediator) : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> List([FromQuery] ScheduleListParams request)
    {
        try
        {
            request.OrderBy ??= ScheduleOrderByEnum.DateCreated;

            var result = await _mediator.Send(new ScheduleListParamsQuery(request));

            return Response(result);
        }
        catch (Exception ex)
        {
            return ResponseError(ex);
        }
    }
}
