using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xenioo.Channels.API.Model;

namespace ChatInspector {

	public class Display {
		
		public static string Write( BotReply reply ) {

			StringBuilder bld = new StringBuilder();

			bld.Append( "\t" + DateTime.Now.ToString( ) + "\r\n" );

			foreach( var part in reply.Parts ) {
				
				switch( part.Type ) {
					case StandardPartTypeEnum.Question:
					case StandardPartTypeEnum.Text:
						bld.Append( "\t" + part.Text.Replace( "\n", "\r\n\t" ) + "\r\n" );
						break;
					case StandardPartTypeEnum.Button:
						bld.Append( "\t[" + part.Text + "]\r\n" );
						break;
				}

			}

			bld.Append( "\r\n" );
			return bld.ToString( );
		}

	}

}
