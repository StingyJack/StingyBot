namespace StingyBot.Common.SlackApi
{
    using System;
    using Newtonsoft.Json;

    public class MethodResponseBase
    {
        public static MethodResponseBase Parse(string responseJson)
        {
            try
            {
                return JsonConvert.DeserializeObject(responseJson, typeof(MethodResponseBase)) as MethodResponseBase;
            }
            catch (Exception)
            {
                //couldnt parse
            }
            return null;
        }

        public static T Parse<T>(string responseJson) where T: MethodResponseBase
        {
            try
            {
                return JsonConvert.DeserializeObject(responseJson, typeof(T)) as T;
            }
            catch (Exception)
            {
                //couldnt parse
            }
            return null;
        }

        public bool Ok { get; set; }

        public string Warning { get; set; }

        public string Error { get; set; }
        
    }
}