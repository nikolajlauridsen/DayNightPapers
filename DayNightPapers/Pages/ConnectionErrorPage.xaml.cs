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

namespace DayNightPapers.Pages
{
    /// <summary>
    /// Interaction logic for ConnectionErrorPage.xaml
    /// </summary>
    public partial class ConnectionErrorPage : Page
    {
        public ConnectionErrorPage(Action<bool> navigateAction)
        {
            InitializeComponent();

            RetryBtn.Click += (sender, e) => navigateAction(false);
        }
    }
}
