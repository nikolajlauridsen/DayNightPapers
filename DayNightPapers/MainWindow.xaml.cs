using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;

using WallpaperLib;
using Path = System.IO.Path;


namespace DayNightPapers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WallpaperStore store = new WallpaperStore();

        public MainWindow()
        {
            InitializeComponent();

            if (store.DayPaper != null) DayPaperLocation.Content = Path.GetFileName(store.DayPaper);
            else DayPaperLocation.Content = "Please select a wallpaper";
            if (store.NightPaper != null) NightPaperLocation.Content = Path.GetFileName(store.NightPaper);
            else NightPaperLocation.Content = "Please select a wallpaper";
        }

        private void DayPickerBtn_Click(object sender, RoutedEventArgs e)
        {
            string paperPath = selectImage();
            if (paperPath != null)
            {
                store.DayPaper = paperPath;
                DayPaperLocation.Content = Path.GetFileName(store.DayPaper); ;
            }
            
        }

        private void NightPickerBtn_Click(object sender, RoutedEventArgs e)
        {
            string paperPath = selectImage();
            if (paperPath != null)
            {
                store.NightPaper = paperPath;
                NightPaperLocation.Content = Path.GetFileName(store.NightPaper);
            }
        }

        private string selectImage()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.DefaultExt = ".png";
            dialog.Filter = "PNG image files (*.png)|*.png";
            bool? result = dialog.ShowDialog();

            if (result == true) {
                return dialog.FileName;
            }

            return null;
        }

    }
}
