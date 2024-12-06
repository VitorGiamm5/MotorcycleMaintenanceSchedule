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
    public async Task<IActionResult> List(
        [FromQuery] int? limit,
        [FromQuery] int? page,
        [FromQuery] string? orderBy,
        [FromQuery] bool? ascending,
        [FromQuery] StatusMaintenanceEnum? status = null,
        [FromQuery] ScheduleSearchFieldEnum? searchField = null,
        [FromQuery] string? searchValue = null)
    {
        try
        {
            limit ??= 100;
            page ??= 1;
            orderBy ??= ScheduleOrderByEnum.DateCreated.ToString();
            ascending ??= true;

            var request = new ScheduleListParams
            {
                Limit = (int)limit,
                Page = (int)page,
                OrderBy = Enum.TryParse<ScheduleOrderByEnum>(orderBy, true, out var parsedOrderBy) ? parsedOrderBy : ScheduleOrderByEnum.DateCreated,
                Ascending = (bool)ascending,
                Status = status,
                SearchField = searchField,
                SearchValue = searchValue
            };

            var result = await _mediator.Send(request);

            return Response(result);
        }
        catch (Exception ex)
        {
            return ResponseError(ex);
        }
    }
}
