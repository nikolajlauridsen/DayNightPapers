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

namespace DayNightPapers
{
    /// <summary>
    /// Interaction logic for SetupPage.xaml
    /// </summary>
    public partial class SetupPage : Page
    {
        private DayNightSwitcher _switcher;
        public SetupPage(DayNightSwitcher switcher, Action navigateAction)
        {
            InitializeComponent();

            _switcher = switcher;
            Latitude.Text = _switcher.Latitude.ToString();
            Longtitude.Text = _switcher.Longtitude.ToString();

            SubmitBtn.Click += (sender, e) =>
            {
                double lat, longt;

                if(double.TryParse(Longtitude.Text, out longt) && double.TryParse(Latitude.Text, out lat))
                {
                    _switcher.Latitude = lat;
                    _switcher.Longtitude = longt;
                    navigateAction();
                } else
                {
                    MessageBox.Show("Incorrect lat/long format");
                }
            };
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.gps-coordinates.net");
        }
    }
}
