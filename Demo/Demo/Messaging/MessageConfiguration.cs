using Demo.Configuration;

namespace Demo.Messaging
{
    /// <summary>
    /// Implementation of <see cref="IMessageConfiguration"/>
    /// </summary>
    public class MessageConfiguration : IMessageConfiguration
    {
        public IConfigure Configure { get; }

        public MessageConfiguration(IConfigure configure)
        {
            Configure = configure;
        }
    }
}