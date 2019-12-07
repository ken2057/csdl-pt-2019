using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace csdl_pt.Pages.Report
{
    /// <summary>
    /// Interaction logic for main_report.xaml
    /// </summary>
    public partial class main_report : Page
    {
        string connectionString;
        List<View.Report> listReport;
        public main_report(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;

            addReportsIntoDataGrid();
        }

        private void addReportsIntoDataGrid()
        {
            // set
            listReport = new List<View.Report>();

            // add reports into list
            listReport.Add(new View.Report("Danh sách nhân viên", new view_rp_NhanVien(connectionString)));
            listReport.Add(new View.Report("Danh sách tài liệu", new view_rp_TaiLieu(connectionString)));
            listReport.Add(new View.Report("Lịch sử mượn", new view_rp_LsMuon_TaiLieu(connectionString)));

            // binding source to DataGrid
            dtgRPOptions.ItemsSource = listReport;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            showOption pg = new showOption(connectionString);
            navService.Navigate(pg);
        }

        private void btnRPNV_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            view_rp_NhanVien pg = new view_rp_NhanVien(connectionString);
            navService.Navigate(pg);
        }

        private void dtgRPOptions_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            View.Report rp = (View.Report)dtgRPOptions.SelectedItem;

            NavigationService navService = NavigationService.GetNavigationService(this);
            navService.Navigate(rp.Page);
        }
    }
}
