namespace StingyBot.Common.HandlerInterfaces
{
    using System.Threading.Tasks;
    using Models;

    public interface IStaticTextResponseHandler : IUserActionHandler
    {
        void Configure(string messageFile);

        Task<Message> ConstructResponse(MessageContext messageContext);
    }
}