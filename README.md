# API-Channel

The Xenioo API Channel allows any application capable of making simple RESTful https calls to integrate and interact with a <a href='https://www.xenioo.com' target="_blank">Xenioo</a> chatbot. 

Using the Xenioo API Channel you will be controlling the way the conversation will be displayed to the user as well as <a href='https://www.xenioo.com/changing-conversation-flow/' target="_blank">being able to change runtime variables</a> anytime.

In this repository you can find two simple C# examples of chatbot interaction as well as a python implementation of a shell based chatbot. All of the samples connect to a Xenioo demo chatbot that is always online. To implement your own chatbot you just need to signup for a free account on <a href='https://app.xenioo.com/auth/signup' target="_blank">Xenioo</a>.

Inside the C# samples folder you can also find the full current source of Xenioo API Channel client library that you can reference to your projects as source or directly as a <a href='https://www.nuget.org/packages/Xenioo.Channels.API/1.0.1'>Nuget package from Visual Studio</a>.

## General

Each call made to your Xenioo chatbot must include the following headers:

|Header           |Value            |Description
|-----------------|-----------------|-----------------|
|Authorization    |Bearer [APIKEY]  | This is the API Key you can copy from your Xenioo API publishing dialog. |
|Content-Type     |application/json | All data exchanged with Xenioo must be of this type |
|user-id          |[any string]     | This is the current chat user id. If no value is specified, Xenioo will create a new user|

## Retrieving Configuration

You chatbot basic configuration, as specified in your Xenioo designer, can be retrieved using the **config** endpoint as follows:

```shell
curl -X GET \
  https://app.xenioo.com/apihook/config \
  -H 'Authorization: Bearer [APIKEY]' \
```

Since this is a global configuration, you don't need to specify the current user-id here. Xenioo reply to this request will be similar to this one:

```javascript
{
    "Name": "My Awesome Bot",
    "EnableTypeSpeed": true,
    "WordsPerMinute": 800,
    "Avatar": "https://app.xenioo.com/api/assets/8e23ff8b3ee7_60f1113c-8e6e-46ce-bf19-a53da5ff4ed0.jpg",
    "Version": 96,
    "DefaultBehaviour": {
        "Name": "My Top Behaviour",
        "APIKey": "[Behaviour API Token]"
    }
}
```

|field|type|description|
|-|-|-|
|Name|string|The name of your chatbot|
|EnableTypeSpeed|boolean|Indicates if the chatbot is configured to use typespeed simulation. If so, TypeDelay will be valued for actions where necessary|
|WordsPerMinute|number|The number of words per minute your chatbot can write. This affects the TypeDelay parameter value|
|Avatar|string|The url of the avatar specified under your chatbot general settings|
|Version|number|The version number of your chatbot. This number is automatically increased by Xenioo|
|DefaultBehaviour|object|The name and the API Key associated to your default chatbot Behaviour. You can use the API Key to forcefully redirect your chatbot conversation to another Behaviour| 


## Initiating Chat

The fist connection to your chatbot chat must be done by calling the **chat** endpoint using the previously specified headers. 

This endpoint, called without sending any data will reset the conversation to the starting point. If the user-id is specified in this call, the conversation will be reset but historic conversation will be kept in the Xenioo conversation history interface.

```shell
curl -X POST \
  https://app.xenioo.com/apihook/chat \
  -H 'Authorization: Bearer [APIKEY]' \
  -H 'Content-Type: application/json' \
  -H 'user-id: some-user-id'
```

If you wish instead to continue a previous conversation with a know user you can add the READY command to the request as below. In this case, Xenioo will not reset the conversation and return the full history as first reply.

```shell
curl -X POST \
  https://app.xenioo.com/apihook/chat \
  -H 'Authorization: Bearer [APIKEY]' \
  -H 'Content-Type: application/json' \
  -H 'user-id: some-user-id' \
  -d '{
	"Command":"READY"
}'
```

A succesfull reply from Xenioo may look like this:

```javascript
{
    "Parts": [
        {
            "Type": 0,
            "Text": "Hello...I am Xenioo 9000.",
            "TypeDelay": 300,
            "BehaviourName": "New Bot Behaviour",
            "InteractionName": "Start Interaction",
            "Parts": []
        }
    ],
    "UserId": "some-user-id",
    "Creation": "2018-10-05T14:43:43.7057755+01:00",
    "EnableUserChat": true,
    "ControlType": 0
}
```

