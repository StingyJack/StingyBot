namespace StingyBot.Common
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Configuration;
    using log4net;
    using Microsoft.Extensions.Configuration;


    public interface IPluggableBot
    {
        /// <summary>
        ///     This will configure the bot using the default configuration method (botconfig.json file)
        /// </summary>
        /// <param name="itsBigItsHeavyItsWood"></param>
        /// <returns>A clone of the bot config</returns>
        BotConfig Configure(ILog itsBigItsHeavyItsWood);

        /// <summary>
        ///     This configures the bot using the configuration roots provided. 
        /// </summary>
        /// <param name="itsBigItsHeavyItsWood"></param>
        /// <param name="configs"></param>
        /// <returns>A clone of the bot config</returns>
        BotConfig Configure(ILog itsBigItsHeavyItsWood, Dictionary<string, IConfigurationRoot> configs);

        Task StartupAsync();
    }
}
