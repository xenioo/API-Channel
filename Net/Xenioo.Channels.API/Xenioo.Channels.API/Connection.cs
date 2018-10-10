/*******************************************************

MIT License

Copyright (c) 2018 Matelab Srl

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

********************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xenioo.Channels.API.Model;
using Xenioo.Channels.API.Utils;

namespace Xenioo.Channels.API {

	public delegate void BotMessagesIncomingHandler( Connection sender, BotReply reply );
	
	public class Connection : IDisposable {

		public event BotMessagesIncomingHandler MessagesIncoming;

		public enum ConnectionInitializationModeEnum {
			Reset,
			Ready,
			Pending
		}

		private const string	API_ENDPOINT_URL	= "https://app.xenioo.com/apihook";
		private bool            _Initialized        = false;
		private Timer           _pollingTimer       = null;

		#region .ctor

		public Connection( string apikey ) : this( apikey, string.Empty, 0 ) {}

		public Connection( string apikey, int pollinterval ) : this( apikey, string.Empty, pollinterval ) { }

		public Connection( string apikey, string userid, int pollinterval ) {
			this.APIKey = apikey;
			this.UserId = userid;

			//If no userid is specified, a new userid is created
			//A new userid means a that a brand new conversation is started.
			//If you need persistence in conversation, implement your own userid store mechanism
			if( string.IsNullOrEmpty( this.UserId ) )
				this.UserId = Guid.NewGuid( ).ToString( );

			if( pollinterval != 0 ) {
				PollingInterval = pollinterval;
				_pollingTimer = new Timer( new TimerCallback( PollChat ), null, PollingInterval * 1000, Timeout.Infinite );

			}
			

		}

		public void Dispose( ) { }

		#endregion

		#region Init

		public BotReply Initialize( ) {
			return Initialize( ConnectionInitializationModeEnum.Reset );
		}

		public BotReply Initialize( ConnectionInitializationModeEnum mode ) {
			return Initialize( mode, null, null );
		}

		public BotReply Initialize( ConnectionInitializationModeEnum mode, List<string> tags, List<Variable> variables ) {

			/*
			 Initializes the conversation, sending the starting command.
			 Check https://github.com/xenioo/API-Channel for a list of starting commands.
			 */

			var ret = REST.Post<BotChatRequest,BotReply>( API_ENDPOINT_URL + "/chat",
														  new BotChatRequest(){
																Command = mode.ToString().ToUpper()
														  },
														  GetBaseHeaders( tags, variables  ) );

			this.UserId = ret.UserId;
			_Initialized = true;
			return ret;
		}

		#endregion

		#region props

		public int PollingInterval {
			get;
			set;
		}

		public string APIKey {
			get;
			set;
		}

		public string UserId {
			get;
			set;
		}

		#endregion

		#region Say

		public BotReply Command( string command ) {
			return Say( new BotChatRequest( ) { Command = command } );
		}

		public BotReply Say( string text ) {
			return Say( new BotChatRequest( ) { Text = text } );
		}

		public BotReply Say( BotChatRequest request ){
			return REST.Post<BotChatRequest, BotReply>( API_ENDPOINT_URL + "/chat", request, GetBaseHeaders( ) );
		}

		#endregion

		#region Status / Config

		public BotState GetState( ) {
			return REST.Get<BotState>( API_ENDPOINT_URL + "/status", GetBaseHeaders( ) );
		}

		public BotConfiguration GetConfiguration( ) {
			return REST.Get<BotConfiguration>( API_ENDPOINT_URL + "/config", GetBaseHeaders( ) );
		}

		#endregion

		#region Variables/Tags Set/Drop

		public void SetVariable( string name, string value ) {
			SetProperty( "set-var", name, value );
		}

		public void DropVariable( string name ) {
			SetProperty( "del-var", name, string.Empty );
		}

		public void SetTag( string tag ) {
			SetProperty( "set-tag", tag, string.Empty );
		}

		public void DropTag( string tag ) {
			SetProperty( "del-tag", tag, string.Empty );
		}

		internal void SetProperty( string updatetype, string name, string value ) {
			REST.Post<BotDataUpdateRequest, object>( API_ENDPOINT_URL + "/status",
													 new BotDataUpdateRequest( ) {
														Name = name,
														Value = value,
														UpdateType = updatetype
													 },
													 GetBaseHeaders( ) );
		}


		#endregion

		#region Polling

		public void PollChat( object state ) {

			if( !_Initialized ) {
				_pollingTimer.Change( PollingInterval * 1000, Timeout.Infinite );
				return;
			}

			var ret = REST.Post<BotChatRequest,BotReply>( API_ENDPOINT_URL + "/chat",
														  new BotChatRequest(){
															  Command = "PENDING"
														  },
														  GetBaseHeaders(   ) );

			if( ret != null &&
				ret.Parts.Count > 0 )
				MessagesIncoming?.Invoke( this, ret );

			_pollingTimer.Change( PollingInterval * 1000, Timeout.Infinite );
		}

		#endregion

		#region Global

		private Dictionary<string, string> GetBaseHeaders( ) {
			return GetBaseHeaders( null, null );
		}

		private Dictionary<string, string> GetBaseHeaders( List<string> tags, List<Variable> variables ) {
			var dict = new Dictionary<string, string> {
				{ "user-id", UserId },
				{ "Authorization", "Bearer " + this.APIKey }
			};

			return dict;
		}

		#endregion
		
	}
	
}
