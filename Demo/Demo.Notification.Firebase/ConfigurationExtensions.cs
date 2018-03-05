using System;
using Demo.Configuration;
using Demo.Notifications;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Notification.Firebase
{
    public static class ConfigurationExtensions
    {
        public static void UseFirebase(this INotificationConfiguration configuration, Action<FirebaseNotificationConfiguration> configureAction)
        {
            var firebaseConfiguration = new FirebaseNotificationConfiguration();
            configureAction(firebaseConfiguration);

            configuration.Configure.Services.AddTransient<INotificationChannel, FirebaseNotificationChannel>();
            configuration.Configure.Services.AddSingleton(firebaseConfiguration);
        }
    }
}
