using System.ComponentModel.DataAnnotations;
using Demo.Domain.Entities;

namespace Demo.Security
{
    /// <summary>
    /// Used to store a User Login for external Login services.
    /// </summary>
    public class UserLogin : Entity<long>
    {
        public long UserId { get; set; }
        [Required]
        public string LoginProvider { get; set; }
        [Required]
        public string ProviderKey { get; set; }
        [Required]
        public string ProviderDisplayName { get; set; }

        public UserLogin()
        {
        }

        public UserLogin(long userId, string loginProvider, string providerKey)
        {
            UserId = userId;
            LoginProvider = loginProvider;
            ProviderKey = providerKey;
        }
    }
}