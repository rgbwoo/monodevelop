//
// CommandArrayInfo.cs
//
// Author:
//   Lluis Sanchez Gual
//
// Copyright (C) 2005 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.Collections;

namespace MonoDevelop.Components.Commands
{
	public class CommandArrayInfo: IEnumerable
	{
		ArrayList list = new ArrayList ();
		CommandInfo defaultInfo;
		bool bypass;
		
		internal CommandArrayInfo (CommandInfo defaultInfo)
		{
			this.defaultInfo = defaultInfo;
		}
		
		public void Add (CommandInfo info, object dataItem)
		{
			info.DataItem = dataItem;
			if (info.Text == null) info.Text = defaultInfo.Text;
			if (info.Icon == null) info.Icon = defaultInfo.Icon;
			list.Add (info);
		}

		public CommandInfo Add (string text, object dataItem)
		{
			CommandInfo info = new CommandInfo (text);
			Add (info, dataItem);
			return info;
		}
		
		public void AddSeparator ()
		{
			CommandInfo info = new CommandInfo ("-");
			info.IsArraySeparator = true;
			Add (info, null);
		}

		public CommandInfo DefaultCommandInfo {
			get { return defaultInfo; }
		}
		
		public IEnumerator GetEnumerator ()
		{
			return list.GetEnumerator ();
		}
		
		// When set in an update handler, the command manager will ignore this handler method
		// and will keep looking in the command route.
		public bool Bypass {
			get { return bypass; }
			set { bypass = value; }
		}
	}
}
