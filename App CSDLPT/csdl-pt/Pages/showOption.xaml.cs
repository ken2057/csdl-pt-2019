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
    /// Interaction logic for showOption.xaml
    /// </summary>
    public partial class showOption : Page
    {
        string connectionString;
        NavigationService navService;
        public showOption(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
        }

        private void btnTaiLieu_Click(object sender, RoutedEventArgs e)
        {
            navService = NavigationService.GetNavigationService(this);
            TaiLieu pg = new TaiLieu(connectionString);
            navService.Navigate(pg);
        }

        private void btnMuon_Click(object sender, RoutedEventArgs e)
        {
            navService = NavigationService.GetNavigationService(this);
            Muon pg = new Muon(connectionString);
            navService.Navigate(pg);
        }

        private void btnLoai_Click(object sender, RoutedEventArgs e)
        {
            navService = NavigationService.GetNavigationService(this);
            LoaiTL pg = new LoaiTL(connectionString);
            navService.Navigate(pg);
        }

        private void btnDocGia_Click(object sender, RoutedEventArgs e)
        {
            navService = NavigationService.GetNavigationService(this);
            DocGia pg = new DocGia(connectionString);
            navService.Navigate(pg);
        }

        private void btnNhanVien_Click(object sender, RoutedEventArgs e)
        {
            navService = NavigationService.GetNavigationService(this);
            NhanVien pg = new NhanVien(connectionString);
            navService.Navigate(pg);
        }

        private void btnTacGia_Click(object sender, RoutedEventArgs e)
        {
            navService = NavigationService.GetNavigationService(this);
            TacGia pg = new TacGia(connectionString);
            navService.Navigate(pg);
        }

        private void btnBaoCao_Click(object sender, RoutedEventArgs e)
        {
            navService = NavigationService.GetNavigationService(this);
            Report.main_report pg = new Report.main_report(connectionString);
            navService.Navigate(pg);
        }

        private void btnDangXuat_Click(object sender, RoutedEventArgs e)
        {
            navService = NavigationService.GetNavigationService(this);
            login pg = new login();
            navService.Navigate(pg);
        }
    }
}
