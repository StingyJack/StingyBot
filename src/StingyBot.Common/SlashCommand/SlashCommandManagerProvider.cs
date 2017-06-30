namespace StingyBot.Common.SlashCommand
{
    using System;

    //this could really just be the actual manager, but there is probably a better pattern
    //or something. I just didnt want to singleton the implementation class
    public static class SlashCommandManagerProvider
    {
        private static SlashCommandManager _instance;
        
        public static void SetInstance(SlashCommandManager manager)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }

            if (_instance != null
                && _instance != manager)
            {
                throw new ArgumentException("Instance already set to something else", nameof(manager));
            }

            _instance = manager;
        }

        public static SlashCommandManager GetInstance()
        {
            return _instance;
        }
    }
}
