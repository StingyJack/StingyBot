namespace StingyBot.Common
{
    using System;
    using System.Collections.Generic;

    public class OicDic<T> : Dictionary<string, T>
    {
        public OicDic() : base(StringComparer.OrdinalIgnoreCase)
        {
        }
    }
}