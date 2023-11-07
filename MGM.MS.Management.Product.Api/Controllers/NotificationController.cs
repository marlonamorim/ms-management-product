using MGM.MS.Management.Product.Notification.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MGM.MS.Management.Product.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [SwaggerResponse(StatusCodes.Status204NoContent)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    public class NotificationController : ControllerBase
    {
        protected readonly INotification _notification;

        public NotificationController(INotification notification)
        {
            _notification = notification;
        }

        protected async Task<IActionResult> HandleResponseAsync<T>(Func<Task<T>> func)
        {
            T result = await func().ConfigureAwait(false);
            return Response(result);
        }

        protected async Task<IActionResult> HandleResponseAsync(Func<Task> func)
        {

            await func().ConfigureAwait(false);
            return Response();
        }

        private new IActionResult Response(object? result = null)
        {
            if (IsValidOperation())
                return Ok(result);

            var notifications = _notification.GetNotifications();

            return PrepareResponseError(_notification.NotificationCode, notifications);
        }

        private IActionResult PrepareResponseError(int status, IReadOnlyCollection<string> notifications)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Ocorreu um erro inesperado.",
                Status = status,
                Detail = string.Join(',', notifications)
            };
            HttpContext.Response.StatusCode = status;
            HttpContext.Response.ContentType = "application/problem+json";

            return StatusCode(status, problemDetails);
        }

        protected bool IsValidOperation()
            => !_notification.HasNotifications();
    }
}
