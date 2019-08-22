using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using DayNightPapers.Properties;
using Microsoft.Win32;

using WallpaperLib;
using Path = System.IO.Path;


namespace DayNightPapers
{
    
    public partial class MainWindow : Window
    {
        // TODO: Display sun data in UI
        private DayNightSwitcher switcher = new DayNightSwitcher();
        private System.Windows.Forms.NotifyIcon trayIcon;

        public MainWindow()
        {
            InitializeComponent();

            // TODO: Change icon and embed in exe, possibly stop using forms if I can find a decent way around it
            trayIcon = new System.Windows.Forms.NotifyIcon();
            trayIcon.Icon = Properties.Resources.icon;

            trayIcon.MouseDoubleClick += trayIcon_MouseDoubleClick;

            if (switcher.DayPaper != null) DayPaperLocation.Content = Path.GetFileName(switcher.DayPaper);
            else DayPaperLocation.Content = "Please select a wallpaper";
            if (switcher.NightPaper != null) NightPaperLocation.Content = Path.GetFileName(switcher.NightPaper);
            else NightPaperLocation.Content = "Please select a wallpaper";

            StartCheck.IsChecked = Settings.Default.LaunchOnStart;
            StartCheck.Checked += registerStartup;
            StartCheck.Unchecked += unregisterStartup;

            switcher.Start();
        }

        private void DayPickerBtn_Click(object sender, RoutedEventArgs e)
        {
            string paperPath = selectImage();
            if (paperPath != null)
            {
                switcher.DayPaper = paperPath;
                switcher.ForcePaperCheck();

                DayPaperLocation.Content = Path.GetFileName(switcher.DayPaper); ;
                
            }
            
        }

        private void NightPickerBtn_Click(object sender, RoutedEventArgs e)
        {
            string paperPath = selectImage();
            if (paperPath != null)
            {
                switcher.NightPaper = paperPath;
                switcher.ForcePaperCheck();

                NightPaperLocation.Content = Path.GetFileName(switcher.NightPaper);
            }
        }

        private string selectImage()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.DefaultExt = ".png";
            dialog.Filter = "Image files (*.png,*.jpeg)|*.png;*.jpeg;*.jpg";
            bool? result = dialog.ShowDialog();

            if (result == true) {
                return dialog.FileName;
            }

            return null;
        }

        private void registerStartup(object sender, EventArgs e)
        {
            if (Settings.Default.LaunchOnStart == false)
            {
                using (RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true)) {
                    rk.SetValue("DayNightPapers", "\"" + Assembly.GetEntryAssembly().Location + "\"");
                }

                Settings.Default.LaunchOnStart = true;
                Settings.Default.Save();
            }
            
        }

        private void unregisterStartup(object sender, EventArgs e)
        {
            if (Settings.Default.LaunchOnStart)
            {
                using (RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true)) {
                    rk.DeleteValue("DayNightPapers", false);
                }

                Settings.Default.LaunchOnStart = false;
                Settings.Default.Save();
            }
            
        }

        // Minimize to tray stuff
        void trayIcon_MouseDoubleClick(object sender,
            System.Windows.Forms.MouseEventArgs e)
        {
            this.WindowState = WindowState.Normal;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized) {
                this.ShowInTaskbar = false;
                this.Focus();
                trayIcon.Visible = true;
            } else if (this.WindowState == WindowState.Normal) {
                trayIcon.Visible = false;
                this.ShowInTaskbar = true;
            }
        }
    }
}
