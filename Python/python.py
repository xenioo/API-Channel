import requests
import json

# Global user-id. Either start with empty or have a unique-id strategy
userid = ""

# Bot reply parts display
def write( parts ):
  for part in parts:
    if( part["Text"] != None ):
      print( "\t" + part["Text"] )
  
  print( "" )
  return

# Default chat request
def chat( text ):
  data = "{ \"Text\": \"" + text + "\" }"
  return requests.post( 'https://app.xenioo.com/apihook/chat' , headers = headers( userid ), data = data ).json()

def status():
  ret = requests.get( 'https://app.xenioo.com/apihook/status' , headers = headers( userid ) ).json()
  
  print( "\tContext=" + ret["Context"]["BehaviorName"] + " // " + ret["Context"]["InteractionName"] )
  print( "" )
  
  for variable in ret["Variables"]:
    if( variable["Value"] == None ):
      print( "\t" + variable["Name"] + "= ** None **" )
    else:
      print( "\t" + variable["Name"] + "=" + variable["Value"] )
      
  print( "" )

# Default headers. The bearer authorization key should be changed to YOUR chatbot API Key
def headers( id ):
  if( id != "" ):
    return {"content-type":"application/json", "Authorization":"Bearer WAtiJ8fBsHK0", "user-id" : id }
  else:
    return {"content-type":"application/json", "Authorization":"Bearer WAtiJ8fBsHK0"}

print( "" )
print( "--------------------------------------" )
print( "This is an interactive API Bot example" )
print( "Type quit to leave" )
print( "Type status to get the list of live variables" )
print( "--------------------------------------" )
print( "" )

# Retrieve the very first welcome message
data = chat( "" )
userid = data["UserId"]
write( data["Parts"] )

while( True ):
  text = raw_input( ">>> " ) 
  #leave loop
  if( text == "quit" ):
    break
    
  if( text == "status" ):
    status()
    continue
    
  data = chat( text )
  write( data["Parts"] )
  
