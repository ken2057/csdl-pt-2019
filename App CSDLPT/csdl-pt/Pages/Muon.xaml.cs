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

namespace csdl_pt.Pages
{
    /// <summary>
    /// Interaction logic for Muon.xaml
    /// </summary>
    public partial class Muon : Page
    {
        string connectionString;
        List<EF.QuaTrinhMuon> listQTMuon;
        public static string qtmuon;
        public Muon(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            get_qt_Muon();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            showOption pg = new showOption(connectionString);
            navService.Navigate(pg);
        }

        private void DtgMuon_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var obj = dtgMuon.SelectedItem;
            qtmuon = (obj as EF.QuaTrinhMuon).ma_tailieu;
            qtmuon = (obj as EF.QuaTrinhMuon).ma_bansao;
            qtmuon = (obj as EF.QuaTrinhMuon).ma_sinhvien;
            qtmuon = (obj as EF.QuaTrinhMuon).ma_nhanvien_dua;
            qtmuon = (obj as EF.QuaTrinhMuon).ma_nhanvien_nhan;
            qtmuon = (obj as EF.QuaTrinhMuon).ngay_tra.ToString();
            qtmuon = (obj as EF.QuaTrinhMuon).ngay_muon.ToString();
            qtmuon = (obj as EF.QuaTrinhMuon).ngay_hethan.ToString();
            qtmuon = (obj as EF.QuaTrinhMuon).ngayGio_muon.ToString();
            qtmuon = (obj as EF.QuaTrinhMuon).tien_datcoc.ToString();
            qtmuon = (obj as EF.QuaTrinhMuon).tien_muon.ToString();
            qtmuon = (obj as EF.QuaTrinhMuon).tien_datcoc.ToString();

            btnUpdateMuon.IsEnabled = true;
        }
        private void BtnBack_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            showOption pg = new showOption(connectionString);
            navService.Navigate(pg);
        }

        private void BtnUpdateMuon_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
           // UpdateQuaTrinhMuon pg = new UpdateQuaTrinhMuon(connectionString);
           // navService.Navigate(pg);
        }

        private void BtnMuon_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            // Borrow pg = new Borrow(connectionString);
            // navService.Navigate(pg);
        }

        private void btnDangKy_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            DangKy pg = new DangKy(connectionString);
            navService.Navigate(pg);
        }
        private void get_qt_Muon()
        {
            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_get_qtMuon", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                try
                {
                    conn.Open();
                    // Add params nếu có
                    //command.Parameters.AddWithValue("@masp", masp);

                    //var rdr = command.ExecuteNonQuery(); // Sử dụng khi không trả về dữ liệu
                    var rdr = command.ExecuteReader(); // Sử dụng khi có trả về dữ liệu

                    listQTMuon = new List<EF.QuaTrinhMuon>();
                    while (rdr.Read())
                    {
                        // dùng rdr["<tên cột>"] để lấy dữ liệu trả về từ sp
                        listQTMuon.Add(new EF.QuaTrinhMuon()
                        {
                            ma_tailieu = rdr["ma_tailieu"].ToString(),
                            ma_bansao = rdr["ma_bansao"].ToString(),
                            ma_sinhvien = rdr["ma_sinhvien"].ToString(),
                            ma_nhanvien_dua = rdr["ma_nhanvien_dua"].ToString(),
                            ma_nhanvien_nhan = rdr["ma_nhanvien_nhan"].ToString(),
                            ngayGio_muon = DateTime.Parse(rdr["ngayGio_muon"].ToString()),
                            ngay_hethan = DateTime.Parse(rdr["ngay_hethan"].ToString()),
                            ngay_muon = DateTime.Parse(rdr["ngay_muon"].ToString()),
                            ngay_tra = DateTime.Parse(rdr["ngay_tra"].ToString()),
                            tien_datcoc = Decimal.Parse( rdr["tien_datcoc"].ToString()),
                            tien_datra =Decimal.Parse(rdr["tien_datra"].ToString()),
                            tien_muon = Decimal.Parse(rdr["tien_muon"].ToString()),
                            ghichu = rdr["ghichu"].ToString()
                        });
                    }

                    dtgMuon.ItemsSource = listQTMuon ;
                    dtgMuon.Items.Refresh();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error");
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
