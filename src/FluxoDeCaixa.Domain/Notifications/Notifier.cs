using FluxoDeCaixa.Domain.Abstractions.Notifications;

namespace FluxoDeCaixa.Domain.Notifications
{
    public class Notifier : INotifier
    {
        private readonly List<Notification> _notifications = [];

        public void AddNotification(string message) =>
            _notifications.Add(new Notification(message));

        public IEnumerable<Notification> GetNotifications() =>
            _notifications;

        public bool HasNotifications() =>
            _notifications?.Count > 0;
    }
}
