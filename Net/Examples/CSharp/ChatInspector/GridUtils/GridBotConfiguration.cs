using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xenioo.Channels.API.Model;

namespace ChatInspector.GridUtils {

	/*
		Utility class to have expandable properties in property grid 
	*/
	public class GridBotConfiguration {

		public GridBotConfiguration( BotConfiguration configuration ) {
			this.Name = configuration.Name;
			this.EnableTypeSpeed = configuration.EnableTypeSpeed;
			this.Avatar = configuration.Avatar;
			this.Version = configuration.Version;
			this.DefaultBehaviour = configuration.DefaultBehaviour;
		}

		public string Name {
			get;
			set;
		}

		public bool EnableTypeSpeed {
			get;
			set;
		}

		public int WordsPerMinute {
			get;
			set;
		}

		public string Avatar {
			get;
			set;
		}

		public string Version {
			get;
			set;
		}

		[TypeConverter( typeof( ExpandableObjectConverter ) )]
		public ConfigurationDefaultBehaviour DefaultBehaviour {
			get;
			set;
		}
		

	}

}
