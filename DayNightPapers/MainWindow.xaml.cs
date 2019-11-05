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
using System.Net;

using Hardcodet.Wpf.TaskbarNotification;
using WallpaperLib;


namespace DayNightPapers
{
    
    public partial class MainWindow : Window
    {
        // TODO: Display sun data in UI
        
        private System.Windows.Forms.NotifyIcon trayIcon;
        private ContextMenu contextMenu;

        public MainWindow()
        {
            InitializeComponent();

            // TODO: Change icon and embed in exe, possibly stop using forms if I can find a decent way around it
            trayIcon = new System.Windows.Forms.NotifyIcon();
            trayIcon.Icon = Properties.Resources.icon;
            trayIcon.MouseClick += trayIcon_MouseDoubleClick;
            trayIcon.Visible = true;

            this.Loaded += LoadedEventHandler;

            DayNightSwitcher switcher = new DayNightSwitcher();
            if(switcher.Latitude == 0 && switcher.Longtitude == 0)
            {
                OpenSetupPage(switcher);
            } else {
                OpenMainPage(switcher);
            }

            contextMenu = CreateContextMenu();
            contextMenu.LostFocus += (sender, buttonvars) =>
            {
                contextMenu.IsOpen = false;
            };
        }

        public void LoadedEventHandler(object sender, EventArgs e)
        {
            if (Settings.Default.Minimize)
            {
                this.WindowState = WindowState.Minimized;
            }
        }

        public void OpenMainPage(DayNightSwitcher switcher)
        {
            try
            {
                // TODO: Rewrite this, if you have bad internet og server takes a long time to answer
                // then this wil give the appearance of the program crashing
                DateTime sunrise = switcher.SunRise;
            }
            catch(WebException we)
            {
                MessageBox.Show("Got an error from suntime server, probably bad coords, navigating you to the setup page.");
                OpenSetupPage(switcher);
                return;
            }
            MainPage mainPage = new MainPage(switcher);
            MainFrame.Navigate(mainPage);
        }

        public void OpenSetupPage(DayNightSwitcher switcher)
        {
            MainFrame.Navigate(new SetupPage(switcher, OpenMainPage));
        }

        // Minimize to tray stuff
        void trayIcon_MouseDoubleClick(object sender,
            System.Windows.Forms.MouseEventArgs e)
        {
            if(e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (this.WindowState == WindowState.Minimized)
                {
                    this.WindowState = WindowState.Normal;
                }
                else
                {
                    this.WindowState = WindowState.Minimized;
                }
            } else if(e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenu.IsOpen = true;
                contextMenu.StaysOpen = false;
            }
        }

        private ContextMenu CreateContextMenu()
        {
            ContextMenu contextMenu = new ContextMenu();
            MenuItem closeItem = new MenuItem();
            closeItem.Click += (closeSender, args) => this.Close();
            closeItem.Header = "Close program";
            contextMenu.Items.Add(closeItem);
            contextMenu.LostKeyboardFocus += (fSender, e) => contextMenu.IsOpen = false;
            // contextMenu.LostFocus += (focus, e) => contextMenu.IsOpen = false;
            return contextMenu;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized) {
                this.ShowInTaskbar = false;
                this.WindowStyle = WindowStyle.ToolWindow;
            } else if (this.WindowState == WindowState.Normal) {
                this.ShowInTaskbar = true;
                this.WindowStyle = WindowStyle.SingleBorderWindow;
            }
        }
    }
}
