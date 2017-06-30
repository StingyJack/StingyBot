// Generated at 2/5/2017 10:21:28 PM UTC by Andrew
// ReSharper disable RedundantUsingDirective
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PartialTypeWithSinglePart

namespace StingyBot.Common.SlackApi.Methods
{
	 using System;
	 using System.Collections.Generic;
	 using System.Collections.Specialized;
	 using Newtonsoft.Json;
	 using Types;

	 /// <summary>
	 /// 	Checks API calling code.
	 /// </summary>
	 public partial class ApiTestRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Error response to return
		 /// </summary>
		 /// <example>
		 /// 	"my_error"
		 /// </example>
		 [JsonProperty("error")]
		 public string Error {get; set;}
		 /// <summary>
		 /// 	example property to return
		 /// </summary>
		 /// <example>
		 /// 	"bar"
		 /// </example>
		 [JsonProperty("foo")]
		 public string Foo {get; set;}

		 public override string GetMethodName() {return "api.test"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Error != null)
			 {
				 returnValue.Add("error", Convert.ToString(Error));
			 }
			 if (Foo != null)
			 {
				 returnValue.Add("foo", Convert.ToString(Foo));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to ApiTestRequestParams
	 /// </summary>
	 public partial class ApiTestResponse : MethodResponseBase 
	 {
		 [JsonProperty("args")]
		 public object Args {get; set;}
	 }

	 /// <summary>
	 /// 	Checks authentication & identity.
	 /// </summary>
	 public partial class AuthTestRequestParams : ApiRequestParams
	 {


		 public override string GetMethodName() {return "auth.test"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to AuthTestRequestParams
	 /// </summary>
	 public partial class AuthTestResponse : MethodResponseBase 
	 {
		 [JsonProperty("url")]
		 public string Url {get; set;}
		 [JsonProperty("team")]
		 public string Team {get; set;}
		 [JsonProperty("user")]
		 public string User {get; set;}
		 [JsonProperty("team_id")]
		 public string Team_Id {get; set;}
		 [JsonProperty("user_id")]
		 public string User_Id {get; set;}
	 }

	 /// <summary>
	 /// 	Archives a channel.
	 /// </summary>
	 public partial class ChannelsArchiveRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Channel to archive
		 /// </summary>
		 [JsonProperty("channel")]
		 public object Channel {get; set;}

		 public override string GetMethodName() {return "channels.archive"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Channel != null)
			 {
				 returnValue.Add("channel", Convert.ToString(Channel));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to ChannelsArchiveRequestParams
	 /// </summary>
	 public partial class ChannelsArchiveResponse : MethodResponseBase 
	 {
	 }

	 /// <summary>
	 /// 	Creates a channel.
	 /// </summary>
	 public partial class ChannelsCreateRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Name of channel to create
		 /// </summary>
		 /// <example>
		 /// 	"mychannel"
		 /// </example>
		 [JsonProperty("name")]
		 public string Name {get; set;}

		 public override string GetMethodName() {return "channels.create"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Name != null)
			 {
				 returnValue.Add("name", Convert.ToString(Name));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to ChannelsCreateRequestParams
	 /// </summary>
	 public partial class ChannelsCreateResponse : MethodResponseBase 
	 {
		 [JsonProperty("channel")]
		 public object Channel {get; set;}
	 }

	 /// <summary>
	 /// 	Gets information about a channel.
	 /// </summary>
	 public partial class ChannelsInfoRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Channel to get info on
		 /// </summary>
		 [JsonProperty("channel")]
		 public object Channel {get; set;}

		 public override string GetMethodName() {return "channels.info"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Channel != null)
			 {
				 returnValue.Add("channel", Convert.ToString(Channel));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to ChannelsInfoRequestParams
	 /// </summary>
	 public partial class ChannelsInfoResponse : MethodResponseBase 
	 {
		 [JsonProperty("channel")]
		 public object Channel {get; set;}
	 }

	 /// <summary>
	 /// 	Invites a user to a channel.
	 /// </summary>
	 public partial class ChannelsInviteRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Channel to invite user to.
		 /// </summary>
		 [JsonProperty("channel")]
		 public object Channel {get; set;}
		 /// <summary>
		 /// 	User to invite to channel.
		 /// </summary>
		 [JsonProperty("user")]
		 public object User {get; set;}

		 public override string GetMethodName() {return "channels.invite"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Channel != null)
			 {
				 returnValue.Add("channel", Convert.ToString(Channel));
			 }
			 if (User != null)
			 {
				 returnValue.Add("user", Convert.ToString(User));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to ChannelsInviteRequestParams
	 /// </summary>
	 public partial class ChannelsInviteResponse : MethodResponseBase 
	 {
		 [JsonProperty("channel")]
		 public object Channel {get; set;}
	 }

	 /// <summary>
	 /// 	Joins a channel, creating it if needed.
	 /// </summary>
	 public partial class ChannelsJoinRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Name of channel to join
		 /// </summary>
		 [JsonProperty("name")]
		 public string Name {get; set;}

		 public override string GetMethodName() {return "channels.join"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Name != null)
			 {
				 returnValue.Add("name", Convert.ToString(Name));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to ChannelsJoinRequestParams
	 /// </summary>
	 public partial class ChannelsJoinResponse : MethodResponseBase 
	 {
		 [JsonProperty("channel")]
		 public object Channel {get; set;}
	 }

	 /// <summary>
	 /// 	Removes a user from a channel.
	 /// </summary>
	 public partial class ChannelsKickRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Channel to remove user from.
		 /// </summary>
		 [JsonProperty("channel")]
		 public object Channel {get; set;}
		 /// <summary>
		 /// 	User to remove from channel.
		 /// </summary>
		 [JsonProperty("user")]
		 public object User {get; set;}

		 public override string GetMethodName() {return "channels.kick"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Channel != null)
			 {
				 returnValue.Add("channel", Convert.ToString(Channel));
			 }
			 if (User != null)
			 {
				 returnValue.Add("user", Convert.ToString(User));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to ChannelsKickRequestParams
	 /// </summary>
	 public partial class ChannelsKickResponse : MethodResponseBase 
	 {
	 }

	 /// <summary>
	 /// 	Leaves a channel.
	 /// </summary>
	 public partial class ChannelsLeaveRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Channel to leave
		 /// </summary>
		 [JsonProperty("channel")]
		 public object Channel {get; set;}

		 public override string GetMethodName() {return "channels.leave"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Channel != null)
			 {
				 returnValue.Add("channel", Convert.ToString(Channel));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to ChannelsLeaveRequestParams
	 /// </summary>
	 public partial class ChannelsLeaveResponse : MethodResponseBase 
	 {
	 }

	 /// <summary>
	 /// 	Lists all channels in a Slack team.
	 /// </summary>
	 public partial class ChannelsListRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Don't return archived channels.
		 /// </summary>
		 /// <example>
		 /// 	"1"
		 /// </example>
		 [JsonProperty("exclude_archived")]
		 public string ExcludeArchived {get; set;}

		 public override string GetMethodName() {return "channels.list"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (ExcludeArchived != null)
			 {
				 returnValue.Add("exclude_archived", Convert.ToString(ExcludeArchived));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to ChannelsListRequestParams
	 /// </summary>
	 public partial class ChannelsListResponse : MethodResponseBase 
	 {
		 [JsonProperty("channels")]
		 public List<Channel> Channels {get; set;}
	 }

	 /// <summary>
	 /// 	Sets the read cursor in a channel.
	 /// </summary>
	 public partial class ChannelsMarkRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Channel to set reading cursor in.
		 /// </summary>
		 [JsonProperty("channel")]
		 public object Channel {get; set;}
		 /// <summary>
		 /// 	Timestamp of the most recently seen message.
		 /// </summary>
		 [JsonProperty("ts")]
		 public object Ts {get; set;}

		 public override string GetMethodName() {return "channels.mark"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Channel != null)
			 {
				 returnValue.Add("channel", Convert.ToString(Channel));
			 }
			 if (Ts != null)
			 {
				 returnValue.Add("ts", Convert.ToString(Ts));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to ChannelsMarkRequestParams
	 /// </summary>
	 public partial class ChannelsMarkResponse : MethodResponseBase 
	 {
	 }

	 /// <summary>
	 /// 	Renames a channel.
	 /// </summary>
	 public partial class ChannelsRenameRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Channel to rename
		 /// </summary>
		 [JsonProperty("channel")]
		 public object Channel {get; set;}
		 /// <summary>
		 /// 	New name for channel.
		 /// </summary>
		 [JsonProperty("name")]
		 public string Name {get; set;}

		 public override string GetMethodName() {return "channels.rename"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Channel != null)
			 {
				 returnValue.Add("channel", Convert.ToString(Channel));
			 }
			 if (Name != null)
			 {
				 returnValue.Add("name", Convert.ToString(Name));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to ChannelsRenameRequestParams
	 /// </summary>
	 public partial class ChannelsRenameResponse : MethodResponseBase 
	 {
		 [JsonProperty("channel")]
		 public object Channel {get; set;}
	 }

	 /// <summary>
	 /// 	Sets the purpose for a channel.
	 /// </summary>
	 public partial class ChannelsSetpurposeRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Channel to set the purpose of
		 /// </summary>
		 [JsonProperty("channel")]
		 public object Channel {get; set;}
		 /// <summary>
		 /// 	The new purpose
		 /// </summary>
		 /// <example>
		 /// 	"My Purpose"
		 /// </example>
		 [JsonProperty("purpose")]
		 public string Purpose {get; set;}

		 public override string GetMethodName() {return "channels.setPurpose"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Channel != null)
			 {
				 returnValue.Add("channel", Convert.ToString(Channel));
			 }
			 if (Purpose != null)
			 {
				 returnValue.Add("purpose", Convert.ToString(Purpose));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to ChannelsSetpurposeRequestParams
	 /// </summary>
	 public partial class ChannelsSetpurposeResponse : MethodResponseBase 
	 {
		 [JsonProperty("purpose")]
		 public string Purpose {get; set;}
	 }

	 /// <summary>
	 /// 	Sets the topic for a channel.
	 /// </summary>
	 public partial class ChannelsSettopicRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Channel to set the topic of
		 /// </summary>
		 [JsonProperty("channel")]
		 public object Channel {get; set;}
		 /// <summary>
		 /// 	The new topic
		 /// </summary>
		 /// <example>
		 /// 	"My Topic"
		 /// </example>
		 [JsonProperty("topic")]
		 public string Topic {get; set;}

		 public override string GetMethodName() {return "channels.setTopic"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Channel != null)
			 {
				 returnValue.Add("channel", Convert.ToString(Channel));
			 }
			 if (Topic != null)
			 {
				 returnValue.Add("topic", Convert.ToString(Topic));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to ChannelsSettopicRequestParams
	 /// </summary>
	 public partial class ChannelsSettopicResponse : MethodResponseBase 
	 {
		 [JsonProperty("topic")]
		 public string Topic {get; set;}
	 }

	 /// <summary>
	 /// 	Unarchives a channel.
	 /// </summary>
	 public partial class ChannelsUnarchiveRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Channel to unarchive
		 /// </summary>
		 [JsonProperty("channel")]
		 public object Channel {get; set;}

		 public override string GetMethodName() {return "channels.unarchive"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Channel != null)
			 {
				 returnValue.Add("channel", Convert.ToString(Channel));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to ChannelsUnarchiveRequestParams
	 /// </summary>
	 public partial class ChannelsUnarchiveResponse : MethodResponseBase 
	 {
	 }

	 /// <summary>
	 /// 	Deletes a message.
	 /// </summary>
	 public partial class ChatDeleteRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Timestamp of the message to be deleted.
		 /// </summary>
		 /// <example>
		 /// 	"1405894322.002768"
		 /// </example>
		 [JsonProperty("ts")]
		 public string Ts {get; set;}
		 /// <summary>
		 /// 	Channel containing the message to be deleted.
		 /// </summary>
		 [JsonProperty("channel")]
		 public object Channel {get; set;}

		 public override string GetMethodName() {return "chat.delete"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Ts != null)
			 {
				 returnValue.Add("ts", Convert.ToString(Ts));
			 }
			 if (Channel != null)
			 {
				 returnValue.Add("channel", Convert.ToString(Channel));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to ChatDeleteRequestParams
	 /// </summary>
	 public partial class ChatDeleteResponse : MethodResponseBase 
	 {
		 [JsonProperty("channel")]
		 public string Channel {get; set;}
		 [JsonProperty("ts")]
		 public string Ts {get; set;}
	 }

	 /// <summary>
	 /// 	Sends a message to a channel.
	 /// </summary>
	 public partial class ChatPostmessageRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Channel, private group, or IM channel to send message to. Can be an encoded ID, or a name. See [below](#channels) for more details.
		 /// </summary>
		 [JsonProperty("channel")]
		 public object Channel {get; set;}
		 /// <summary>
		 /// 	Text of the message to send. See below for an explanation of [formatting](#formatting).
		 /// </summary>
		 /// <example>
		 /// 	"Hello world"
		 /// </example>
		 [JsonProperty("text")]
		 public string Text {get; set;}
		 /// <summary>
		 /// 	Change how messages are treated. Defaults to `none`. See [below](#formatting).
		 /// </summary>
		 /// <example>
		 /// 	"full"
		 /// </example>
		 [JsonProperty("parse")]
		 public string Parse {get; set;}
		 /// <summary>
		 /// 	Find and link channel names and usernames.
		 /// </summary>
		 /// <example>
		 /// 	"1"
		 /// </example>
		 [JsonProperty("link_names")]
		 public string LinkNames {get; set;}
		 /// <summary>
		 /// 	Structured message attachments.
		 /// </summary>
		 /// <example>
		 /// 	"[{"pretext": "pre-hello", "text": "text-world"}]"
		 /// </example>
		 [JsonProperty("attachments")]
		 public string Attachments {get; set;}
		 /// <summary>
		 /// 	Pass true to enable unfurling of primarily text-based content.
		 /// </summary>
		 /// <example>
		 /// 	"true"
		 /// </example>
		 [JsonProperty("unfurl_links")]
		 public string UnfurlLinks {get; set;}
		 /// <summary>
		 /// 	Pass false to disable unfurling of media content.
		 /// </summary>
		 /// <example>
		 /// 	"false"
		 /// </example>
		 [JsonProperty("unfurl_media")]
		 public string UnfurlMedia {get; set;}
		 /// <summary>
		 /// 	Set your bot's user name. Must be used in conjunction with `as_user` set to false, otherwise ignored. See [authorship](#authorship) below.
		 /// </summary>
		 /// <example>
		 /// 	"My Bot"
		 /// </example>
		 [JsonProperty("username")]
		 public string Username {get; set;}
		 /// <summary>
		 /// 	Pass true to post the message as the authed user, instead of as a bot. Defaults to false. See [authorship](#authorship) below.
		 /// </summary>
		 /// <example>
		 /// 	"true"
		 /// </example>
		 [JsonProperty("as_user")]
		 public string AsUser {get; set;}
		 /// <summary>
		 /// 	URL to an image to use as the icon for this message. Must be used in conjunction with `as_user` set to false, otherwise ignored. See [authorship](#authorship) below.
		 /// </summary>
		 /// <example>
		 /// 	"http://lorempixel.com/48/48"
		 /// </example>
		 [JsonProperty("icon_url")]
		 public string IconUrl {get; set;}
		 /// <summary>
		 /// 	emoji to use as the icon for this message. Overrides `icon_url`. Must be used in conjunction with `as_user` set to false, otherwise ignored. See [authorship](#authorship) below.
		 /// </summary>
		 /// <example>
		 /// 	":chart_with_upwards_trend:"
		 /// </example>
		 [JsonProperty("icon_emoji")]
		 public string IconEmoji {get; set;}

		 public override string GetMethodName() {return "chat.postMessage"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Channel != null)
			 {
				 returnValue.Add("channel", Convert.ToString(Channel));
			 }
			 if (Text != null)
			 {
				 returnValue.Add("text", Convert.ToString(Text));
			 }
			 if (Parse != null)
			 {
				 returnValue.Add("parse", Convert.ToString(Parse));
			 }
			 if (LinkNames != null)
			 {
				 returnValue.Add("link_names", Convert.ToString(LinkNames));
			 }
			 if (Attachments != null)
			 {
				 returnValue.Add("attachments", Convert.ToString(Attachments));
			 }
			 if (UnfurlLinks != null)
			 {
				 returnValue.Add("unfurl_links", Convert.ToString(UnfurlLinks));
			 }
			 if (UnfurlMedia != null)
			 {
				 returnValue.Add("unfurl_media", Convert.ToString(UnfurlMedia));
			 }
			 if (Username != null)
			 {
				 returnValue.Add("username", Convert.ToString(Username));
			 }
			 if (AsUser != null)
			 {
				 returnValue.Add("as_user", Convert.ToString(AsUser));
			 }
			 if (IconUrl != null)
			 {
				 returnValue.Add("icon_url", Convert.ToString(IconUrl));
			 }
			 if (IconEmoji != null)
			 {
				 returnValue.Add("icon_emoji", Convert.ToString(IconEmoji));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to ChatPostmessageRequestParams
	 /// </summary>
	 public partial class ChatPostmessageResponse : MethodResponseBase 
	 {
		 [JsonProperty("ts")]
		 public string Ts {get; set;}
		 [JsonProperty("channel")]
		 public string Channel {get; set;}
		 [JsonProperty("message")]
		 public object Message {get; set;}
	 }

	 /// <summary>
	 /// 	Updates a message.
	 /// </summary>
	 public partial class ChatUpdateRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Timestamp of the message to be updated.
		 /// </summary>
		 /// <example>
		 /// 	"1405894322.002768"
		 /// </example>
		 [JsonProperty("ts")]
		 public string Ts {get; set;}
		 /// <summary>
		 /// 	Channel containing the message to be updated.
		 /// </summary>
		 [JsonProperty("channel")]
		 public object Channel {get; set;}
		 /// <summary>
		 /// 	New text for the message, using the [default formatting rules](/docs/formatting).
		 /// </summary>
		 /// <example>
		 /// 	"Hello world"
		 /// </example>
		 [JsonProperty("text")]
		 public string Text {get; set;}
		 /// <summary>
		 /// 	Structured message attachments.
		 /// </summary>
		 /// <example>
		 /// 	"[{"pretext": "pre-hello", "text": "text-world"}]"
		 /// </example>
		 [JsonProperty("attachments")]
		 public string Attachments {get; set;}
		 /// <summary>
		 /// 	Change how messages are treated. Defaults to `client`, unlike `chat.postMessage`. See [below](#formatting).
		 /// </summary>
		 /// <example>
		 /// 	"none"
		 /// </example>
		 [JsonProperty("parse")]
		 public string Parse {get; set;}
		 /// <summary>
		 /// 	Find and link channel names and usernames. Defaults to `none`. This parameter should be used in conjunction with `parse`. To set `link_names` to `1`, specify a `parse` mode of `full`.
		 /// </summary>
		 /// <example>
		 /// 	"1"
		 /// </example>
		 [JsonProperty("link_names")]
		 public string LinkNames {get; set;}
		 /// <summary>
		 /// 	Pass true to update the message as the authed user. [Bot users](/bot-users) in this context are considered authed users.
		 /// </summary>
		 /// <example>
		 /// 	"true"
		 /// </example>
		 [JsonProperty("as_user")]
		 public string AsUser {get; set;}

		 public override string GetMethodName() {return "chat.update"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Ts != null)
			 {
				 returnValue.Add("ts", Convert.ToString(Ts));
			 }
			 if (Channel != null)
			 {
				 returnValue.Add("channel", Convert.ToString(Channel));
			 }
			 if (Text != null)
			 {
				 returnValue.Add("text", Convert.ToString(Text));
			 }
			 if (Attachments != null)
			 {
				 returnValue.Add("attachments", Convert.ToString(Attachments));
			 }
			 if (Parse != null)
			 {
				 returnValue.Add("parse", Convert.ToString(Parse));
			 }
			 if (LinkNames != null)
			 {
				 returnValue.Add("link_names", Convert.ToString(LinkNames));
			 }
			 if (AsUser != null)
			 {
				 returnValue.Add("as_user", Convert.ToString(AsUser));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to ChatUpdateRequestParams
	 /// </summary>
	 public partial class ChatUpdateResponse : MethodResponseBase 
	 {
		 [JsonProperty("channel")]
		 public string Channel {get; set;}
		 [JsonProperty("ts")]
		 public string Ts {get; set;}
		 [JsonProperty("text")]
		 public string Text {get; set;}
	 }

	 /// <summary>
	 /// 	Ends the current user's Do Not Disturb session immediately.
	 /// </summary>
	 public partial class DndEnddndRequestParams : ApiRequestParams
	 {


		 public override string GetMethodName() {return "dnd.endDnd"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to DndEnddndRequestParams
	 /// </summary>
	 public partial class DndEnddndResponse : MethodResponseBase 
	 {
	 }

	 /// <summary>
	 /// 	Ends the current user's snooze mode immediately.
	 /// </summary>
	 public partial class DndEndsnoozeRequestParams : ApiRequestParams
	 {


		 public override string GetMethodName() {return "dnd.endSnooze"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to DndEndsnoozeRequestParams
	 /// </summary>
	 public partial class DndEndsnoozeResponse : MethodResponseBase 
	 {
		 [JsonProperty("dnd_enabled")]
		 public bool Dnd_Enabled {get; set;}
		 [JsonProperty("next_dnd_start_ts")]
		 public int Next_Dnd_Start_Ts {get; set;}
		 [JsonProperty("next_dnd_end_ts")]
		 public int Next_Dnd_End_Ts {get; set;}
		 [JsonProperty("snooze_enabled")]
		 public bool Snooze_Enabled {get; set;}
	 }

	 /// <summary>
	 /// 	Retrieves a user's current Do Not Disturb status.
	 /// </summary>
	 public partial class DndInfoRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	User to fetch status for (defaults to current user)
		 /// </summary>
		 /// <example>
		 /// 	"U1234"
		 /// </example>
		 [JsonProperty("user")]
		 public object User {get; set;}

		 public override string GetMethodName() {return "dnd.info"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (User != null)
			 {
				 returnValue.Add("user", Convert.ToString(User));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to DndInfoRequestParams
	 /// </summary>
	 public partial class DndInfoResponse : MethodResponseBase 
	 {
		 [JsonProperty("dnd_enabled")]
		 public bool Dnd_Enabled {get; set;}
		 [JsonProperty("next_dnd_start_ts")]
		 public int Next_Dnd_Start_Ts {get; set;}
		 [JsonProperty("next_dnd_end_ts")]
		 public int Next_Dnd_End_Ts {get; set;}
		 [JsonProperty("snooze_enabled")]
		 public bool Snooze_Enabled {get; set;}
		 [JsonProperty("snooze_endtime")]
		 public int Snooze_Endtime {get; set;}
		 [JsonProperty("snooze_remaining")]
		 public int Snooze_Remaining {get; set;}
	 }

	 /// <summary>
	 /// 	Turns on Do Not Disturb mode for the current user, or changes its duration.
	 /// </summary>
	 public partial class DndSetsnoozeRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Number of minutes, from now, to snooze until.
		 /// </summary>
		 /// <example>
		 /// 	"60"
		 /// </example>
		 [JsonProperty("num_minutes")]
		 public object NumMinutes {get; set;}

		 public override string GetMethodName() {return "dnd.setSnooze"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (NumMinutes != null)
			 {
				 returnValue.Add("num_minutes", Convert.ToString(NumMinutes));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to DndSetsnoozeRequestParams
	 /// </summary>
	 public partial class DndSetsnoozeResponse : MethodResponseBase 
	 {
		 [JsonProperty("snooze_enabled")]
		 public bool Snooze_Enabled {get; set;}
		 [JsonProperty("snooze_endtime")]
		 public int Snooze_Endtime {get; set;}
		 [JsonProperty("snooze_remaining")]
		 public int Snooze_Remaining {get; set;}
	 }

	 /// <summary>
	 /// 	Retrieves the Do Not Disturb status for users on a team.
	 /// </summary>
	 public partial class DndTeaminfoRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Comma-separated list of users to fetch Do Not Disturb status for
		 /// </summary>
		 /// <example>
		 /// 	"U1234,U4567"
		 /// </example>
		 [JsonProperty("users")]
		 public object Users {get; set;}

		 public override string GetMethodName() {return "dnd.teamInfo"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Users != null)
			 {
				 returnValue.Add("users", Convert.ToString(Users));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to DndTeaminfoRequestParams
	 /// </summary>
	 public partial class DndTeaminfoResponse : MethodResponseBase 
	 {
		 [JsonProperty("users")]
		 public object Users {get; set;}
	 }

	 /// <summary>
	 /// 	Lists custom emoji for a team.
	 /// </summary>
	 public partial class EmojiListRequestParams : ApiRequestParams
	 {


		 public override string GetMethodName() {return "emoji.list"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to EmojiListRequestParams
	 /// </summary>
	 public partial class EmojiListResponse : MethodResponseBase 
	 {
		 [JsonProperty("emoji")]
		 public object Emoji {get; set;}
	 }

	 /// <summary>
	 /// 	Add a comment to an existing file.
	 /// </summary>
	 public partial class FilesCommentsAddRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	File to add a comment to.
		 /// </summary>
		 /// <example>
		 /// 	"F1234567890"
		 /// </example>
		 [JsonProperty("file")]
		 public string File {get; set;}
		 /// <summary>
		 /// 	Text of the comment to add.
		 /// </summary>
		 /// <example>
		 /// 	"Everyone should take a moment to read this file."
		 /// </example>
		 [JsonProperty("comment")]
		 public string Comment {get; set;}

		 public override string GetMethodName() {return "files.comments.add"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (File != null)
			 {
				 returnValue.Add("file", Convert.ToString(File));
			 }
			 if (Comment != null)
			 {
				 returnValue.Add("comment", Convert.ToString(Comment));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to FilesCommentsAddRequestParams
	 /// </summary>
	 public partial class FilesCommentsAddResponse : MethodResponseBase 
	 {
		 [JsonProperty("comment")]
		 public object Comment {get; set;}
	 }

	 /// <summary>
	 /// 	Deletes an existing comment on a file.
	 /// </summary>
	 public partial class FilesCommentsDeleteRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	File to delete a comment from.
		 /// </summary>
		 /// <example>
		 /// 	"F1234567890"
		 /// </example>
		 [JsonProperty("file")]
		 public string File {get; set;}
		 /// <summary>
		 /// 	The comment to delete.
		 /// </summary>
		 /// <example>
		 /// 	"Fc1234567890"
		 /// </example>
		 [JsonProperty("id")]
		 public string Id {get; set;}

		 public override string GetMethodName() {return "files.comments.delete"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (File != null)
			 {
				 returnValue.Add("file", Convert.ToString(File));
			 }
			 if (Id != null)
			 {
				 returnValue.Add("id", Convert.ToString(Id));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to FilesCommentsDeleteRequestParams
	 /// </summary>
	 public partial class FilesCommentsDeleteResponse : MethodResponseBase 
	 {
	 }

	 /// <summary>
	 /// 	Edit an existing file comment.
	 /// </summary>
	 public partial class FilesCommentsEditRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	File containing the comment to edit.
		 /// </summary>
		 /// <example>
		 /// 	"F1234567890"
		 /// </example>
		 [JsonProperty("file")]
		 public string File {get; set;}
		 /// <summary>
		 /// 	The comment to edit.
		 /// </summary>
		 /// <example>
		 /// 	"Fc1234567890"
		 /// </example>
		 [JsonProperty("id")]
		 public string Id {get; set;}
		 /// <summary>
		 /// 	Text of the comment to edit.
		 /// </summary>
		 /// <example>
		 /// 	"Everyone should take a moment to read this file, seriously."
		 /// </example>
		 [JsonProperty("comment")]
		 public string Comment {get; set;}

		 public override string GetMethodName() {return "files.comments.edit"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (File != null)
			 {
				 returnValue.Add("file", Convert.ToString(File));
			 }
			 if (Id != null)
			 {
				 returnValue.Add("id", Convert.ToString(Id));
			 }
			 if (Comment != null)
			 {
				 returnValue.Add("comment", Convert.ToString(Comment));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to FilesCommentsEditRequestParams
	 /// </summary>
	 public partial class FilesCommentsEditResponse : MethodResponseBase 
	 {
		 [JsonProperty("comment")]
		 public object Comment {get; set;}
	 }

	 /// <summary>
	 /// 	Deletes a file.
	 /// </summary>
	 public partial class FilesDeleteRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	ID of file to delete.
		 /// </summary>
		 [JsonProperty("file")]
		 public string File {get; set;}

		 public override string GetMethodName() {return "files.delete"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (File != null)
			 {
				 returnValue.Add("file", Convert.ToString(File));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to FilesDeleteRequestParams
	 /// </summary>
	 public partial class FilesDeleteResponse : MethodResponseBase 
	 {
	 }

	 /// <summary>
	 /// 	Archives a private channel.
	 /// </summary>
	 public partial class GroupsArchiveRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Private channel to archive
		 /// </summary>
		 [JsonProperty("channel")]
		 public object Channel {get; set;}

		 public override string GetMethodName() {return "groups.archive"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Channel != null)
			 {
				 returnValue.Add("channel", Convert.ToString(Channel));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to GroupsArchiveRequestParams
	 /// </summary>
	 public partial class GroupsArchiveResponse : MethodResponseBase 
	 {
	 }

	 /// <summary>
	 /// 	Closes a private channel.
	 /// </summary>
	 public partial class GroupsCloseRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Private channel to close.
		 /// </summary>
		 [JsonProperty("channel")]
		 public object Channel {get; set;}

		 public override string GetMethodName() {return "groups.close"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Channel != null)
			 {
				 returnValue.Add("channel", Convert.ToString(Channel));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to GroupsCloseRequestParams
	 /// </summary>
	 public partial class GroupsCloseResponse : MethodResponseBase 
	 {
	 }

	 /// <summary>
	 /// 	Removes a user from a private channel.
	 /// </summary>
	 public partial class GroupsKickRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Private channel to remove user from.
		 /// </summary>
		 [JsonProperty("channel")]
		 public object Channel {get; set;}
		 /// <summary>
		 /// 	User to remove from private channel.
		 /// </summary>
		 [JsonProperty("user")]
		 public object User {get; set;}

		 public override string GetMethodName() {return "groups.kick"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Channel != null)
			 {
				 returnValue.Add("channel", Convert.ToString(Channel));
			 }
			 if (User != null)
			 {
				 returnValue.Add("user", Convert.ToString(User));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to GroupsKickRequestParams
	 /// </summary>
	 public partial class GroupsKickResponse : MethodResponseBase 
	 {
	 }

	 /// <summary>
	 /// 	Leaves a private channel.
	 /// </summary>
	 public partial class GroupsLeaveRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Private channel to leave
		 /// </summary>
		 [JsonProperty("channel")]
		 public object Channel {get; set;}

		 public override string GetMethodName() {return "groups.leave"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Channel != null)
			 {
				 returnValue.Add("channel", Convert.ToString(Channel));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to GroupsLeaveRequestParams
	 /// </summary>
	 public partial class GroupsLeaveResponse : MethodResponseBase 
	 {
	 }

	 /// <summary>
	 /// 	Sets the read cursor in a private channel.
	 /// </summary>
	 public partial class GroupsMarkRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Private channel to set reading cursor in.
		 /// </summary>
		 [JsonProperty("channel")]
		 public object Channel {get; set;}
		 /// <summary>
		 /// 	Timestamp of the most recently seen message.
		 /// </summary>
		 [JsonProperty("ts")]
		 public object Ts {get; set;}

		 public override string GetMethodName() {return "groups.mark"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Channel != null)
			 {
				 returnValue.Add("channel", Convert.ToString(Channel));
			 }
			 if (Ts != null)
			 {
				 returnValue.Add("ts", Convert.ToString(Ts));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to GroupsMarkRequestParams
	 /// </summary>
	 public partial class GroupsMarkResponse : MethodResponseBase 
	 {
	 }

	 /// <summary>
	 /// 	Opens a private channel.
	 /// </summary>
	 public partial class GroupsOpenRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Private channel to open.
		 /// </summary>
		 [JsonProperty("channel")]
		 public object Channel {get; set;}

		 public override string GetMethodName() {return "groups.open"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Channel != null)
			 {
				 returnValue.Add("channel", Convert.ToString(Channel));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to GroupsOpenRequestParams
	 /// </summary>
	 public partial class GroupsOpenResponse : MethodResponseBase 
	 {
	 }

	 /// <summary>
	 /// 	Renames a private channel.
	 /// </summary>
	 public partial class GroupsRenameRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Private channel to rename
		 /// </summary>
		 [JsonProperty("channel")]
		 public object Channel {get; set;}
		 /// <summary>
		 /// 	New name for private channel.
		 /// </summary>
		 [JsonProperty("name")]
		 public string Name {get; set;}

		 public override string GetMethodName() {return "groups.rename"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Channel != null)
			 {
				 returnValue.Add("channel", Convert.ToString(Channel));
			 }
			 if (Name != null)
			 {
				 returnValue.Add("name", Convert.ToString(Name));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to GroupsRenameRequestParams
	 /// </summary>
	 public partial class GroupsRenameResponse : MethodResponseBase 
	 {
		 [JsonProperty("channel")]
		 public object Channel {get; set;}
	 }

	 /// <summary>
	 /// 	Sets the purpose for a private channel.
	 /// </summary>
	 public partial class GroupsSetpurposeRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Private channel to set the purpose of
		 /// </summary>
		 [JsonProperty("channel")]
		 public object Channel {get; set;}
		 /// <summary>
		 /// 	The new purpose
		 /// </summary>
		 /// <example>
		 /// 	"My Purpose"
		 /// </example>
		 [JsonProperty("purpose")]
		 public string Purpose {get; set;}

		 public override string GetMethodName() {return "groups.setPurpose"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Channel != null)
			 {
				 returnValue.Add("channel", Convert.ToString(Channel));
			 }
			 if (Purpose != null)
			 {
				 returnValue.Add("purpose", Convert.ToString(Purpose));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to GroupsSetpurposeRequestParams
	 /// </summary>
	 public partial class GroupsSetpurposeResponse : MethodResponseBase 
	 {
		 [JsonProperty("purpose")]
		 public string Purpose {get; set;}
	 }

	 /// <summary>
	 /// 	Sets the topic for a private channel.
	 /// </summary>
	 public partial class GroupsSettopicRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Private channel to set the topic of
		 /// </summary>
		 [JsonProperty("channel")]
		 public object Channel {get; set;}
		 /// <summary>
		 /// 	The new topic
		 /// </summary>
		 /// <example>
		 /// 	"My Topic"
		 /// </example>
		 [JsonProperty("topic")]
		 public string Topic {get; set;}

		 public override string GetMethodName() {return "groups.setTopic"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Channel != null)
			 {
				 returnValue.Add("channel", Convert.ToString(Channel));
			 }
			 if (Topic != null)
			 {
				 returnValue.Add("topic", Convert.ToString(Topic));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to GroupsSettopicRequestParams
	 /// </summary>
	 public partial class GroupsSettopicResponse : MethodResponseBase 
	 {
		 [JsonProperty("topic")]
		 public string Topic {get; set;}
	 }

	 /// <summary>
	 /// 	Unarchives a private channel.
	 /// </summary>
	 public partial class GroupsUnarchiveRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Private channel to unarchive
		 /// </summary>
		 [JsonProperty("channel")]
		 public object Channel {get; set;}

		 public override string GetMethodName() {return "groups.unarchive"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Channel != null)
			 {
				 returnValue.Add("channel", Convert.ToString(Channel));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to GroupsUnarchiveRequestParams
	 /// </summary>
	 public partial class GroupsUnarchiveResponse : MethodResponseBase 
	 {
	 }

	 /// <summary>
	 /// 	Close a direct message channel.
	 /// </summary>
	 public partial class ImCloseRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Direct message channel to close.
		 /// </summary>
		 [JsonProperty("channel")]
		 public object Channel {get; set;}

		 public override string GetMethodName() {return "im.close"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Channel != null)
			 {
				 returnValue.Add("channel", Convert.ToString(Channel));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to ImCloseRequestParams
	 /// </summary>
	 public partial class ImCloseResponse : MethodResponseBase 
	 {
	 }

	 /// <summary>
	 /// 	Lists direct message channels for the calling user.
	 /// </summary>
	 public partial class ImListRequestParams : ApiRequestParams
	 {


		 public override string GetMethodName() {return "im.list"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to ImListRequestParams
	 /// </summary>
	 public partial class ImListResponse : MethodResponseBase 
	 {
		 [JsonProperty("ims")]
		 public List<Im> Ims {get; set;}
	 }

	 /// <summary>
	 /// 	Sets the read cursor in a direct message channel.
	 /// </summary>
	 public partial class ImMarkRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Direct message channel to set reading cursor in.
		 /// </summary>
		 [JsonProperty("channel")]
		 public object Channel {get; set;}
		 /// <summary>
		 /// 	Timestamp of the most recently seen message.
		 /// </summary>
		 [JsonProperty("ts")]
		 public object Ts {get; set;}

		 public override string GetMethodName() {return "im.mark"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Channel != null)
			 {
				 returnValue.Add("channel", Convert.ToString(Channel));
			 }
			 if (Ts != null)
			 {
				 returnValue.Add("ts", Convert.ToString(Ts));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to ImMarkRequestParams
	 /// </summary>
	 public partial class ImMarkResponse : MethodResponseBase 
	 {
	 }

	 /// <summary>
	 /// 	Opens a direct message channel.
	 /// </summary>
	 public partial class ImOpenRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	User to open a direct message channel with.
		 /// </summary>
		 [JsonProperty("user")]
		 public object User {get; set;}

		 public override string GetMethodName() {return "im.open"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (User != null)
			 {
				 returnValue.Add("user", Convert.ToString(User));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to ImOpenRequestParams
	 /// </summary>
	 public partial class ImOpenResponse : MethodResponseBase 
	 {
		 [JsonProperty("channel")]
		 public object Channel {get; set;}
	 }

	 /// <summary>
	 /// 	Closes a multiparty direct message channel.
	 /// </summary>
	 public partial class MpimCloseRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	MPIM to close.
		 /// </summary>
		 [JsonProperty("channel")]
		 public object Channel {get; set;}

		 public override string GetMethodName() {return "mpim.close"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Channel != null)
			 {
				 returnValue.Add("channel", Convert.ToString(Channel));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to MpimCloseRequestParams
	 /// </summary>
	 public partial class MpimCloseResponse : MethodResponseBase 
	 {
	 }

	 /// <summary>
	 /// 	Sets the read cursor in a multiparty direct message channel.
	 /// </summary>
	 public partial class MpimMarkRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	multiparty direct message channel to set reading cursor in.
		 /// </summary>
		 [JsonProperty("channel")]
		 public object Channel {get; set;}
		 /// <summary>
		 /// 	Timestamp of the most recently seen message.
		 /// </summary>
		 [JsonProperty("ts")]
		 public object Ts {get; set;}

		 public override string GetMethodName() {return "mpim.mark"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Channel != null)
			 {
				 returnValue.Add("channel", Convert.ToString(Channel));
			 }
			 if (Ts != null)
			 {
				 returnValue.Add("ts", Convert.ToString(Ts));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to MpimMarkRequestParams
	 /// </summary>
	 public partial class MpimMarkResponse : MethodResponseBase 
	 {
	 }

	 /// <summary>
	 /// 	Exchanges a temporary OAuth code for an API token.
	 /// </summary>
	 public partial class OauthAccessRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Issued when you created your application.
		 /// </summary>
		 /// <example>
		 /// 	"4b39e9-752c4"
		 /// </example>
		 [JsonProperty("client_id")]
		 public string ClientId {get; set;}
		 /// <summary>
		 /// 	Issued when you created your application.
		 /// </summary>
		 /// <example>
		 /// 	"33fea0113f5b1"
		 /// </example>
		 [JsonProperty("client_secret")]
		 public string ClientSecret {get; set;}
		 /// <summary>
		 /// 	The `code` param returned via the OAuth callback.
		 /// </summary>
		 /// <example>
		 /// 	"ccdaa72ad"
		 /// </example>
		 [JsonProperty("code")]
		 public string Code {get; set;}
		 /// <summary>
		 /// 	This must match the originally submitted URI (if one was sent).
		 /// </summary>
		 /// <example>
		 /// 	"http://example.com"
		 /// </example>
		 [JsonProperty("redirect_uri")]
		 public string RedirectUri {get; set;}

		 public override string GetMethodName() {return "oauth.access"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (ClientId != null)
			 {
				 returnValue.Add("client_id", Convert.ToString(ClientId));
			 }
			 if (ClientSecret != null)
			 {
				 returnValue.Add("client_secret", Convert.ToString(ClientSecret));
			 }
			 if (Code != null)
			 {
				 returnValue.Add("code", Convert.ToString(Code));
			 }
			 if (RedirectUri != null)
			 {
				 returnValue.Add("redirect_uri", Convert.ToString(RedirectUri));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to OauthAccessRequestParams
	 /// </summary>
	 public partial class OauthAccessResponse : MethodResponseBase 
	 {
		 [JsonProperty("access_token")]
		 public string Access_Token {get; set;}
		 [JsonProperty("scope")]
		 public string Scope {get; set;}
	 }

	 /// <summary>
	 /// 	Pins an item to a channel.
	 /// </summary>
	 public partial class PinsAddRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Channel to pin the item in.
		 /// </summary>
		 [JsonProperty("channel")]
		 public object Channel {get; set;}
		 /// <summary>
		 /// 	File to pin.
		 /// </summary>
		 [JsonProperty("file")]
		 public object File {get; set;}
		 /// <summary>
		 /// 	File comment to pin.
		 /// </summary>
		 [JsonProperty("file_comment")]
		 public object FileComment {get; set;}
		 /// <summary>
		 /// 	Timestamp of the message to pin.
		 /// </summary>
		 [JsonProperty("timestamp")]
		 public object Timestamp {get; set;}

		 public override string GetMethodName() {return "pins.add"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Channel != null)
			 {
				 returnValue.Add("channel", Convert.ToString(Channel));
			 }
			 if (File != null)
			 {
				 returnValue.Add("file", Convert.ToString(File));
			 }
			 if (FileComment != null)
			 {
				 returnValue.Add("file_comment", Convert.ToString(FileComment));
			 }
			 if (Timestamp != null)
			 {
				 returnValue.Add("timestamp", Convert.ToString(Timestamp));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to PinsAddRequestParams
	 /// </summary>
	 public partial class PinsAddResponse : MethodResponseBase 
	 {
	 }

	 /// <summary>
	 /// 	Un-pins an item from a channel.
	 /// </summary>
	 public partial class PinsRemoveRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Channel where the item is pinned to.
		 /// </summary>
		 [JsonProperty("channel")]
		 public object Channel {get; set;}
		 /// <summary>
		 /// 	File to un-pin.
		 /// </summary>
		 [JsonProperty("file")]
		 public object File {get; set;}
		 /// <summary>
		 /// 	File comment to un-pin.
		 /// </summary>
		 [JsonProperty("file_comment")]
		 public object FileComment {get; set;}
		 /// <summary>
		 /// 	Timestamp of the message to un-pin.
		 /// </summary>
		 [JsonProperty("timestamp")]
		 public object Timestamp {get; set;}

		 public override string GetMethodName() {return "pins.remove"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Channel != null)
			 {
				 returnValue.Add("channel", Convert.ToString(Channel));
			 }
			 if (File != null)
			 {
				 returnValue.Add("file", Convert.ToString(File));
			 }
			 if (FileComment != null)
			 {
				 returnValue.Add("file_comment", Convert.ToString(FileComment));
			 }
			 if (Timestamp != null)
			 {
				 returnValue.Add("timestamp", Convert.ToString(Timestamp));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to PinsRemoveRequestParams
	 /// </summary>
	 public partial class PinsRemoveResponse : MethodResponseBase 
	 {
	 }

	 /// <summary>
	 /// 	Manually set user presence
	 /// </summary>
	 public partial class PresenceSetRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Either `active` or `away`
		 /// </summary>
		 /// <example>
		 /// 	"away"
		 /// </example>
		 [JsonProperty("presence")]
		 public string Presence {get; set;}

		 public override string GetMethodName() {return "presence.set"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Presence != null)
			 {
				 returnValue.Add("presence", Convert.ToString(Presence));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to PresenceSetRequestParams
	 /// </summary>
	 public partial class PresenceSetResponse : MethodResponseBase 
	 {
	 }

	 /// <summary>
	 /// 	Adds a reaction to an item.
	 /// </summary>
	 public partial class ReactionsAddRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Reaction (emoji) name.
		 /// </summary>
		 /// <example>
		 /// 	"thumbsup"
		 /// </example>
		 [JsonProperty("name")]
		 public string Name {get; set;}
		 /// <summary>
		 /// 	File to add reaction to.
		 /// </summary>
		 [JsonProperty("file")]
		 public object File {get; set;}
		 /// <summary>
		 /// 	File comment to add reaction to.
		 /// </summary>
		 [JsonProperty("file_comment")]
		 public object FileComment {get; set;}
		 /// <summary>
		 /// 	Channel where the message to add reaction to was posted.
		 /// </summary>
		 [JsonProperty("channel")]
		 public object Channel {get; set;}
		 /// <summary>
		 /// 	Timestamp of the message to add reaction to.
		 /// </summary>
		 [JsonProperty("timestamp")]
		 public object Timestamp {get; set;}

		 public override string GetMethodName() {return "reactions.add"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Name != null)
			 {
				 returnValue.Add("name", Convert.ToString(Name));
			 }
			 if (File != null)
			 {
				 returnValue.Add("file", Convert.ToString(File));
			 }
			 if (FileComment != null)
			 {
				 returnValue.Add("file_comment", Convert.ToString(FileComment));
			 }
			 if (Channel != null)
			 {
				 returnValue.Add("channel", Convert.ToString(Channel));
			 }
			 if (Timestamp != null)
			 {
				 returnValue.Add("timestamp", Convert.ToString(Timestamp));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to ReactionsAddRequestParams
	 /// </summary>
	 public partial class ReactionsAddResponse : MethodResponseBase 
	 {
	 }

	 /// <summary>
	 /// 	Removes a reaction from an item.
	 /// </summary>
	 public partial class ReactionsRemoveRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Reaction (emoji) name.
		 /// </summary>
		 /// <example>
		 /// 	"thumbsup"
		 /// </example>
		 [JsonProperty("name")]
		 public string Name {get; set;}
		 /// <summary>
		 /// 	File to remove reaction from.
		 /// </summary>
		 [JsonProperty("file")]
		 public object File {get; set;}
		 /// <summary>
		 /// 	File comment to remove reaction from.
		 /// </summary>
		 [JsonProperty("file_comment")]
		 public object FileComment {get; set;}
		 /// <summary>
		 /// 	Channel where the message to remove reaction from was posted.
		 /// </summary>
		 [JsonProperty("channel")]
		 public object Channel {get; set;}
		 /// <summary>
		 /// 	Timestamp of the message to remove reaction from.
		 /// </summary>
		 [JsonProperty("timestamp")]
		 public object Timestamp {get; set;}

		 public override string GetMethodName() {return "reactions.remove"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Name != null)
			 {
				 returnValue.Add("name", Convert.ToString(Name));
			 }
			 if (File != null)
			 {
				 returnValue.Add("file", Convert.ToString(File));
			 }
			 if (FileComment != null)
			 {
				 returnValue.Add("file_comment", Convert.ToString(FileComment));
			 }
			 if (Channel != null)
			 {
				 returnValue.Add("channel", Convert.ToString(Channel));
			 }
			 if (Timestamp != null)
			 {
				 returnValue.Add("timestamp", Convert.ToString(Timestamp));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to ReactionsRemoveRequestParams
	 /// </summary>
	 public partial class ReactionsRemoveResponse : MethodResponseBase 
	 {
	 }

	 /// <summary>
	 /// 	Starts a Real Time Messaging session.
	 /// </summary>
	 public partial class RtmStartRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Return timestamp only for latest message object of each channel (improves performance).
		 /// </summary>
		 [JsonProperty("simple_latest")]
		 public object SimpleLatest {get; set;}
		 /// <summary>
		 /// 	Skip unread counts for each channel (improves performance).
		 /// </summary>
		 [JsonProperty("no_unreads")]
		 public object NoUnreads {get; set;}
		 /// <summary>
		 /// 	Returns MPIMs to the client in the API response.
		 /// </summary>
		 [JsonProperty("mpim_aware")]
		 public object MpimAware {get; set;}

		 public override string GetMethodName() {return "rtm.start"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (SimpleLatest != null)
			 {
				 returnValue.Add("simple_latest", Convert.ToString(SimpleLatest));
			 }
			 if (NoUnreads != null)
			 {
				 returnValue.Add("no_unreads", Convert.ToString(NoUnreads));
			 }
			 if (MpimAware != null)
			 {
				 returnValue.Add("mpim_aware", Convert.ToString(MpimAware));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to RtmStartRequestParams
	 /// </summary>
	 public partial class RtmStartResponse : MethodResponseBase 
	 {
		 [JsonProperty("url")]
		 public string Url {get; set;}
		 [JsonProperty("self")]
		 public object Self {get; set;}
		 [JsonProperty("team")]
		 public object Team {get; set;}
		 [JsonProperty("users")]
		 public List<User> Users {get; set;}
		 [JsonProperty("channels")]
		 public List<Channel> Channels {get; set;}
		 [JsonProperty("groups")]
		 public List<Group> Groups {get; set;}
		 [JsonProperty("mpims")]
		 public List<Mpim> Mpims {get; set;}
		 [JsonProperty("ims")]
		 public List<Im> Ims {get; set;}
		 [JsonProperty("bots")]
		 public object[] Bots {get; set;}
	 }

	 /// <summary>
	 /// 	Adds a star to an item.
	 /// </summary>
	 public partial class StarsAddRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	File to add star to.
		 /// </summary>
		 [JsonProperty("file")]
		 public object File {get; set;}
		 /// <summary>
		 /// 	File comment to add star to.
		 /// </summary>
		 [JsonProperty("file_comment")]
		 public object FileComment {get; set;}
		 /// <summary>
		 /// 	Channel to add star to, or channel where the message to add star to was posted (used with `timestamp`).
		 /// </summary>
		 [JsonProperty("channel")]
		 public object Channel {get; set;}
		 /// <summary>
		 /// 	Timestamp of the message to add star to.
		 /// </summary>
		 [JsonProperty("timestamp")]
		 public object Timestamp {get; set;}

		 public override string GetMethodName() {return "stars.add"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (File != null)
			 {
				 returnValue.Add("file", Convert.ToString(File));
			 }
			 if (FileComment != null)
			 {
				 returnValue.Add("file_comment", Convert.ToString(FileComment));
			 }
			 if (Channel != null)
			 {
				 returnValue.Add("channel", Convert.ToString(Channel));
			 }
			 if (Timestamp != null)
			 {
				 returnValue.Add("timestamp", Convert.ToString(Timestamp));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to StarsAddRequestParams
	 /// </summary>
	 public partial class StarsAddResponse : MethodResponseBase 
	 {
	 }

	 /// <summary>
	 /// 	Removes a star from an item.
	 /// </summary>
	 public partial class StarsRemoveRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	File to remove star from.
		 /// </summary>
		 [JsonProperty("file")]
		 public object File {get; set;}
		 /// <summary>
		 /// 	File comment to remove star from.
		 /// </summary>
		 [JsonProperty("file_comment")]
		 public object FileComment {get; set;}
		 /// <summary>
		 /// 	Channel to remove star from, or channel where the message to remove star from was posted (used with `timestamp`).
		 /// </summary>
		 [JsonProperty("channel")]
		 public object Channel {get; set;}
		 /// <summary>
		 /// 	Timestamp of the message to remove star from.
		 /// </summary>
		 [JsonProperty("timestamp")]
		 public object Timestamp {get; set;}

		 public override string GetMethodName() {return "stars.remove"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (File != null)
			 {
				 returnValue.Add("file", Convert.ToString(File));
			 }
			 if (FileComment != null)
			 {
				 returnValue.Add("file_comment", Convert.ToString(FileComment));
			 }
			 if (Channel != null)
			 {
				 returnValue.Add("channel", Convert.ToString(Channel));
			 }
			 if (Timestamp != null)
			 {
				 returnValue.Add("timestamp", Convert.ToString(Timestamp));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to StarsRemoveRequestParams
	 /// </summary>
	 public partial class StarsRemoveResponse : MethodResponseBase 
	 {
	 }

	 /// <summary>
	 /// 	Gets information about the current team.
	 /// </summary>
	 public partial class TeamInfoRequestParams : ApiRequestParams
	 {


		 public override string GetMethodName() {return "team.info"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to TeamInfoRequestParams
	 /// </summary>
	 public partial class TeamInfoResponse : MethodResponseBase 
	 {
		 [JsonProperty("team")]
		 public object Team {get; set;}
	 }

	 /// <summary>
	 /// 	Gets user presence information.
	 /// </summary>
	 public partial class UsersGetpresenceRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	User to get presence info on. Defaults to the authed user.
		 /// </summary>
		 [JsonProperty("user")]
		 public object User {get; set;}

		 public override string GetMethodName() {return "users.getPresence"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (User != null)
			 {
				 returnValue.Add("user", Convert.ToString(User));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to UsersGetpresenceRequestParams
	 /// </summary>
	 public partial class UsersGetpresenceResponse : MethodResponseBase 
	 {
		 [JsonProperty("presence")]
		 public string Presence {get; set;}
	 }

	 /// <summary>
	 /// 	Gets information about a user.
	 /// </summary>
	 public partial class UsersInfoRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	User to get info on
		 /// </summary>
		 [JsonProperty("user")]
		 public object User {get; set;}

		 public override string GetMethodName() {return "users.info"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (User != null)
			 {
				 returnValue.Add("user", Convert.ToString(User));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to UsersInfoRequestParams
	 /// </summary>
	 public partial class UsersInfoResponse : MethodResponseBase 
	 {
		 [JsonProperty("user")]
		 public object User {get; set;}
	 }

	 /// <summary>
	 /// 	Lists all users in a Slack team.
	 /// </summary>
	 public partial class UsersListRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Whether to include presence data in the output
		 /// </summary>
		 /// <example>
		 /// 	"1"
		 /// </example>
		 [JsonProperty("presence")]
		 public string Presence {get; set;}

		 public override string GetMethodName() {return "users.list"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Presence != null)
			 {
				 returnValue.Add("presence", Convert.ToString(Presence));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to UsersListRequestParams
	 /// </summary>
	 public partial class UsersListResponse : MethodResponseBase 
	 {
		 [JsonProperty("members")]
		 public object[] Members {get; set;}
	 }

	 /// <summary>
	 /// 	Marks a user as active.
	 /// </summary>
	 public partial class UsersSetactiveRequestParams : ApiRequestParams
	 {


		 public override string GetMethodName() {return "users.setActive"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to UsersSetactiveRequestParams
	 /// </summary>
	 public partial class UsersSetactiveResponse : MethodResponseBase 
	 {
	 }

	 /// <summary>
	 /// 	Manually sets user presence.
	 /// </summary>
	 public partial class UsersSetpresenceRequestParams : ApiRequestParams
	 {

		 /// <summary>
		 /// 	Either `auto` or `away`
		 /// </summary>
		 /// <example>
		 /// 	"away"
		 /// </example>
		 [JsonProperty("presence")]
		 public string Presence {get; set;}

		 public override string GetMethodName() {return "users.setPresence"; }

		 public override NameValueCollection GetParams()
		 {
			 var returnValue = new NameValueCollection();
			 returnValue.Add("token", Convert.ToString(ApiToken));
			 if (Presence != null)
			 {
				 returnValue.Add("presence", Convert.ToString(Presence));
			 }
			 return returnValue;
		 }
	 }

	 /// <summary>
	 /// 	The response to UsersSetpresenceRequestParams
	 /// </summary>
	 public partial class UsersSetpresenceResponse : MethodResponseBase 
	 {
	 }

}

