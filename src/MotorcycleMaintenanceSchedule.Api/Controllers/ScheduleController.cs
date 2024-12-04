using MediatR;
using Microsoft.AspNetCore.Mvc;
using MotorcycleMaintenanceSchedule.Api.Controllers.BaseController;
using MotorcycleMaintenanceSchedule.Application.Services.Internal.Maintence.Queries.List;

namespace MotorcycleMaintenanceSchedule.Api.Controllers;

[Route("api/schedule")]
[ApiController]
public class ScheduleController(IMediator _mediator) : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> List([FromQuery] MaintenanceListParamsQuery request)
    {
        try
        {
            var result = await _mediator.Send(request);

            return Response(result);
        }
        catch (Exception ex)
        {
            return ResponseError(ex);
        }
    }
}