The root reply fields have the follwing format:

|field|type|description|
|-|-|-|
|Parts|array|This is an array of 1 or more chat parts. Each chat part is usually a Xenioo Action Result|
|UserId|string|The user-id of the current user chatting with you. If you did not create it yourself store it and re-use it for subsequent calls|
|Creation|datetime|The Xenioo reply creation date and time |
|EnableUserChat|boolean|This may change depending on your chatbot design. You should comply to the chabot designer choices by either allowing or forbidding an open reply from the user|
|ControlType|number|The control state of the chatbot. 0-Xenioo, 1-Operator Requested, 2-Operator Taken Over|

Each part may contain different fields, depending on the type. All general fields are as follows:

|field|type|description|
|-|-|-|
|Type|number|The type of action that should be exectued on your client. See below for a list of all actions|
|Text|string|The text content of the action. If available it should be displayed to the user in some way|
|Command|string|The command payload associated to this action. To execute this action you should send back this payload to Xenioo|
|TypeDelay|number|The delay, in milliseconds, you should wait before displayng the content. Your interface should have a mean of showing a typical typing indicator|
|BehaviourName|string|This is the name of the Behaviour that generated this conversation part|
|InteractionName|string|The interactin, inside the Behaviour that generated this conversation part|
|Parts|array|This array may hierarchically contain more parts, depending on the complexity of the action.|

## Chatting

Once the chat control is given to the user, he can interact with your chatbot in two ways: executing a command or saying something (sending a text).

After acquiring the user input, you can relay it to Xenioo using the same connection endpoint, with the following syntax:

```shell
curl -X POST \
  https://app.xenioo.com/apihook/chat \
  -H 'Authorization: Bearer [APIKEY]' \
  -H 'Content-Type: application/json' \
  -H 'user-id: some-user-id' \
  -d '{
  	"Text":"Hello there!"
  }'
```

Depending on how you've implemented your chatbot reactions and interactions the answer may change but will always be compliant to the previous reply fields. If your user has instead any mean to click on chat buttons you've implemented or on Carousel contents you must forward to Xenioo the command payload as follows:

```shell
curl -X POST \
  https://app.xenioo.com/apihook/chat \
  -H 'Authorization: Bearer [APIKEY]' \
  -H 'Content-Type: application/json' \
  -H 'user-id: some-user-id' \
  -d '{
	"Command":"3131292d-945e-4b6b-9f78-7f5eacebf5b6"
}'
```

Command payloads are always GUID values generated by Xenioo. If the command payload is recognized, the command will trigger and the conversation continue, according to the flow you've designed.

## Variables and Tags

You can update (or create new) variables value or conversation tags using the **status** endpoint like in the example below:

```shell
curl -X POST \
  https://app.xenioo.com/apihook/status \
  -H 'Authorization: Bearer [APIKEY]' \
  -H 'Content-Type: application/json' \
  -H 'user-id: some-user-id' \
  -d '{
	"UpdateType":"[type]",
	"Name":"variable_name",
	"Value":"variable_value"
}'
```

The UpdateType parameter specifies the type of operation to be done on the chatbot conversation. You have four different update types as specified in the table below:

|type|description|
|-|-|
|set-var|Updates or set the variable with name specified in Name and value specified in Value|
|de-var|Drops the variable with name as specified in Name field|
|set-tag|Adds a new tag named Name in the conversation|
|del-tag|Removes the specified tag

Variable changes are immediate and can even alter variables that have been created or changed during last interaction execution.

## Status

Any time during conversation you can retrive the full chatbot status calling the **status** endpoint as in the example below:

```shell
curl -X GET \
  https://app.xenioo.com/apihook/status \
  -H 'Authorization: Bearer [APIKEY]' \
  -H 'Content-Type: application/json' \
  -H 'user-id: some-user-id'
```

Xenioo reply will contain all of the currently valued variables, tags, Privacy flags and context in the following format:

```javascript
{
    "Variables": [
        {
            "Name": "user_name",
            "Value": "User 1372503197"
        },
        [...]
    ],
    "Tags": [
        "new_user"
    ],
    "PrivacyFlags": [
        {
            "Name": "personal_data_processing",
            "Enabled": false
        },
        [...]
    ],
    "Context": {
        "BehaviorName": "My Current Behaviour",
        "InteractionName": "Start Interaction"
    }
}
```

