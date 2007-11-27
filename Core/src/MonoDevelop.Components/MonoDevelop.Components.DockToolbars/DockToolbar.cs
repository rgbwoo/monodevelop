//
// DockToolbar.cs
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
using Gtk;
using Gdk;

namespace MonoDevelop.Components.DockToolbars
{
	public class DockToolbar: Toolbar, IDockToolbar
	{
		DockGrip grip;
		DockToolbarFrame parentFrame;
		bool dragging;
		FloatingDock floatingDock;
		string id;
		string title;
		
		int row;
		int offset;
		int shiftOffset = -1;
		
		// Offset requested by the user. It is used to know where the
		// toolbar is expected to move when the window is expanded.
		int anchorOffset;	
		
		Animation animation;
		DockToolbarPosition lastPosition;
		DockToolbarStatus defaultStatus;
		
		int defaultSize;
		int defaultHeight;
		bool gotSize = false;
		
		public DockToolbar (string id, string title)
		{
			grip = new DockGrip ();
			Add (grip);
			this.id = id;
			this.title = title;
			ShowArrow = false;
		}
		
		internal void SetParentFrame (DockToolbarFrame frame)
		{
			parentFrame = frame;
		}
		
		public string Id {
			get { return id; }
		}
		
		public string Title {
			get { return title; }
		}
		
		public bool Floating {
			get { return floatingDock != null; }
		}
		
		bool IDockToolbar.Visible {
			get { return Parent != null; }
			set {
				if (value == ((IDockToolbar)this).Visible)
					return;
					
				if (value) {
					if (lastPosition != null)
						lastPosition.RestorePosition (parentFrame, this);
					else
						defaultStatus.Position.RestorePosition (parentFrame, this);
				} else {
					lastPosition = DockToolbarPosition.Create (this);
					if (Floating) {
						FloatingDock win = FloatingDock;
						win.Detach ();
						win.Destroy ();
					}
					else
						DockPanel.RemoveBar (this);
				}
			}
		}
		
		internal DockToolbarPosition Position {
			get {
				if (((IDockToolbar)this).Visible) return DockToolbarPosition.Create (this);
				else if (lastPosition != null) return lastPosition;
				else return defaultStatus.Position;
			}
		}
		
		internal DockToolbarStatus Status {
			get {
				return new DockToolbarStatus (id, ((IDockToolbar)this).Visible, Position);
			}
			set {
				if (value.Visible) {
					((IDockToolbar)this).Visible = false;
					lastPosition = value.Position;
					((IDockToolbar)this).Visible = true;
				} else {
					((IDockToolbar)this).Visible = false;
					lastPosition = value.Position;
				}
			}
			
		}
		
		internal DockToolbarStatus DefaultStatus {
			get { return defaultStatus; }
			set { defaultStatus = value; }
		}
		
		internal int DockRow {
			get { return row; }
			set { row = value; }
		}
		
		internal int DockOffset {
			get { return offset; }
			set { offset = value; }
		}
		
		internal int DockShiftOffset {
			get { return shiftOffset; }
			set { shiftOffset = value; }
		}
		
		internal int AnchorOffset {
			get { return anchorOffset; }
			set { anchorOffset = value; }
		}
		
		internal int DefaultSize {
			get {
				if (!gotSize) CalcSizes ();
				return defaultSize;
			}
		}
		
		public int DefaultHeight {
			get {
				if (!gotSize) CalcSizes ();
				return defaultHeight;
			}
		}
		
		internal void CalcSizes ()
		{
			// Calculates the real size of the toolbar. ShowArrow=false is
			// needed, since SizeRequest reports 0 size requested if not.
			
			bool olda = ShowArrow;
			ShowArrow = false;
			Requisition r = SizeRequest ();
			if (Orientation == Orientation.Horizontal) {
				defaultSize = r.Width;
				defaultHeight = r.Height;
			}
			else {
				defaultSize = r.Height;
				defaultHeight = r.Width;
			}
			
			ShowArrow = olda;
			gotSize = true;
		}
		
