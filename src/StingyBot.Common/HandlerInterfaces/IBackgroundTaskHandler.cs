namespace StingyBot.Common.HandlerInterfaces
{
    using Microsoft.Extensions.Configuration;
    
    /// <summary>
    ///     A background task 
    /// </summary>
    public interface IBackgroundTaskHandler
    {
        void Configure(IConfigurationRoot configurationRoot);
        void Start();
        void Stop();
    }
}