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
using WallpaperLib;
using DayNightPapers.Properties;

namespace DayNightPapers.Pages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        private Action<bool> _returnHandler;
        private DayNightSwitcher _switcher;

        public SettingsPage(DayNightSwitcher switcher, Action<bool> returnHandler)
        {
            InitializeComponent();

            _returnHandler = returnHandler;
            _switcher = switcher;

            LongBox.Text = _switcher.Longtitude.ToString();
            LatBox.Text = _switcher.Latitude.ToString();
            MinimizeAtStartCheck.IsChecked = Settings.Default.Minimize;

            CancelBtn.Click += (sender, e) => _returnHandler?.Invoke(true);
            SaveBtn.Click += SaveBtnClick;
        }

        private void SaveBtnClick(object sender, RoutedEventArgs e)
        {
            // Parse lat/long
            double lat, longt;

            if (double.TryParse(LongBox.Text, out longt) && double.TryParse(LatBox.Text, out lat))
            {
                _switcher.Latitude = lat;
                _switcher.Longtitude = longt;
            }
            else
            {
                MessageBox.Show("Incorrect lat/long format");
            }

            Settings.Default.Minimize = (bool) MinimizeAtStartCheck.IsChecked;
            Settings.Default.Save();
            _returnHandler(true);
        }
    }
}
