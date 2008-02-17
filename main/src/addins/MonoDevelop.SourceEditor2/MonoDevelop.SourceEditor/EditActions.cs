// EditActions.cs
//
// Author:
//   Mike Krüger <mkrueger@novell.com>
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

using System;
using Mono.TextEditor;

namespace MonoDevelop.SourceEditor
{
	public class TabAction : InsertTab
	{
		ExtendibleTextEditor editor;
		
		public TabAction (ExtendibleTextEditor editor)
		{
			this.editor = editor;
		}
		
		public override void Run (TextEditorData data)
		{
			if (!editor.DoInsertTemplate ())
				base.Run (data);
		}
	}
	
	public class AdvancedBackspaceAction : BackspaceAction
	{
		const string open    = "'\"([{<";
		const string closing = "'\")]}>";
		
		int GetNextNonWsCharOffset (TextEditorData data, int offset)
		{
			int result = offset;
			while (Char.IsWhiteSpace (data.Document.GetCharAt (result))) {
				result++;
				if (result >= data.Document.Length)
					return -1;
			}
			return result;
		}
		
		protected override void RemoveCharBeforCaret (TextEditorData data)
		{
			char ch = data.Document.GetCharAt (data.Caret.Offset - 1);
			int idx = open.IndexOf (ch);
			if (idx >= 0) {
				int nextCharOffset = GetNextNonWsCharOffset (data, data.Caret.Offset);
				if (nextCharOffset >= 0 && closing[idx] == data.Document.GetCharAt (nextCharOffset)) {
					bool updateToEnd = data.Document.OffsetToLineNumber (nextCharOffset) != data.Caret.Line;
					data.Document.Remove (data.Caret.Offset, nextCharOffset - data.Caret.Offset + 1);
					if (updateToEnd)
						data.Document.CommitLineToEndUpdate (data.Caret.Line);
				}
			}
			base.RemoveCharBeforCaret (data);
		}
	}
}
