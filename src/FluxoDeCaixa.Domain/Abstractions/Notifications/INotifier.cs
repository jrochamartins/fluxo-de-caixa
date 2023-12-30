using FluxoDeCaixa.Domain.Notifications;

namespace FluxoDeCaixa.Domain.Abstractions.Notifications
{
    public interface INotifier
    {
        bool HasNotifications();

        void AddNotification(string message);

        IEnumerable<Notification> GetNotifications();
    }
}
