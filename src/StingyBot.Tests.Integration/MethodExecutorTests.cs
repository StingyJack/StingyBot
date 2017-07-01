namespace StingyBot.Tests.Integration
{
    using System;
    using Common.Configuration;
    using Common.SlackApi;
    using Common.SlackApi.Methods;
    using Microsoft.Extensions.Configuration;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MethodExecutorTests
    {
        private static MethodExecutor BuildMethodExecutor()
        {
            var slackConfigBuilder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("slackConfig.json.secret", false);

            var configurationRoot = slackConfigBuilder.Build();
            var slackConfig = new SlackConfig();
            configurationRoot.Bind(slackConfig);
            if (string.IsNullOrWhiteSpace(slackConfig.ApiKey))
            {
                throw new InvalidOperationException("No API key defined in the slackConfig.json.secret file. "
                                                    + "To fix this, make a file by that name and put it in your bin/Debug for this project "
                                                    + "with content like this (but use your own Api Key)"
                                                    + "'{ \"ApiKey\": \"xoxb-1098asdad25751-nDzLdjikKSDJSi0iG7iFy7Nj7q\"}'");
            }
            var me = new MethodExecutor(slackConfig.ApiKey);
            return me;
        }

        [TestMethod]
        public void ApiTest()
        {
            var me = BuildMethodExecutor();

            var result = me.Execute(new ApiTestRequestParams
            {
                Foo = "foob"
            });

            Assert.IsTrue(result.Ok);
        }

        [TestMethod]
        public void Channel_List()
        {
            var me = BuildMethodExecutor();
            var result = me.ExecChannelsList(new ChannelsListRequestParams {ExcludeArchived = "true"});
            Assert.IsTrue(result.Ok, result.Error);
        }

        [TestMethod]
        public void DirectMessage_List()
        {
            var me = BuildMethodExecutor();
            var result = me.ExecImList(new ImListRequestParams());
            Assert.IsTrue(result.Ok, result.Error);
        }

        [TestMethod]
        public void Chat_PostMessage()
        {
            var me = BuildMethodExecutor();

            var result = me.Execute(new ChatPostmessageRequestParams
            {
                AsUser = "",
                Attachments = "",
                Channel = "",
                IconEmoji = "",
                IconUrl = "",
                LinkNames = "",
                Parse = "",
                Text = "",
                UnfurlLinks = "",
                UnfurlMedia = "",
                Username = ""
            });

            Assert.IsTrue(result.Ok, result.Error);
        }
    }
}