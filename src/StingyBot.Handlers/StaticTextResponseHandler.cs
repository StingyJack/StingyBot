namespace StingyBot.Handlers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using Common.HandlerInterfaces;
    using Common.Models;
    using Newtonsoft.Json;

    public class StaticTextResponseHandler : LoggableBase, IStaticTextResponseHandler
    {
        //add stuff for loading message from file on disk
        protected string _FileName;

        public void Configure(string messageFile)
        {
            _FileName = messageFile;

            if (File.Exists(_FileName) == false)
            {
                throw new FileNotFoundException(_FileName);
            }
        }

        public async Task<Message> ConstructResponse(MessageContext messageContext)
        {
            try
            {
                string fileContent;
                using (var sr = File.OpenText(_FileName))
                {
                    fileContent = await sr.ReadToEndAsync();
                }
                var rawMsgFile = JsonConvert.DeserializeObject(fileContent, typeof(StaticMessageFile));
                var msgFile = (StaticMessageFile) rawMsgFile;
                return msgFile.StaticMessages.FirstOrDefault(f => f.Culture.Equals("en-US"))?.Message;
            }
            catch (Exception ex)
            {
                LogErr("Couldn't load file as message", ex);
                return new Message {Text = "Couldnt load the message from disk."};
            }
        }

        #region "misc"

        public void Dispose()
        {
            //no handles to worry about
        }

        #endregion //#region "misc"
    }
}