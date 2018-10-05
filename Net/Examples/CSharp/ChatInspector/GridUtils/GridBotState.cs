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
	public class GridBotState {

		public GridBotState( BotState state ) {
			this.Variables = state.Variables;
			this.Tags = state.Tags;
			this.PrivacyFlags = state.PrivacyFlags;
			this.Context = state.Context;
		}

		[TypeConverter( typeof(ExpandableObjectConverter))]
		public List<Variable> Variables {
			get;
			set;
		}

		[TypeConverter( typeof( ExpandableObjectConverter ) )]
		public List<string> Tags {
			get;
			set;
		}

		[TypeConverter( typeof( ExpandableObjectConverter ) )]
		public List<Flag> PrivacyFlags {
			get;
			set;
		}

		[TypeConverter( typeof( ExpandableObjectConverter ) )]
		public BotStateContext Context {
			get;
			set;
		}

	}

}
