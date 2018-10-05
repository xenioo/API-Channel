using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xenioo.Channels.API.Model;

namespace TesterConsole {

	public class Display {
		
		public static List<string> Write( BotReply reply ) {

			List<string> commands = new List<string>();
			int buttoncount = 1;

			foreach( var part in reply.Parts ) {

				if( part.TypeDelay > 0 ) {
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.Write( "[...]" );
					System.Threading.Thread.Sleep( part.TypeDelay );
					Console.ForegroundColor = ConsoleColor.Green;
					Console.CursorLeft = 0;
				}

				switch( part.Type ) {
					case StandardPartTypeEnum.Question:
					case StandardPartTypeEnum.Text:
						Console.WriteLine( "\t" + part.Text );
						break;
					case StandardPartTypeEnum.Button:
						Console.Write( "\t[" + buttoncount.ToString() + "]:" + part.Text  );
						buttoncount++;
						commands.Add( part.Command );
						break;
				}

			}

			if( reply.Parts.Any( _ => _.Type == StandardPartTypeEnum.Button ) )
				Console.WriteLine( );

			Console.WriteLine( );

			return commands;

		}

	}

}
