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

namespace csdl_pt.Pages
{
    /// <summary>
    /// Interaction logic for loginOption.xaml
    /// </summary>
    public partial class loginOption : Page
    {
        List<string> listCS;
string connectionString;
        public loginOption(List<string> listCS, string connectionString)
        {
            InitializeComponent();
            this.listCS = listCS;
this.connectionString = connectionString;
        }

        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            login sO = new login();
            navService.Navigate(sO);
        }

        private void btnTram_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int index = int.Parse(btn.Tag.ToString());

            NavigationService navService = NavigationService.GetNavigationService(this);
            showOption sO = new showOption(listCS[index]);
            navService.Navigate(sO);
        }
    }
}
