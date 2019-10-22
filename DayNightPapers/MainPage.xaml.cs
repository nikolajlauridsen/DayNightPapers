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
using DayNightPapers.Properties;

using SunData; 
using WallpaperLib;
using Path = System.IO.Path;
using Microsoft.Win32;

namespace DayNightPapers
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {

        private DayNightSwitcher _switcher;

        public MainPage(DayNightSwitcher switcher)
        {
            InitializeComponent();

            _switcher = switcher;

            if (switcher.DayPaper != null) { 
                DayPaperLocation.Content = Path.GetFileName(switcher.DayPaper);
                DayImage.Source = getImageSource(switcher.DayPaper);
            }
            else DayPaperLocation.Content = "Please select a wallpaper";

            if (switcher.NightPaper != null) { 
                NightPaperLocation.Content = Path.GetFileName(switcher.NightPaper);
                NightImage.Source = getImageSource(switcher.NightPaper);
            }
            else NightPaperLocation.Content = "Please select a wallpaper";

            MinimizeAtStartCheck.IsChecked = Settings.Default.Minimize;
            MinimizeAtStartCheck.Checked += (sender, e) => {
                Settings.Default.Minimize = true;
                Settings.Default.Save();
                };

            MinimizeAtStartCheck.Unchecked += (sender, e) =>
            {
                Settings.Default.Minimize = false;
                Settings.Default.Save();
            };

            switcher.SunDataChanged += SunTimeChanged;

            switcher.Start();
        }

        private void SunTimeChanged(SunTimeData newData)
        {
            Dispatcher.Invoke(() => { 
                // UI code here....
            });
        }

        private void DayPickerBtn_Click(object sender, RoutedEventArgs e)
        {
            string paperPath = selectImage();
            if (paperPath != null)
            {
                DayImage.Source = null;
                _switcher.DayPaper = paperPath;
                _switcher.ForcePaperCheck();

                DayPaperLocation.Content = Path.GetFileName(_switcher.DayPaper); ;
                DayImage.Source = getImageSource(_switcher.DayPaper);
            }

        }

        private void NightPickerBtn_Click(object sender, RoutedEventArgs e)
        {
            string paperPath = selectImage();
            if (paperPath != null)
            {
                NightImage.Source = null;
                _switcher.NightPaper = paperPath;
                _switcher.ForcePaperCheck();

                NightPaperLocation.Content = Path.GetFileName(_switcher.NightPaper);
                NightImage.Source = getImageSource(_switcher.NightPaper);
            }
        }

        private BitmapImage getImageSource(string path)
        {
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.UriSource = new Uri(path);
            img.CacheOption = BitmapCacheOption.OnLoad;
            img.EndInit();
            return img;
        }

        private string selectImage()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.DefaultExt = ".png";
            dialog.Filter = "Image files (*.png,*.jpeg)|*.png;*.jpeg;*.jpg";
            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                return dialog.FileName;
            }

            return null;
        }
    }
}
