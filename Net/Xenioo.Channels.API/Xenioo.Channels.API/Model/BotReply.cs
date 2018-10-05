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

namespace Xenioo.Channels.API.Model {

	public enum EntryControlTypeEnum {
		Chatbot,
		Operator
	}

	public enum StandardPartTypeEnum {
		Text,
		Button,
		QuickLink,
		Image,
		Typing,
		GenericCard,
		Question,
		DefaultDisplay,
		FlowJump,
		Video,
		Audio,
		File,
		ListCard,
		ButtonsCard,
		CardElement,
		Execution,
		HideParent,
		URL,
		Flatten,
		UserText,  
		FlowJumpEnd,
		StopExecution,
		PhoneNumberButton,
		EmailButton,
		LocationButton,
		ChatDelay,
		Composite,
		ClientScript
	}

	public class BotReply {

		public List<BotReplyPart> Parts {
			get;
			set;
		}

		public string UserId {
			get;
			set;
		}

		public DateTime Creation {
			get;
			set;
		}

		public bool EnableUserChat {
			get;
			set;
		}

		public EntryControlTypeEnum ControlType {
			get;
			set;
		}

	}

	public class BotReplyPart {

		public StandardPartTypeEnum Type {
			get;
			set;
		}

		public string SubType {
			get;
			set;
		}

		public string Text {
			get;
			set;
		}

		public string Command {
			get;
			set;
		}

		public int TypeDelay {
			get;
			set;
		}

		public string BehaviourName {
			get;
			set;
		}

		public string InteractionName {
			get;
			set;
		}

		public List<BotReplyPart> Parts {
			get;
			set;
		}

	}

}
