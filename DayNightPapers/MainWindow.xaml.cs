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


namespace DayNightPapers
{
    
    public partial class MainWindow : Window
    {
        // TODO: Display sun data in UI
        
        private System.Windows.Forms.NotifyIcon trayIcon;

        public MainWindow()
        {
            InitializeComponent();

            // TODO: Change icon and embed in exe, possibly stop using forms if I can find a decent way around it
            trayIcon = new System.Windows.Forms.NotifyIcon();
            trayIcon.Icon = Properties.Resources.icon;

            trayIcon.MouseDoubleClick += trayIcon_MouseDoubleClick;

            MainPage mainPage = new MainPage();
            MainFrame.Navigate(mainPage);
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
