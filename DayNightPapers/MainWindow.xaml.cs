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

    public class ClickCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event EventHandler Clicked;

        public ClickCommand(EventHandler click)
        {
            Clicked = click;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Clicked?.Invoke(this, new EventArgs());
        }
    }

    public partial class MainWindow : Window
    {
        // TODO: Display sun data in UI
        
        private ContextMenu contextMenu;

        public MainWindow()
        {
            InitializeComponent();

            // TODO: Change icon and embed in exe, possibly stop using forms if I can find a decent way around it
            NotifyIcon.Icon = Properties.Resources.icon;
            NotifyIcon.ContextMenu = CreateContextMenu();
            NotifyIcon.NoLeftClickDelay = true;
            NotifyIcon.LeftClickCommand = new ClickCommand(trayIcon_MouseDoubleClick);
            
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
        void trayIcon_MouseDoubleClick(object sender, EventArgs e)
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

        private ContextMenu CreateContextMenu()
        {
            ContextMenu contextMenu = new ContextMenu();
            MenuItem closeItem = new MenuItem();
            closeItem.Click += (closeSender, args) => this.Close();
            closeItem.Header = "Close program";
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
