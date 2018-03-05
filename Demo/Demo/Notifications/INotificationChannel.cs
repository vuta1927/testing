using System;
using System.Threading.Tasks;

namespace Demo.Notifications
{
    public interface INotificationChannel
    {
        bool CanHandle(Type notificationDataType);
        Task ProcessAsync(UserNotificationInfo userNotificationsInfo);
    }
}