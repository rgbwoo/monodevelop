// LocalsPad.cs
//
// Author:
//   Lluis Sanchez Gual <lluis@novell.com>
//
// Copyright (c) 2008 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//

using System;
using System.Linq;
using Mono.Debugging.Client;

namespace MonoDevelop.Debugger
{
	public class LocalsPad: ObjectValuePad
	{
		StackFrame lastFrame;
		
		public LocalsPad()
		{
			tree.AllowEditing = true;
			tree.AllowAdding = false;
		}

		public override void OnUpdateList ()
		{
			try {
				base.OnUpdateList ();
				StackFrame frame = DebuggingService.CurrentFrame;
				if (null != frame && !FrameEquals (frame, lastFrame)) {
					tree.ClearValues ();
					tree.AddValues (frame.GetAllLocals ());
					lastFrame = frame;
				} else {
					tree.Update ();
				}
			} catch (Exception ex) {
				MonoDevelop.Core.LoggingService.LogError ("Error updating locals", ex);
			}
		}
		
		static bool FrameEquals (StackFrame a, StackFrame z)
		{
			if (null == a || null == z)
				return a == z;
			return a.SourceLocation.Filename.Equals (z.SourceLocation.Filename, StringComparison.Ordinal) &&
			       a.SourceLocation.Method.Equals (z.SourceLocation.Method, StringComparison.Ordinal);
		}
	}
}
