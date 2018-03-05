using System.Runtime.Serialization;

namespace Demo.Notification.Firebase
{
    public enum FirebaseNotificationPriority
    {
        [EnumMember(Value = "normal")]
        Normal = 5,
        [EnumMember(Value = "high")]
        High = 10
    }
}