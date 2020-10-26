using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;
using System.Resources;
using Microsoft.VisualBasic;

namespace Mother3Patcher.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void browseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "GBA ROM files (*.gba)|*.gba|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            if (openFileDialog.ShowDialog() == true)
            {
                fileBox.Text = openFileDialog.FileName;
            }
               
        }

        private void applyButton_Click(object sender, RoutedEventArgs e)
        {

            if (backupChecked.IsChecked == true)
            {
                
                using (Process process = new Process { })
                {

                    process.StartInfo = new ProcessStartInfo
                    {
                        

                        FileName = "CMD.exe",
                        Arguments = String.Format("/C copy /b \"{0}\" \"{0}.bak\"", fileBox.Text)

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

            string tempExeName = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "ups.exe");
            string tempUpsName = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "mother3.ups");


            using (FileStream fsDst = new FileStream(tempExeName, FileMode.CreateNew, FileAccess.Write))
            {
                byte[] bytes = Resource1.GetUps();

                fsDst.Write(bytes, 0, bytes.Length);
            }

            using (FileStream fsDst = new FileStream(tempUpsName, FileMode.CreateNew, FileAccess.Write))
            {
                byte[] bytes = Resource1.GetMother3();

                fsDst.Write(bytes, 0, bytes.Length);
            }

            using (Process process = new Process { })
            {

                process.StartInfo = new ProcessStartInfo
                {

                    FileName = "cmd.exe",
                    Arguments = String.Format("/C {0} apply -b \"{2}\" -p {1} -o \"{2}\"", tempExeName, tempUpsName, fileBox.Text)

                };
                //Start the process 
                process.Start();
                //Wait until the process is done
                process.WaitForExit();
                //Dispose the process
                process.Dispose();

                File.Delete(tempUpsName);
                File.Delete(tempExeName);
                MessageBox.Show("Mother 3 has been successfully patched.", "Done", MessageBoxButton.OK, MessageBoxImage.Information);


            }

        
    }

        
    }
}
