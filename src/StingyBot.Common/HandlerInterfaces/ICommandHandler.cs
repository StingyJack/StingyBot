namespace StingyBot.Common.HandlerInterfaces
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Models;

    /// <summary>
    ///     The slash command (and web hook) handler interface
    /// </summary>
    public interface ICommandHandler : IUserActionHandler
    {
        void Configure(IConfigurationRoot configRoot, string slackApiKey);

        Task<Message> PerformUserCommandAsync(MessageContext messageContext);
    }
}