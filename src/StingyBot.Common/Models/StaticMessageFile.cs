namespace StingyBot.Common.Models
{
    using System.Collections.Generic;

    public class StaticMessageFile
    {
        public string Name { get; set; }

        public List<StaticMessage> StaticMessages { get; set; }
    }
}
