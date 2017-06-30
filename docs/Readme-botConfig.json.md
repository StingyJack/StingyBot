# botConfig.json
_This may be slightly out of date as its a work in progress_

There are three significant parts... 

- **"slackConfig"** is a single property at the moment
- **"hears"** are things the bot actively listens for and has to perform actions in order to respond.
- **"knows"** are static text strings/json payloads. This is for simpler, non-action required utterances.

#### slackConfig
Put the **Bot** ApiKey in here. 

For local development, create a botConfig.json.secret file (in bin/Debug of the host app)
and put this snippet in it to override the default non-working value. This can be done for any value 
you want to override.

```
{
  "slackConfig": {
    "ApiKey": "There are many like it, but this one is yours"
  }
}
```


#### hears


- **"name"** - a non repeating key for the hear
- **"evaluationPriority"** - the order of evaluation for hears. See (hearing priority)[### hearingpriority] below for more info
- **"triggerType"** - `directMention` requires the bot to be named in order to respond.  `slashCommand` is a /command
- **"handlerType"** is "messageHandler" or "slashCommand"
- **"semanticReplacementTokens"** are the word replacement tokens. These are a simple way to help identify or 
derive user intent.  
- **"handlerDef"** are loaded and instantiated at startup by the handlerFullTypeName, 
  - **responseType** "**messageHandler**" must implement `StingyBot.Common.IMessageHandler`
  - **responseType** "**slashCommand**" must implement `StingyBot.Common.ICommandHandler`


#### "knows"
These are basically static text to show for preconfigured conditions (help, nogrok, yermum, etc)

 
### hearing priority
For evaluating which handler to pass the message token to, the following rules are applied. 

1) The `Hears` are ordered by the evaluationPriority ASC _(0,1,2,etc)_. 
2) The first `Hears` that matches all tokens is the winner and the evalution is halted.
3) If no "All Token" matches are found after iterating through the `Hears`, the first 
   `Hear` (by evaluation priority) that had a "Some Token" result is selected.

### example config



``` json
{
  "slackConfig": {
    "ApiKey": "From the bot integration page. Probably called API Token now"
  },
  "hears": [
    {
      "name": "tfsWorkitems",
      "enabled": true,
      "evaluationPriority": 1,
      "triggerType": "directMention",
      "handlerType": "messageHandler",
      "semanticReplacementTokens": [
        {
            "sourceWord": "wi",
            "coalescedReplacement": "WORKITEM"
            "isRequired" : true
        },
        {
            "sourceWord": "pbi",
            "coalescedReplacement": "WORKITEM"
        },
        {
            "sourceWord": "bg",
            "coalescedReplacement": "WORKITEM"
        },
        {
            "sourceWord": "tk",
            "coalescedReplacement": "WORKITEM"
        }
      ],
      "handlerDef": {
        "handlerFullTypeName": "StingyBot.Tfs.TfsWorkItemController, StingyBot.Tfs",
        "configFiles": [ "tfsconfiguration.json", "tfsconfiguration.json.secret" ]
      }
    }
  ],  
  "knows": [
    {
      "key": "unknownCommand",
      "enabled": true,
      "staticText": "You sent a command which I don't understand"
    },
    {
      "key": "defaultHelp",
      "enabled": true,
      "message": {
        "Attachments": [
          {
            "fallback": "known commands",
            "color": "#36a64f",
            "pretext": "I know the following commands",
            "author_name": null,
            "author_link": null,
            "author_icon": null,
            "title": null,
            "title_link": null,
            "text": "*case* _#####_ - Displays details for a salesforce case \n`case list` - Displays the last 5 cases n *tfs wi* _#####_ - Displays details for a TFS workitem \n*tfs wiql* \"_SELECT System.ID FROM WorkItems\" - Displays details for a TFS workitem \n",
            "fields": [],
            "callback_id": null,
            "actions": null,
            "image_url": null,
            "thumb_url": null,
            "mrkdwn_in": [ "fields", "pretext", "text" ]
          }
        ],
        "Text": "How may I enable you today? n_Don't forget to mention me before each command_ \n"
      }
    }
  ]

}


```
<div style="border: 1px solid black">
    <p style="font-size:0.5em">Look ma! html in muh markdown</p>
</div>