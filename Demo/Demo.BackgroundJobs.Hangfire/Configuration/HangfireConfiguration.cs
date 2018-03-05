using Hangfire;

namespace Demo.BackgroundJobs.Hangfire.Configuration
{
    public class HangfireConfiguration : IHangfireConfiguration
    {
        public BackgroundJobServer Server { get; set; }
        public IGlobalConfiguration GlobalConfiguration => global::Hangfire.GlobalConfiguration.Configuration;
    }
}