### Handlers

The handlers can be pretty much anything and come in a few basic interface types.

**Message handlers** are the common, action required responses to a user interactions  

**Static Text Response handlers** are for general fixed responses to user interactions (help, link lists, or other canned responses)

**Command handlers** are handling for slash commands  (requires some additional setup for web hooks)  

**Background Task handlers** are useful for posting those TPS reports (every hour, on the weekends)




There are a few handlers available already...

##### StingyBot.Handlers assembly

The `StaticTextResponseHandler` this will load a json file from disk and respond to the user with it. 
Useful for documentation, links, dad jokes, etc.


The `MarkUnreadCommandHandler` is a slash command to mark items unread either per the requesting users Do Not Disturb settings, or
from a specific point in time the user specifies. _This is still in progress, there is some api method generation that needs to get done first_

The `TeaTimeBackgroundTaskHandler` is an example of a background task that will post a message twice a day (no TPS reports, I promise)



The `StingyBot.SalesForce` plugin has a few case related commands
- `@botname case 8776` gets some specific salesforce case details and last update
- `@botname case list` connect to salesforce and get one case detail or last 5 cases opened


The `StingyBot.Tfs` assembly has a few work item related commands 
- `@botname wi 1234` connects to tfs and gets workitem 1234 with some details. (This
        also can use `pbi`, `bg`. or `tk` instead of `wi` but its not going to fail if 
        you ask for pbi 321 and its really a bug.)
- `@botname wiql....` is *partially* done. Some details will be emitted, but not a tabular or specific result.


