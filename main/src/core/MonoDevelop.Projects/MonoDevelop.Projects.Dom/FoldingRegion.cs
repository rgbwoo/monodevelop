// FoldingRegion.cs
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
//

using System;

namespace MonoDevelop.Projects.Dom
{
	public class FoldingRegion
	{
		string name;
		public string Name {
			get {
				return name;
			}
			set {
				name = value;
			}
		}
		
		bool defaultIsFolded;
		public bool DefaultIsFolded {
			get {
				return defaultIsFolded;
			}
			set {
				defaultIsFolded = value;
			}
		}
		
		DomRegion region;
		public DomRegion Region {
			get {
				return region;
			}
			set {
				region = value;
			}
		}
		
		public FoldingRegion (string name, DomRegion region) : this (name, region, false)
		{
		}
		
		public FoldingRegion (string name, DomRegion region, bool defaultIsFolded)
		{
			this.name = name;
			this.region = region;
			this.defaultIsFolded = defaultIsFolded;
		}
	}
}
