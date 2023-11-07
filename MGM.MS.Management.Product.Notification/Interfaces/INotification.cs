namespace MGM.MS.Management.Product.Notification.Interfaces
{
    public interface INotification
    {
        bool HasNotifications();

        int NotificationCode { get; }

        IReadOnlyCollection<string> GetNotifications();

        void AddNotification(int code, string message);
    }
}
