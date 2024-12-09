using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotorcycleMaintenanceSchedule.Application.Exceptions;
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
            ResponseException(new BussinessExceptionExtension(response.GetError()));
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

    protected IActionResult ResponseException(object exception)
    {
        var apiResponse = new ActionResult();

        if (exception is DbUpdateException dbUpdateException)
        {
            apiResponse.SetError(FaultMessagesConst.MESSAGE_ERROR_CREATE_ITEM, dbUpdateException.InnerException?.Message);

            return BadRequest(apiResponse);
        }

        if (exception is ArgumentNullException argumentNullException)
        {
            apiResponse.SetError(FaultMessagesConst.MESSAGE_ERROR_NULL_ARGUMENT, argumentNullException.Message);

            return BadRequest(apiResponse);
        }

        if (exception is InvalidOperationException invalidOperationException)
        {
            apiResponse.SetError(FaultMessagesConst.MESSAGE_ERROR_INVALID_OPERATION, invalidOperationException.Message);

            return BadRequest(apiResponse);
        }

        if (exception is UnauthorizedAccessException unauthorizedAccessException)
        {
            apiResponse.SetError(FaultMessagesConst.MESSAGE_ERROR_UNAUTHORIZED_ACCESS, unauthorizedAccessException.Message);

            return StatusCode((int)HttpStatusCode.Unauthorized, apiResponse);
        }

        if (exception is InvalidDataContractException invalidDataContractException)
        {
            apiResponse.SetError(FaultMessagesConst.MESSAGE_ERROR_INVALID_PAYLOAD, invalidDataContractException.Message);

            return BadRequest(apiResponse);
        }

        if (exception is ArgumentException argumentException)
        {
            apiResponse.SetError(FaultMessagesConst.MESSAGE_ERROR_INVALID_PARAMETERS, argumentException.Message);

            return BadRequest(apiResponse);
        }

        if (exception is BussinessExceptionExtension bussinessException)
        {
            apiResponse.SetError(FaultMessagesConst.MESSAGE_ERROR_BUSINESS_EXCEPTION, bussinessException.Message);

            return BadRequest(apiResponse);
        }

        apiResponse.SetError(FaultMessagesConst.MESSAGE_ERROR_INTERNAL_ERROR, exception);

        return StatusCode((int)HttpStatusCode.InternalServerError, apiResponse);
    }
}
