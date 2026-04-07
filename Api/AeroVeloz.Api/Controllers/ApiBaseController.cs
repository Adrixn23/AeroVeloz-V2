using AeroVeloz.Application.Services.Result;
using Microsoft.AspNetCore.Mvc;

namespace AeroVeloz.Api.Controllers
{
    /// <summary>
    /// Controlador base para centralizar la lógica de respuesta y estandarizar los códigos de estado HTTP.
    /// </summary>
    [ApiController]
    public abstract class ApiBaseController : ControllerBase
    {
        protected ActionResult<T> ProcessResult<T>(OperationResult<T> result)
        {
            if (result.Success)
            {
                return Ok(result);
            }

            return result.ErrorCode switch
            {
                "NOT_FOUND" or "FLIGHT_NOT_FOUND" or "AIRLINE_NOT_FOUND" => NotFound(result),
                "AUTH_ERROR" or "AIRLINE_AUTH" or "BATCH_AUTH" => StatusCode(StatusCodes.Status403Forbidden, result),
                "VALIDATION_ERROR" or "INVALID_INPUT" => BadRequest(result),
                _ => BadRequest(result)
            };
        }

        protected ActionResult<T> ProcessCreatedResult<T>(OperationResult<T> result, string actionName, object routeValues)
        {
            if (result.Success)
            {
                return CreatedAtAction(actionName, routeValues, result);
            }

            return ProcessResult(result);
        }

        protected ActionResult ProcessResult(OperationResult<bool> result)
        {
            if (result.Success)
            {
                return NoContent(); // 204 No Content es ideal para Updates y Deletes exitosos
            }

            return result.ErrorCode switch
            {
                "NOT_FOUND" => NotFound(result),
                "AUTH_ERROR" => Forbid(),
                _ => BadRequest(result)
            };
        }

        protected ActionResult ProcessNoContentResult(OperationResult<bool> result)
        {
            if (result.Success)
            {
                return NoContent();
            }

            return ProcessResult(result);
        }
    }
}
