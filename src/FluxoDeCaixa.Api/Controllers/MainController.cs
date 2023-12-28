using FluxoDeCaixa.Domain.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace FluxoDeCaixa.Api.Controllers
{
    [ApiController]
    public abstract class MainController(INotifier notifier) : ControllerBase
    {
        protected bool IsValidOperation() =>
            !notifier.HasNotifications();

        protected ActionResult CustomResponse(HttpStatusCode statusCode = HttpStatusCode.OK, object? result = null)
        {
            if (IsValidOperation())
                return new ObjectResult(result) { StatusCode = Convert.ToInt32(statusCode) };

            return BadRequest(new
            {
                errors = notifier.GetNotifications().Select(x => x.Message)
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
                NotifyInvalidModel(modelState);
            return CustomResponse();
        }

        protected void NotifyInvalidModel(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                var message = error.Exception != null
                    ? error.Exception.Message
                    : error.ErrorMessage;
                AddNotification(message);
            }
        }

        protected void AddNotification(string message) =>
            notifier.AddNotification(message);
    }
}
