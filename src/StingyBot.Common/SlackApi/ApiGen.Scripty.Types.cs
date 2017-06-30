// Generated at 2/5/2017 10:21:27 PM UTC by Andrew
// Types Generation
// ReSharper disable RedundantUsingDirective
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PartialTypeWithSinglePart

namespace StingyBot.Common.SlackApi.Types
{
	 using System;
	 using Newtonsoft.Json;

	 public partial class Channel
	 {
		  [JsonProperty("id")]
		  public string Id {get;set;}

		  [JsonProperty("name")]
		  public string Name {get;set;}

		  [JsonProperty("is_channel")]
		  public string IsChannel {get;set;}

		  [JsonProperty("created")]
		  public string Created {get;set;}

		  [JsonProperty("creator")]
		  public string Creator {get;set;}

		  [JsonProperty("is_archived")]
		  public string IsArchived {get;set;}

		  [JsonProperty("is_general")]
		  public string IsGeneral {get;set;}

		  [JsonProperty("members")]
		  public string[] Members {get;set;}

		 //skipping Topic - custom type

		 //skipping Purpose - custom type

		  [JsonProperty("is_member")]
		  public string IsMember {get;set;}

		  [JsonProperty("last_read")]
		  public string LastRead {get;set;}

		 //skipping Latest - custom type

		  [JsonProperty("unread_count")]
		  public string UnreadCount {get;set;}

		  [JsonProperty("unread_count_display")]
		  public string UnreadCountDisplay {get;set;}

	 }

	 public partial class File
	 {
		  [JsonProperty("id")]
		  public string Id {get;set;}

		  [JsonProperty("created")]
		  public string Created {get;set;}

		  [JsonProperty("timestamp")]
		  public string Timestamp {get;set;}

		  [JsonProperty("name")]
		  public string Name {get;set;}

		  [JsonProperty("title")]
		  public string Title {get;set;}

		  [JsonProperty("mimetype")]
		  public string Mimetype {get;set;}

		  [JsonProperty("filetype")]
		  public string Filetype {get;set;}

		  [JsonProperty("pretty_type")]
		  public string PrettyType {get;set;}

		  [JsonProperty("user")]
		  public string User {get;set;}

		  [JsonProperty("mode")]
		  public string Mode {get;set;}

		  [JsonProperty("editable")]
		  public string Editable {get;set;}

		  [JsonProperty("is_external")]
		  public string IsExternal {get;set;}

		  [JsonProperty("external_type")]
		  public string ExternalType {get;set;}

		  [JsonProperty("username")]
		  public string Username {get;set;}

		  [JsonProperty("size")]
		  public string Size {get;set;}

		  [JsonProperty("url_private")]
		  public string UrlPrivate {get;set;}

		  [JsonProperty("url_private_download")]
		  public string UrlPrivateDownload {get;set;}

		  [JsonProperty("thumb_64")]
		  public string Thumb64 {get;set;}

		  [JsonProperty("thumb_80")]
		  public string Thumb80 {get;set;}

		  [JsonProperty("thumb_360")]
		  public string Thumb360 {get;set;}

		  [JsonProperty("thumb_360_gif")]
		  public string Thumb360Gif {get;set;}

		  [JsonProperty("thumb_360_w")]
		  public string Thumb360W {get;set;}

		  [JsonProperty("thumb_360_h")]
		  public string Thumb360H {get;set;}

		  [JsonProperty("thumb_480")]
		  public string Thumb480 {get;set;}

		  [JsonProperty("thumb_480_w")]
		  public string Thumb480W {get;set;}

		  [JsonProperty("thumb_480_h")]
		  public string Thumb480H {get;set;}

		  [JsonProperty("thumb_160")]
		  public string Thumb160 {get;set;}

		  [JsonProperty("permalink")]
		  public string Permalink {get;set;}

		  [JsonProperty("permalink_public")]
		  public string PermalinkPublic {get;set;}

		  [JsonProperty("edit_link")]
		  public string EditLink {get;set;}

		  [JsonProperty("preview")]
		  public string Preview {get;set;}

		  [JsonProperty("preview_highlight")]
		  public string PreviewHighlight {get;set;}

		  [JsonProperty("lines")]
		  public string Lines {get;set;}

		  [JsonProperty("lines_more")]
		  public string LinesMore {get;set;}

		  [JsonProperty("is_public")]
		  public string IsPublic {get;set;}

		  [JsonProperty("public_url_shared")]
		  public string PublicUrlShared {get;set;}

		  [JsonProperty("display_as_bot")]
		  public string DisplayAsBot {get;set;}

		  [JsonProperty("channels")]
		  public string[] Channels {get;set;}

		  [JsonProperty("groups")]
		  public string[] Groups {get;set;}

		  [JsonProperty("ims")]
		  public string[] Ims {get;set;}

		 //skipping InitialComment - custom type

		  [JsonProperty("num_stars")]
		  public string NumStars {get;set;}

		  [JsonProperty("is_starred")]
		  public string IsStarred {get;set;}

