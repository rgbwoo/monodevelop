
// This file has been generated by the GUI designer. Do not modify.
namespace MonoDevelop.Ide.Execution
{
	internal partial class MonoExecutionParametersWidget
	{
		private global::Gtk.HBox hbox1;
		private global::MonoDevelop.Components.PropertyGrid.PropertyGrid propertyGrid;
		private global::Gtk.VBox vbox4;
		private global::Gtk.Button buttonReset;
		private global::Gtk.Button buttonPreview;
		
		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget MonoDevelop.Ide.Execution.MonoExecutionParametersWidget
			global::Stetic.BinContainer.Attach (this);
			this.Name = "MonoDevelop.Ide.Execution.MonoExecutionParametersWidget";
			// Container child MonoDevelop.Ide.Execution.MonoExecutionParametersWidget.Gtk.Container+ContainerChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			this.hbox1.BorderWidth = ((uint)(6));
			// Container child hbox1.Gtk.Box+BoxChild
			this.propertyGrid = new global::MonoDevelop.Components.PropertyGrid.PropertyGrid ();
			this.propertyGrid.Name = "propertyGrid";
			this.propertyGrid.ShowToolbar = false;
			this.propertyGrid.ShowHelp = true;
			this.hbox1.Add (this.propertyGrid);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.propertyGrid]));
			w1.Position = 0;
			// Container child hbox1.Gtk.Box+BoxChild
			this.vbox4 = new global::Gtk.VBox ();
			this.vbox4.Name = "vbox4";
			this.vbox4.Spacing = 6;
			// Container child vbox4.Gtk.Box+BoxChild
			this.buttonReset = new global::Gtk.Button ();
			this.buttonReset.CanFocus = true;
			this.buttonReset.Name = "buttonReset";
			this.buttonReset.UseUnderline = true;
			// Container child buttonReset.Gtk.Container+ContainerChild
			global::Gtk.Alignment w2 = new global::Gtk.Alignment (0.5F, 0.5F, 0F, 0F);
			// Container child GtkAlignment.Gtk.Container+ContainerChild
			global::Gtk.HBox w3 = new global::Gtk.HBox ();
			w3.Spacing = 2;
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Image w4 = new global::Gtk.Image ();
			w4.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-clear", global::Gtk.IconSize.Menu);
			w3.Add (w4);
			// Container child GtkHBox.Gtk.Container+ContainerChild
			global::Gtk.Label w6 = new global::Gtk.Label ();
			w6.LabelProp = global::Mono.Unix.Catalog.GetString ("Clear All Options");
			w6.UseUnderline = true;
			w3.Add (w6);
			w2.Add (w3);
			this.buttonReset.Add (w2);
			this.vbox4.Add (this.buttonReset);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.buttonReset]));
			w10.Position = 0;
			w10.Expand = false;
			w10.Fill = false;
			// Container child vbox4.Gtk.Box+BoxChild
			this.buttonPreview = new global::Gtk.Button ();
			this.buttonPreview.CanFocus = true;
			this.buttonPreview.Name = "buttonPreview";
			this.buttonPreview.UseUnderline = true;
			this.buttonPreview.Label = global::Mono.Unix.Catalog.GetString ("Preview Options");
			this.vbox4.Add (this.buttonPreview);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.vbox4 [this.buttonPreview]));
			w11.Position = 1;
			w11.Expand = false;
			w11.Fill = false;
			this.hbox1.Add (this.vbox4);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.vbox4]));
			w12.Position = 1;
			w12.Expand = false;
			w12.Fill = false;
			this.Add (this.hbox1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
			this.buttonReset.Clicked += new global::System.EventHandler (this.OnButtonResetClicked);
			this.buttonPreview.Clicked += new global::System.EventHandler (this.OnButtonPreviewClicked);
		}
	}
}
