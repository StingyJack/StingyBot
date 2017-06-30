## stuff to do

The current stuff to do...

#### general bot things

- use `OicDic()` from stingyjunk 
- 
- create a base message formatter that can do string replacement on the values
  by passing in the replacement tokens and values. Don't add caching unless its really needed. 
   -  `nameof(object.Propery)` will be useful for this
   -  At least add in a way to have the bot refer to itself by name, so it can be used in messages.
- add `@bot feedback "some feedback"` to collect user feedback (feature requests, problems, etc)
- wire in a database to hold the feedback and report on it. make the db accessible to handlers
- Figure out if log4net actually works for what i want
- add in ambient listening when bot is invited to the channel. 
- add method in IMessageHandlers that will get coarse command help that can be assembled for
    dynamic help. 
    - The specific help should come from the IMessageHandler.
    - Commands should follow the pattern of `action help` to get the help text for that command.
- `/markunread from <loosely specific time>` needs to mark unread from a specific point in time 
    for a user. Can only go back 48 hours
  -  `/markunread from 5pm yesterday` - everything after 5pm in all subscribed channels is marked unread
  -  `/markunread dnd` - Reads the user's dnd settings, and marks messages unread since the 
        end of the last "off" period
- `/cleanup ems` - cleanup ephemeral ("only visible to you") messages for the last 20 minutes
- figure out how to package this. Choco/Ansible/zip/etc. 
- add support for file upload
- Add support for returning larger ressage content as files or snippets (tabular query results) 
- add event api support
 - add custom reactions
 - listen for those reactions 
 - provide handlers for them (update a tfs workitem field with the message text for example)
  
#### tfs basic

- `@botname tfs qb vtcid` - queues the vt Cid build after sending notification with cancel button
- `@botname tfs find "search text"`
- `@botname tfs sprint roll tasks` - rollover tasks from one sprint to another
- `@botname tfs create new project "ProjectName"` setup team projects, etc, send out reminders or questions for each that has hours logged. 
- ambient listener to detect "PBI XXXX" and replace the text with a hyperlink to the tfs artifact
- `@botname tfs close/update wi XXXX` <- does that

#### tfs process related  
- define work request work item and add support for creating them 
- figure out how to make custom task board or kanban
- add function to perform work request aggregations and updates and other housekeeping

#### salesforce
- needs to get more case detail information (last update text, etc)
- `case find Idoc posting` - Gets latest X cases with that text in the title or description

### Dynamics GP
- query dynamics for available timecodes
- report on workitem hours per project code
- anything to ease those workflows
- produce reports or warnings

#### other stuff
- listen for "Its just like" and other verboten phrases and replace them with Kanye quotes, so everyone 
    gets that its bad. Or maybe related examples of why they are forbidden.
- plant or nomnoml uml rendering