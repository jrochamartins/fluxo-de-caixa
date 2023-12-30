using FluentValidation;
using FluentValidation.Results;
using FluxoDeCaixa.Domain.Abstractions.Notifications;
using FluxoDeCaixa.Domain.Models;

namespace FluxoDeCaixa.Domain.Services
{
    public abstract class BaseService(INotifier notifier)
    {
        protected void AddNotification(string message) =>
            notifier.AddNotification(message);

        protected void AddNotification(ValidationResult validationResult)
        {
            foreach (var item in validationResult.Errors)
                AddNotification(item.ErrorMessage);
        }

        protected bool Validate<TValidator, TEntity>(TValidator validator, TEntity entity)
            where TValidator : AbstractValidator<TEntity>
            where TEntity : Entity
        {
            var validationResult = validator.Validate(entity);

            if (validationResult.IsValid)
                return true;

            AddNotification(validationResult);
            return false;
        }
    }
}
