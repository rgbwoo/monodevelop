//
// UnknownCombineEntry.cs
//
// Author:
//   Lluis Sanchez Gual
//
// Copyright (C) 2006 Novell, Inc (http://www.novell.com)
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
using MonoDevelop.Core;

namespace MonoDevelop.Projects
{
	public class UnknownCombineEntry: CombineEntry
	{
		public UnknownCombineEntry()
		{
		}
		
		public override IConfiguration CreateConfiguration (string name)
		{
			return null;
		}
		
		protected internal override void OnClean ()
		{
		}
		
		protected internal override ICompilerResult OnBuild (IProgressMonitor monitor)
		{
			return null;
		}
		
		protected internal override void OnExecute (IProgressMonitor monitor, ExecutionContext context)
		{
		}
		
		protected internal override bool OnGetNeedsBuilding ()
		{
			return false;
		}
		
		protected internal override void OnSetNeedsBuilding (bool value)
		{
		}
	}
}
