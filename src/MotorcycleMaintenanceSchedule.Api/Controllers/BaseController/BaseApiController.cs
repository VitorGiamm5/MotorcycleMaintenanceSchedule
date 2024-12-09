using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotorcycleMaintenanceSchedule.Domain.Exceptions;
using MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse;
using System.Net;
using System.Runtime.Serialization;
using ActionResult = MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse.ActionResult;

namespace MotorcycleMaintenanceSchedule.Api.Controllers.BaseController;

[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
public class BaseApiController : ControllerBase
{
    protected new IActionResult Response(ActionResult response)
    {
        if (response.HasError())
        {
            return BadRequest(response.GetError());
        }

        if (response is null || !response.HasData())
        {
            return Created();
        }

        if (response.HasData())
        {
            return Ok(response.GetData());
        }

        return NoContent();
    }
}