		  [JsonProperty("pinned_to")]
		  public string[] PinnedTo {get;set;}

		  [JsonProperty("reactions")]
		  public string[] Reactions {get;set;}

		  [JsonProperty("comments_count")]
		  public string CommentsCount {get;set;}

	 }

	 public partial class Group
	 {
		  [JsonProperty("id")]
		  public string Id {get;set;}

		  [JsonProperty("name")]
		  public string Name {get;set;}

		  [JsonProperty("is_group")]
		  public string IsGroup {get;set;}

		  [JsonProperty("created")]
		  public string Created {get;set;}

		  [JsonProperty("creator")]
		  public string Creator {get;set;}

		  [JsonProperty("is_archived")]
		  public string IsArchived {get;set;}

		  [JsonProperty("is_mpim")]
		  public string IsMpim {get;set;}

		  [JsonProperty("members")]
		  public string[] Members {get;set;}

		 //skipping Topic - custom type

		 //skipping Purpose - custom type

		  [JsonProperty("last_read")]
		  public string LastRead {get;set;}

		 //skipping Latest - custom type

		  [JsonProperty("unread_count")]
		  public string UnreadCount {get;set;}

		  [JsonProperty("unread_count_display")]
		  public string UnreadCountDisplay {get;set;}

	 }

	 public partial class Im
	 {
		  [JsonProperty("id")]
		  public string Id {get;set;}

		  [JsonProperty("is_im")]
		  public string IsIm {get;set;}

		  [JsonProperty("user")]
		  public string User {get;set;}

		  [JsonProperty("created")]
		  public string Created {get;set;}

		  [JsonProperty("is_user_deleted")]
		  public string IsUserDeleted {get;set;}

	 }

	 public partial class Mpim
	 {
		  [JsonProperty("id")]
		  public string Id {get;set;}

		  [JsonProperty("name")]
		  public string Name {get;set;}

		  [JsonProperty("is_mpim")]
		  public string IsMpim {get;set;}

		  [JsonProperty("is_group")]
		  public string IsGroup {get;set;}

		  [JsonProperty("created")]
		  public string Created {get;set;}

		  [JsonProperty("creator")]
		  public string Creator {get;set;}

		  [JsonProperty("members")]
		  public string[] Members {get;set;}

		  [JsonProperty("last_read")]
		  public string LastRead {get;set;}

		 //skipping Latest - custom type

		  [JsonProperty("unread_count")]
		  public string UnreadCount {get;set;}

		  [JsonProperty("unread_count_display")]
		  public string UnreadCountDisplay {get;set;}

	 }

	 public partial class User
	 {
		  [JsonProperty("id")]
		  public string Id {get;set;}

		  [JsonProperty("name")]
		  public string Name {get;set;}

		  [JsonProperty("deleted")]
		  public string Deleted {get;set;}

		  [JsonProperty("color")]
		  public string Color {get;set;}

		 //skipping Profile - custom type

		  [JsonProperty("is_admin")]
		  public string IsAdmin {get;set;}

		  [JsonProperty("is_owner")]
		  public string IsOwner {get;set;}

		  [JsonProperty("is_primary_owner")]
		  public string IsPrimaryOwner {get;set;}

		  [JsonProperty("is_restricted")]
		  public string IsRestricted {get;set;}

		  [JsonProperty("is_ultra_restricted")]
		  public string IsUltraRestricted {get;set;}

		  [JsonProperty("has_2fa")]
		  public string Has2Fa {get;set;}

		  [JsonProperty("two_factor_type")]
		  public string TwoFactorType {get;set;}

		  [JsonProperty("has_files")]
		  public string HasFiles {get;set;}

	 }

	 public partial class Usergroup
	 {
		  [JsonProperty("id")]
		  public string Id {get;set;}

		  [JsonProperty("team_id")]
		  public string TeamId {get;set;}

		  [JsonProperty("is_usergroup")]
		  public string IsUsergroup {get;set;}

		  [JsonProperty("name")]
		  public string Name {get;set;}

		  [JsonProperty("description")]
		  public string Description {get;set;}

		  [JsonProperty("handle")]
		  public string Handle {get;set;}

		  [JsonProperty("is_external")]
		  public string IsExternal {get;set;}

		  [JsonProperty("date_create")]
		  public string DateCreate {get;set;}

		  [JsonProperty("date_update")]
		  public string DateUpdate {get;set;}

		  [JsonProperty("date_delete")]
		  public string DateDelete {get;set;}

		  [JsonProperty("auto_type")]
		  public string AutoType {get;set;}

		  [JsonProperty("created_by")]
		  public string CreatedBy {get;set;}

		  [JsonProperty("updated_by")]
		  public string UpdatedBy {get;set;}

		  [JsonProperty("deleted_by")]
		  public string DeletedBy {get;set;}

		  [JsonProperty("prefs")]
		  public string[] Prefs {get;set;}

		  [JsonProperty("users")]
		  public string[] Users {get;set;}

		  [JsonProperty("user_count")]
		  public string UserCount {get;set;}

	 }

}
// Methods Generation
// Method Exec Generation
