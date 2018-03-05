using Hangfire;

namespace Demo.BackgroundJobs.Hangfire.Configuration
{
    public interface IHangfireConfiguration
    {
        BackgroundJobServer Server { get; set; }

        IGlobalConfiguration GlobalConfiguration { get; }
    }
}