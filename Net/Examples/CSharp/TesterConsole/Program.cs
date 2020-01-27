using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xenioo.Channels.API;

namespace TesterConsole {
	class Program {

		private const string XENIOO_API_KEY = "YOUR CHATBOT API KEY";
		

		static void Main( string[] args ) {
			
			Console.ForegroundColor = ConsoleColor.Green;
			Banner.Write( );
			
			var connection = new Connection( XENIOO_API_KEY );

			var commands = Display.Write( connection.Initialize( ) );
			
			while( true ) {

				Console.Write( " >>> " );

				string userinput = Console.ReadLine();
				Console.WriteLine( );

				int commandindex = 0;
				if( int.TryParse( userinput, out commandindex ) ) {
					commandindex--;
					if( commandindex >= 0 && commandindex < commands.Count )
						commands = Display.Write( connection.Command( commands[ commandindex ] ) );
					else
						commands = Display.Write( connection.Say( userinput ) );
				} else
					commands = Display.Write( connection.Say( userinput ) );

			}
			

		}
		
	}
}
