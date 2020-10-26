using System;
using System.Diagnostics;
using System.IO;
using AppKit;
using Foundation;

namespace Mother3Patcher.macOS
{
    public partial class ViewController : NSViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Do any additional setup after loading the view.
        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }

        partial void browseButton(Foundation.NSObject sender)
        {
            var dlg = NSOpenPanel.OpenPanel;
            dlg.CanChooseFiles = true;
            dlg.CanChooseDirectories = false;
            dlg.AllowedFileTypes = new string[] { "gba" };

            if (dlg.RunModal() == 1)
            {
                // Nab the first file
                var url = dlg.Urls[0];

                if (url != null)
                {
                    fileBox.StringValue = url.Path;
                }
            }
        }

        partial void applyButton(Foundation.NSObject sender)
        {
            if(backupChecked.IntValue == 1)
            {
                using (Process process = new Process { })
                {

                    process.StartInfo = new ProcessStartInfo
                    {

                        FileName = "cp",
                        Arguments = String.Format("'{0}' '{0}.bak'", fileBox.StringValue)

                    };
                    //Start the process 
                    process.Start();
                    //Wait until the process is done
                    process.WaitForExit();
                    //Dispose the process
                    process.Dispose();
                    //Repeat
                }
            }

            using (Process process = new Process { })
            {

                process.StartInfo = new ProcessStartInfo
                {

                    FileName = "ups",
                    Arguments = String.Format("apply -b '{0}' -p mother3.ups -o '{0}'", fileBox.StringValue)

                };
                //Start the process 
                process.Start();
                //Wait until the process is done
                process.WaitForExit();
                //Dispose the process
                process.Dispose();
                //Repeat

                var alert = new NSAlert()
                {
                    AlertStyle = NSAlertStyle.Informational,
                    InformativeText = "Mother 3 has been successfully patched.",
                    MessageText = "Done!",
                };
                alert.RunModal();

            }

        }
    }
}
