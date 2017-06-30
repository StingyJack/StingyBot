namespace StingyBot.Tests.Integration
{
    using System;
    using System.Collections.Generic;
    using Common;
    using Common.Nlp;
    using Microsoft.Extensions.Configuration;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tfs;

    [TestClass]
    public class TfsWorkitemControllerTests
    {
        [TestMethod]
        public void GetWorkitemById()
        {
            var cnl = BuildController();
            var workItemId = 1;
            var words = $"@bot wi {workItemId}";
            var messageContext = GetMessageContext(words, "TestTheRest");

            var workitem = cnl.AcceptUserInputAsync(messageContext).GetAwaiter().GetResult();

            Assert.IsNotNull(workitem);
        }

        [TestMethod]
        public void GetWorkitemsByQuery()
        {
            var cnl = BuildController();

            var query = WiqlQueries.GetQueryForAnyInProgressWork();
            var words = $"@bot wiql \"{query}\"";
            var messageContext = GetMessageContext(words, "TestTheRest");

            var result = cnl.GetWorkitemDetailsByWiqlQueryAsync(messageContext).GetAwaiter().GetResult();

            Assert.IsNotNull(result);
        }

        private static TfsWorkitemController BuildController()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("tfsconfiguration.json", true, true)
                .AddJsonFile("tfsconfiguration.json.secrets", true);

            var configFileSettings = builder.Build();
            var settings = new TfsConfiguration();
            configFileSettings.Bind(settings);

            var tfsc = new TfsWorkitemController();

            tfsc.Configure(configFileSettings);

            return tfsc;
        }


        private MessageContext GetMessageContext(string words, string channelName)
        {
            var lnp = new LexAndParseEngine(new List<SemanticReplacementToken>());
            var sentence = lnp.LexAndParseSentence(words);

            return new MessageContext
            {
                ChannelName = channelName,
                Sentence = sentence,
                RawMessage = words
            };
        }
    }
}