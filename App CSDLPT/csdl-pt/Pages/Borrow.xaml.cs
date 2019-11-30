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
    /// Interaction logic for Borrow.xaml
    /// </summary>
    public partial class Borrow : Page
    {
        string connectionString;
        public Borrow(string connectionStrin)
        {
            InitializeComponent();
        }

        private void DtgBorrow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btnUpdate_Muon.IsEnabled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAdd_Muon_Click(object sender, RoutedEventArgs e)
        {

            NavigationService navService = NavigationService.GetNavigationService(this);
            Add_Update_Muon pg = new Add_Update_Muon(connectionString);
            navService.Navigate(pg);
        }

        private void BtnUpdate_Muon_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDelete_Muon_Click(object sender, RoutedEventArgs e)
        {
            btnDelete_Muon.IsEnabled = true;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            Muon pg = new Muon(connectionString);
            navService.Navigate(pg);
        }
    }
}
