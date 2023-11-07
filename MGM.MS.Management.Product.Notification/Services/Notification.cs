using MGM.MS.Management.Product.Notification.Interfaces;

namespace MGM.MS.Management.Product.Notification.Services
{
    internal class Notification : INotification
    {
        public int NotificationCode { get; private set; }

        private readonly List<string> _notifications;

        public Notification() => _notifications = new List<string>();

        public IReadOnlyCollection<string> GetNotifications()
            => _notifications;

        public bool HasNotifications()
            => _notifications.Any();

        public void AddNotification(int code, string message)
        {
            NotificationCode = code;
            _notifications.Add(message);
        }
    }
}
