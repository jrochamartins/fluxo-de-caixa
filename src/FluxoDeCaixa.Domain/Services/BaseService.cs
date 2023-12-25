using FluentValidation;
using FluentValidation.Results;
using FluxoDeCaixa.Domain.Models;
using FluxoDeCaixa.Domain.Services.Contracts;

namespace FluxoDeCaixa.Domain.Services
{
    public abstract class BaseService(INotifier notifier)
    {
        private readonly INotifier _notifier = notifier;

        protected void AddNotification(string message) =>
            _notifier.AddNotification(message);

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
