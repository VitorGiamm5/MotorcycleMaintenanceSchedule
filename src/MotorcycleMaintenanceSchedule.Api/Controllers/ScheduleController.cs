using MediatR;
using Microsoft.AspNetCore.Mvc;
using MotorcycleMaintenanceSchedule.Api.Controllers.BaseController;
using MotorcycleMaintenanceSchedule.Application.Services.Internal.Schedule.Commands.Create;
using MotorcycleMaintenanceSchedule.Application.Services.Internal.Schedule.Commands.Delete;
using MotorcycleMaintenanceSchedule.Application.Services.Internal.Schedule.Commands.Update;
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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ScheduleCreateCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return Response(result);
        }
        catch (Exception ex)
        {
            return ResponseError(ex);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string? id, [FromBody] ScheduleUpdateCommand command)
    {
        try
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Response(result);
        }
        catch (Exception ex)
        {
            return ResponseError(ex);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            var result = await _mediator.Send(new ScheduleDeleteCommand { Id = id });
            return Response(result);
        }
        catch (Exception ex)
        {
            return ResponseError(ex);
        }
    }
}
