using FluxoDeCaixa.Domain.Notifications;

namespace FluxoDeCaixa.Domain.Services.Contracts
{
    public interface INotifier
    {
        bool HasNotifications();

        void AddNotification(string message);

        IEnumerable<Notification> GetNotifications();
    }
}
