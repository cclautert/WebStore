using FluentValidation;
using FluentValidation.Results;
using WebStore.Business.Interfaces;
using WebStore.Business.Models;
using WebStore.Business.Notifications;

namespace WebStore.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotifier _notificador;

        public BaseService(INotifier notificador)
        {
            _notificador = notificador;
        }

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var item in validationResult.Errors) 
            {
                Notify(item.ErrorMessage);
            }
        }

        protected void Notify(string mensagem)
        {
            _notificador.Handle(new Notification(mensagem));
        }

        protected bool RunValidation<TV, TE>(TV validation, TE entity) 
            where TV : AbstractValidator<TE>
            where TE : Entity
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid) return true;

            Notify(validator);

            return false;
        }
    }
}
