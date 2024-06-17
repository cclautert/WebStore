using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebStore.Business.Interfaces;
using WebStore.Business.Notifications;

namespace WebStore.Identity.API.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotifier _notificator;

        protected MainController(INotifier notificator)
        {
            _notificator = notificator;
        }

        protected bool ValidatedOperation()
        {
            return !_notificator.HasNotification();
        }

        protected ActionResult CustomResponse(HttpStatusCode statusCode = HttpStatusCode.OK, object result = null)
        {
            if (ValidatedOperation())
            {
                return new ObjectResult(result)
                {
                    StatusCode = Convert.ToInt32(statusCode),
                };
            }

            return BadRequest(new
            {
                errors = _notificator.GetNotifications().Select(n => n.Mensage)
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotificarErroModelInvalid(modelState);
            return CustomResponse();
        }

        protected void NotificarErroModelInvalid(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotifyError(errorMsg);
            }
        }

        protected void NotifyError(string mensagem)
        {
            _notificator.Handle(new Notification(mensagem));
        }
    }
}
