// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Mother3Patcher.macOS
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSButton backupChecked { get; set; }

		[Outlet]
		AppKit.NSTextField fileBox { get; set; }

		[Action ("applyButton:")]
		partial void applyButton (Foundation.NSObject sender);

		[Action ("browseButton:")]
		partial void browseButton (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (fileBox != null) {
				fileBox.Dispose ();
				fileBox = null;
			}

			if (backupChecked != null) {
				backupChecked.Dispose ();
				backupChecked = null;
			}
		}
	}
}
