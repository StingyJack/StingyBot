namespace StingyBot.Common
{
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using Newtonsoft.Json;
    using SlackConnector.Models;

    public static class Extensions
    {
        public static string GetCommaSepListOfPropNames(this object instance)
        {
            var props = new StringBuilder();
            foreach (var prop in instance.GetType().GetProperties())
            {
                props.Append($"{prop.Name},");
            }
            props.Length--;
            return props.ToString();
        }


        public static string GetBotInternalSelfIdent(this ContactDetails instance)
        {
            return $"<@{instance.Id}>";
        }

        /// <summary>
        ///     I hate the attributes and refactoring this binary formatter makes me pick
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static T CloneByBinaryFormatter<T>(this T instance)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, instance);
                ms.Position = 0;

                return (T) formatter.Deserialize(ms);
            }
        }

        public static T CloneByJson<T>(this T instance) where T : class
        {
            var json = JsonConvert.SerializeObject(instance);
            var newInstance = JsonConvert.DeserializeObject(json,typeof(T));
            return newInstance as T;
        }
    }
}