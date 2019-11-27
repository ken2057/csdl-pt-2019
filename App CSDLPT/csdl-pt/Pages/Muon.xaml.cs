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
    /// Interaction logic for Muon.xaml
    /// </summary>
    public partial class Muon : Page
    {
        string connectionString;
        public Muon(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            showOption pg = new showOption(connectionString);
            navService.Navigate(pg);
        }

        private void DtgMuon_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void BtnAddMuon_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnUpdateMuon_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
