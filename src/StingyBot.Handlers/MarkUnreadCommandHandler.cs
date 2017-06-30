// ReSharper disable UnusedMember.Global
namespace StingyBot.Handlers
{
    using System;
    using System.Threading.Tasks;
    using Common;
    using Common.HandlerInterfaces;
    using Common.Models;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    ///     Marks messages as unread for the specified timeframe
    /// </summary>
    public class MarkUnreadCommandHandler : SlackConnectedBase, ICommandHandler
    {
        private string _slackApiKey; //not needed

        public void Configure(IConfigurationRoot configRoot, string slackApiKey)
        {
            _slackApiKey = slackApiKey;
        }

        public async Task<Message> PerformUserCommandAsync(MessageContext messageContext)
        {
            //var twoHoursAgo = DateTimeOffset.Now.AddHours(-2).ToUnixTimeMilliseconds();

            //get the timestamp from the message text
            //  or just now - 2 hours
            //get the channels the user is in 

            var sc = await GetNewSlackConnectionAsync(_slackApiKey);
            await sc.GetUsers();
            //var users = await sc.GetUsers();
            //var user = users.FirstOrDefault(u => u.Name == messageContext.UserName);


            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }

    
    }
}