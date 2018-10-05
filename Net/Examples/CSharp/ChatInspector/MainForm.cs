using ChatInspector.GridUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xenioo.Channels.API;

namespace ChatInspector {
	public partial class MainForm : Form {

		private const string	XENIOO_API_KEY		= "WAtiJ8fBsHK0";
		private Connection      _xeniooConnection   = null;

		#region .ctor

		public MainForm( ) {
			InitializeComponent( );
		}

		#endregion

		private void OnFormShown( object sender, EventArgs e ) {
			_xeniooConnection = new Connection( XENIOO_API_KEY, 5 );
			_xeniooConnection.MessagesIncoming += OnNewMessagesIncoming;
			_chatLog.AppendText( Display.Write( _xeniooConnection.Initialize( ) ) );
			_chatText.Focus( );

			_chatState.SelectedObject = new GridBotState( _xeniooConnection.GetState( ) );
			_configData.SelectedObject = new GridBotConfiguration( _xeniooConnection.GetConfiguration( ) );

			this.Text += " --- " + ((GridBotConfiguration) _configData.SelectedObject).Name;

		}

		private void OnNewMessagesIncoming( Connection sender, Xenioo.Channels.API.Model.BotReply reply ) {
			SetChatText( Display.Write( reply ) );
		}

		private void OnChatTextKeyDown( object sender, KeyEventArgs e ) {
			if( e.KeyCode != Keys.Enter || _chatText.Text.Trim().Length == 0 )
				return;

			_chatLog.AppendText( "\t>>>> " + _chatText.Text + "\r\n\r\n" );

			_chatLog.AppendText( Display.Write( _xeniooConnection.Say( _chatText.Text ) ) );
			_chatText.Text = string.Empty;
			_chatText.Focus( );

			_chatState.SelectedObject = new GridBotState( _xeniooConnection.GetState( ) );

		}

		delegate void SetChatTextCallback( string text );

		private void SetChatText( string text ) {
			if( _chatLog.InvokeRequired ) {
				SetChatTextCallback callback = new SetChatTextCallback(SetChatText);
				this.Invoke( callback, new object[] { text } );
			} else {
				_chatLog.AppendText( text );
			}
		}

	}
}
