# API-Channel

The Xenioo API Channel allows any application capable of making simple RESTful https calls to integrate and interact with a <a href='https://www.xenioo.com' target="_blank">Xenioo</a> chatbot. 

Using the Xenioo API Channel you will be controlling the way the conversation will be displayed to the user as well as <a href='https://www.xenioo.com/changing-conversation-flow/' target="_blank">being able to change runtime variables</a> anytime.

In this repository you can find two simple C# examples of chatbot interaction as well as a python implementation of a shell based chatbot. All of the samples connect to a Xenioo demo chatbot that is always online. To implement your own chatbot you just need to signup for a free account on <a href='https://app.xenioo.com/auth/signup' target="_blank">Xenioo</a>.

## General

Each call made to your Xenioo chatbot must include the following headers:

|Header           |Value            |Description
|-----------------|-----------------|-----------------|
|Authorization    |Bearer [APIKEY]  | This is the API Key you can copy from your Xenioo API publishing dialog. |
|Content-Type     |application/json | All data exchanged with Xenioo must be of this type |
|user-id          |[any string]     | This is the current chat user id. If no value is specified, Xenioo will create a new user|


## Connection

The fist connection to your chatbot must be done by calling the https://app.xenioo.com/apihook/chat endpoint using the previously specified headers. 

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

An example succesfull reply from Xenioo may look like this:

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
|Parts|array|This array may hierarchically contain more parts, depending on the complexity of the action.|

















## Chat


