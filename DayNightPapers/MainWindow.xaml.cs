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
using DayNightPapers.Helpers;
using DayNightPapers.Pages;


namespace DayNightPapers
{
    public partial class MainWindow : Window
    {
        private DayNightSwitcher _switcher;
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += LoadedEventHandler;

            SetupNotifyIcon();
            _switcher = new DayNightSwitcher();
        }

        public void LoadedEventHandler(object sender, EventArgs e)
        {
            OpenMainPage();
        }

        /// <summary>
        /// Opens the main page and navigates the UI to it 
        /// Will force _switcher to make a request and navigate to Setup if it fails.
        /// </summary>
        public void OpenMainPage(bool blockMinimize = false)
        {
            // If the latituted and longtitude is 0 then it's the first time the program has started.
            if (_switcher.Latitude == 0 && _switcher.Longtitude == 0)
            {
                OpenSetupPage();
            }
            else
            {
                // Try and fetch a result from the API to ensure that the user has internet connection and has proper coordinates
                try
                {
                    // TODO: Rewrite this, if you have bad internet og server takes a long time to answer
                    // then this wil give the appearance of the program crashing
                    DateTime sunrise = _switcher.SunRise;
                }
                catch (WebException we)
                {
                    // No internet connection 
                    if (we.Status == WebExceptionStatus.ConnectFailure || we.Status == WebExceptionStatus.NameResolutionFailure)
                    {
                        MessageBox.Show("Could not connect to the Sun server, navigating to setup page, click submit when you have regained internet connection");
                    }
                    // Bad coordinate format.
                    else
                    {
                        MessageBox.Show("Got an error from suntime server, probably bad coords, navigating you to the setup page.");
                    }
                    OpenSetupPage();
                    return;
                }

                // If all checks passes then just open the main page.
                MainFrame.Navigate(new MainPage(_switcher, OpenSettingsPage));
                // Don't minimize if setup page is displayed.
                if (Settings.Default.Minimize && !blockMinimize)
                {
                    this.WindowState = WindowState.Minimized;
                }
            }
        }

        private void OpenSetupPage()
        {
            MainFrame.Navigate(new SetupPage(_switcher, OpenMainPage));
        }

        private void OpenSettingsPage(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SettingsPage(_switcher, OpenMainPage));
        }

        // Minimize to tray stuff
        private void NotifyClick(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                this.WindowState = WindowState.Normal;
                this.Topmost = true;
                this.Topmost = false;
            }
            else
            {
                this.WindowState = WindowState.Minimized;
            }
        }

        private void SetupNotifyIcon()
        {
            NotifyIcon.Icon = Properties.Resources.icon;
            NotifyIcon.ContextMenu = GetContextMenu();
            NotifyIcon.NoLeftClickDelay = true;
            NotifyIcon.LeftClickCommand = new ClickEventWrapper(NotifyClick);
        }

        private ContextMenu GetContextMenu()
        {
            ContextMenu contextMenu = new ContextMenu();
            MenuItem closeItem = new MenuItem();
            closeItem.Click += (closeSender, args) => this.Close();
            closeItem.Header = "Close";
            contextMenu.Items.Add(closeItem);
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
