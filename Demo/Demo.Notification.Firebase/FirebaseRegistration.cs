using System;
using Demo.Domain.Entities.Auditing;

namespace Demo.Notification.Firebase
{
    public class FirebaseRegistration : CreationAuditedEntity<Guid>
    {
        public string RegistrationId { get; set; }
        public string DeviceId { get; set; }
        public long UserId { get; set; }
    }
}