using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Application.Services;

namespace Demo.Notification.Firebase
{
    public interface IFirebaseService : IApplicationService
    {
        Task InsertRegistrationAsync(FirebaseRegistration firebaseRegistration);
        Task DeleteRegistrationAsync(Guid id);
        Task<List<FirebaseRegistration>> GetAllRegistrationAsync(long userId);
    }
}