namespace StingyBot.Tests.Integration
{
    using System;
    using System.Collections.Generic;
    using Common;
    using Common.Nlp;
    using Microsoft.Extensions.Configuration;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SalesForce;

    [TestClass]
    public class SalesForceControllerTests
    {
        [TestMethod]
        public void ActualGetCaseDetails()
        {
            var sfc = GetSalesForceController();
            var mc = GetMessageContext("case 1026", "dne");
            var task = sfc.GetCaseDetailsAsync(mc);
            var result = task.GetAwaiter().GetResult();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ActualGetCaseList()
        {
            var sfc = GetSalesForceController();
            var mc = GetMessageContext("case list", "dne");
            var task = sfc.GetCaseListAsync(mc);
            var result = task.GetAwaiter().GetResult();

            Assert.IsNotNull(result);
        }

        private SalesForceController GetSalesForceController()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("salesforcesettings.json", true, true)
                .AddJsonFile("salesforcesettings.json.secrets", true);

            //.AddEnvironmentVariables();
            var configFileSettings = builder.Build();
            var sfc = new SalesForceController();
            sfc.Configure(configFileSettings);

            return sfc;
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

        // useful for getting salesforce object definitions
        [Ignore]
        [TestMethod]
        public void MyTestMethod()
        {
            var sfc = GetSalesForceController();
            var value = sfc.DescribeObjectAsync("CaseComment").GetAwaiter().GetResult();

            Assert.IsNotNull(value);
        }
    }
}