// 
// SimpleCompletionData.cs
// 
// Author:
//   Michael Hutchinson <mhutchinson@novell.com>
// 
// Copyright (C) 2008 Novell, Inc (http://www.novell.com)
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
using System.Collections.Generic;
using System.Linq;

namespace MonoDevelop.Projects.Gui.Completion
{
	
	public class CompletionData : ICompletionData
	{
		public CompletionData (string text) : this (text, null, null) {}
		public CompletionData (string text, string icon) : this (text, icon, null) {}
		public CompletionData (string text, string icon, string description) : this (text, icon, description, text) {}
		
		public CompletionData (string displayText, string icon, string description, string completionText)
		{
			this.DisplayText = displayText;
			this.Icon = icon;
			this.Description = description;
			this.CompletionText = completionText;
		}
		
		public string Icon { get; set; }
		public string DisplayText { get; set; }
		public string Description { get; set; }
		public string CompletionText { get; set; }
		public DisplayFlags DisplayFlags { get; set; }
	}
	
	public class SimpleCompletionDataProvider : ICompletionDataProvider
	{
		public ICompletionData[] Data { get; set; }
		public bool AutoCompleteUniqueMatch { get; set; } 
		
		ICompletionData[] ICompletionDataProvider.GenerateCompletionData (ICompletionWidget widget, char charTyped)
		{
			return Data;
		}
		
		void IDisposable.Dispose ()
		{
		}
		
		public string DefaultCompletionString { get; set; }
		
		public SimpleCompletionDataProvider (ICompletionData [] data, string defaultVal)
		{
			Data = data;
			DefaultCompletionString = defaultVal;
		}
	}
	
	public class LazyCompletionDataProvider : ICompletionDataProvider
	{
		public Func<ICompletionWidget, char, IEnumerable<ICompletionData>> Func { get; set; }
		public string DefaultCompletionString { get; set; }
		public bool AutoCompleteUniqueMatch { get; set; } 
		
		public LazyCompletionDataProvider (Func<ICompletionWidget, char,
		                                   IEnumerable<ICompletionData>> func, string defaultVal)
		{
			this.Func = func;
			this.DefaultCompletionString = defaultVal;
		}
		
		public LazyCompletionDataProvider (Func<ICompletionWidget, char, IEnumerable<ICompletionData>> func)
			: this (func, null)
		{
		}
		
		public LazyCompletionDataProvider (Func<IEnumerable<ICompletionData>> func, string defaultVal)
			: this ((ICompletionWidget x, char y) => func (), defaultVal)
		{
		}
		
		public LazyCompletionDataProvider (Func<IEnumerable<ICompletionData>> func)
			: this (func, null)
		{
		}
		
		ICompletionData[] ICompletionDataProvider.GenerateCompletionData (ICompletionWidget widget, char charTyped)
		{
			return Func (widget, charTyped).ToArray ();
		}
		
		void IDisposable.Dispose ()
		{
		}
	}
}
