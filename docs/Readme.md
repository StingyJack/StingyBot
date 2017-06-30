## This am the Stingy Bot

Well... there is a bot, but this is mostly a bot _framework_ with stuff already available to plug in.

This bot connects to slack and listens for direct mentions that include configured 
single word combinations (a.k.a "_Hears_"). The possible word combinations are scored and the highest 
scoring Hear has its _handler_ called to perform whatever action that handler does.

It's also able to listen for and respond to slash commands (this is working, but the example handler isnt done yet)

More information about configuration and "Hears" [here](Readme-botConfig.json.md).

[Available handlers](Readme-Handlers.md) and how to work with them

The current [Work in progress and planned work](Readme-WorkList.md)



#### Work currently in progress

- CodeGenning the RawApi stuff for other slack methods
- `@botname wiql "SELECT System.ID FROM WorkItems;"` - Executes the query and displays the results. This runs the query but
    needs to format the results nicer. Perhaps CodeSnippet or text/csv file (https://api.slack.com/methods/files.list). (slack connector
    doesn't yet have file upload, pull request outstanding).
- slash command handling works, but...
  - it needs to be accessible from slackHQ (or wherever) so you can use localtunnel, ngrok, or setup a proper tunnel
  - the /markunread handler is not complete (the slack connector needs a bit more functionality 
- Background task handling (for things like "do thing X at time T")  available but not finished

 ### things that needs user feedback
     - get more detail for a work item (codegen is working for tfs fields and workitems)
     - get more detail on cases (last update - not sure which is the right one; case comments, milestones, etc.)


More info on planned items in the [worklist readme](Readme-WorkList.md)

## Working with the code

**VERY IMPORTANT** - The System.Net.Http assembly that comes along with NETStandard 1.6 breaks most of the WebApi
and other calls (tfs, etc). Don't update the existing nuget packages without ensuring things arent 
wildly broken.  See https://github.com/dotnet/corefx/issues/11100 for more info. 

**less important** - handlers are intended to be loaded dynamically, by configuration.    



### solution parts

- **/Hosts/StingyBot.ConsoleHost** - a console host for the StingyBot. 
- **/Tests/\*\*** - integration and unit tests
- **/StingyBot.Bot** - the actual bot assembly
- **/StingyBot.Common** - interfaces, abstractions, and common components used by the bot and plugins.
- **/StingyBot.Configuration.MsOverride** = Pulled from Microsoft.Configuration.Extensions and related assemblies because
    MS dun goofed with System.Net.Http and broke a lot of stuff.
- **/StingyBot.Tfs** - the tfs related extension
- **/StingyBot.SalesForce** - the salesforce related extension
   

 ## botConfig.json
 This be the config file for the bot. Be sure to review and edit this before starting. (more info in the [readme for that file](Readme-botConfig.json.md))
 
 Please do not check your secrets or settings in, use a `.secret` file or $env

 ## getting started

 1) Clone repo
 2) (optional) `> npm install -g localtunnel` if you want to use it to expose webhooks
 3) Configure
 4) Build
 5) Run

LMK if i havent made the above possible (sans your specific settings for #3)