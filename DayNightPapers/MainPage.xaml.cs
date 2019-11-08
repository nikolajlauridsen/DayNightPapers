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
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace DayNightPapers
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {

        private DayNightSwitcher _switcher;

        public MainPage(DayNightSwitcher switcher, RoutedEventHandler settingsOpen)
        {
            InitializeComponent();

            _switcher = switcher;

            if (switcher.DayPaper != null) { 
                DayImage.Source = getImageSource(switcher.DayPaper);
            }

            if (switcher.NightPaper != null) { 
                NightImage.Source = getImageSource(switcher.NightPaper);
            }

            SettingsImage.Source = BitmapToImage(Properties.Resources.wrench, ImageFormat.Png);
            SettingsBtn.Click += settingsOpen;

            DayLabelImage.Source = BitmapToImage(Properties.Resources.sun, ImageFormat.Png);
            NightLabelImage.Source = BitmapToImage(Properties.Resources.moon, ImageFormat.Png);

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

            SunriseLabel.Content = _switcher.SunRise.ToString("H:mm");
            SunsetLabel.Content = _switcher.SunSet.ToString("H:mm");

            switcher.SunDataChanged += SunTimeChanged;

            switcher.Start();
        }

        private void SunTimeChanged(SunTimeData newData)
        {
            Dispatcher.Invoke(() => { 
                SunriseLabel.Content = newData.Sunrise.ToString("H:mm");
                SunsetLabel.Content = newData.Sunset.ToString("H:mm");
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

        private BitmapImage BitmapToImage(Bitmap bitmap, ImageFormat format)
        {
            using(MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory,  format);
                memory.Position = 0;
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.StreamSource = memory;
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.EndInit();
                return img;
            }
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