		internal bool CanDockTo (DockToolbarPanel panel)
		{
			return true;
		}
		
		internal FloatingDock FloatingDock {
			get { return floatingDock; }
			set { floatingDock = value; }
		}
		
		internal DockToolbarPanel DockPanel {
			get { return Parent as DockToolbarPanel; }
		}
		
		internal Animation Animation {
			get { return animation; }
			set { animation = value; }
		}
		
		protected override bool OnButtonPressEvent (EventButton e)
		{
			if (e.Button == 1) {
				if (Orientation == Orientation.Horizontal && e.X <= 10) {
					dragging = true;
					parentFrame.StartDragBar (this, (int)e.X, (int)e.Y, e.Time);
					return true;
				}
				else if (Orientation == Orientation.Vertical && e.Y <= 10) {
					dragging = true;
					parentFrame.StartDragBar (this, (int)e.X, (int)e.Y, e.Time);
					return true;
				}
			}
			return base.OnButtonPressEvent (e);
		}
		
		protected override bool OnButtonReleaseEvent (EventButton e)
		{
			if (e.Button == 1 && dragging) {
				dragging = false;
				parentFrame.EndDragBar (this, e.Time);
			}
			return base.OnButtonReleaseEvent (e);
		}
		
		protected override bool OnExposeEvent (EventExpose evnt)
		{
		    GdkWindow.DrawRectangle (Style.BackgroundGC (State), true, Allocation);
            
            foreach (Widget child in Children) {
                PropagateExpose (child, evnt);
            }
		
		    return true;
		}

		internal void ResetSize ()
		{
			WidthRequest = -1;
			HeightRequest = -1;
			ShowArrow = false;
		}
		
		public int Size {
			set {
				if (Orientation == Orientation.Horizontal) {
					if (value >= DefaultSize)
						WidthRequest = -1;
					else
						WidthRequest = value;
				}
				else {
					if (value >= DefaultSize)
						HeightRequest = -1;
					else
						HeightRequest = value;
					HeightRequest = value;
				}
			}
			get {
				if (Orientation == Orientation.Horizontal)
					return SizeRequest ().Width;
				else
					return SizeRequest ().Height;
			}
		}
		
		public new Orientation Orientation {
			get {
				return base.Orientation;
			}
			set {
				if (value == base.Orientation)
					return;
				
				if (FloatingDock != null) {
					// I create a new dock window because resizing the
					// current one has lots of issues (mainly synchronization
					// problems between the size change of the window and
					// the toolbar).
					
					int x,y;
					FloatingDock w = FloatingDock;
					w.GetPosition (out x, out y);
					w.Detach ();
					
					base.Orientation = value;
					
					FloatingDock fdock = new FloatingDock (parentFrame);
					fdock.Move (x, y);
					fdock.Attach (this);
					w.Destroy ();
				} else
					base.Orientation = value;
				gotSize = false;
			}
		}
		
		protected override void OnAdded (Widget w)
		{
			base.OnAdded (w);
			gotSize = false;
			if (DefaultSizeChanged != null)
				DefaultSizeChanged (this, EventArgs.Empty);
			w.Shown += new EventHandler (OnItemChange);
			w.Hidden += new EventHandler (OnItemChange);
		}
		
		protected override void OnRemoved (Widget w)
		{
			base.OnRemoved (w);
			gotSize = false;
			if (DefaultSizeChanged != null)
				DefaultSizeChanged (this, EventArgs.Empty);
			w.Shown -= new EventHandler (OnItemChange);
			w.Hidden -= new EventHandler (OnItemChange);
		}
		
		void OnItemChange (object o, EventArgs args)
		{
			// This notifies changes in the size of the toolbar
			gotSize = false;
			if (DefaultSizeChanged != null)
				DefaultSizeChanged (this, EventArgs.Empty);
		}
		
		internal event EventHandler DefaultSizeChanged;
	}
}

