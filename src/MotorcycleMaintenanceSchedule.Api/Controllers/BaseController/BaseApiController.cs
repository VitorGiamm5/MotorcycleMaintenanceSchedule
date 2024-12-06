using Microsoft.AspNetCore.Mvc;
using MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse;
using System.Net;
using ActionResult = MotorcycleMaintenanceSchedule.Domain.Response.BaseResponse.ActionResult;

namespace MotorcycleMaintenanceSchedule.Api.Controllers.BaseController;

[ApiController]
[Produces("application/json")]
[Consumes("application/json")]
public class BaseApiController : ControllerBase
{
    protected new IActionResult Response(object? result)
    {
        return StatusCode((int)HttpStatusCode.NotFound);
    }

    protected new IActionResult Response(ActionResult response)
    {
        if (response is null)
        {
            return StatusCode((int)HttpStatusCode.NotFound);
        }

        var data = response.GetData();

        if (response.HasError())
        {
            return StatusCode((int)HttpStatusCode.BadRequest, response.GetError());
        }
        else if (response.HasData())
        {
            return StatusCode((int)HttpStatusCode.OK, data);
        }

        return StatusCode((int)HttpStatusCode.NotFound);
    }

    protected IActionResult ResponseError(object exception)
    {
        var apiResponse = new ActionResult();

        apiResponse.SetError(CommomMessagesConst.MESSAGE_INVALID_DATA, exception);

        return StatusCode((int)HttpStatusCode.InternalServerError, apiResponse);
    }
}
