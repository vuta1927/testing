using Newtonsoft.Json;

namespace Demo.Notification.Firebase
{
    public interface IFirebaseNotificationData
    {
        [JsonIgnore]
        object Tag { get; set; }

        [JsonProperty("message_id")]
        string MessageId { get; }
    }
}