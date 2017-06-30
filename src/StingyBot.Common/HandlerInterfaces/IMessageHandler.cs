namespace StingyBot.Common.HandlerInterfaces
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Models;
    
    /// <summary>
    ///     The message handler interface
    /// </summary>
    public interface IMessageHandler : IUserActionHandler
    {
        void Configure(IConfigurationRoot configRoot);

        Task<Message> AcceptUserInputAsync(MessageContext messageContext);
    }
}