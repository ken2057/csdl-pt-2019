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
    /// Interaction logic for view_rp_LsMuon_TaiLieu.xaml
    /// </summary>
    public partial class view_rp_LsMuon_TaiLieu : Page
    {
        string connectionString;
        List<string> listTraHan;
        public view_rp_LsMuon_TaiLieu(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            setValue();


            get_LsMuon();
        }

        private void setValue()
        {
            listTraHan = new List<string> {"", "Đúng hạn", "Trễ hạn" };
            cboTraHan.ItemsSource = listTraHan;
            cboTraHan.SelectedIndex = 0;
        }

        private void get_LsMuon()
        {
            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_get_lsMuon_taiLieu", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                try
                {
                    conn.Open();
                    // Add params nếu có
                    command.Parameters.AddWithValue("@maTaiLieu", txtTaiLieu.Text);
                    command.Parameters.AddWithValue("@maNV", txtNhanVien.Text);
                    command.Parameters.AddWithValue("@ma_sinhvien", txtDocGia.Text);

                    string treHan = listTraHan[cboTraHan.SelectedIndex] == "Đúng hạn" ? "khong tre" :
                                     listTraHan[cboTraHan.SelectedIndex] == "Trễ hạn" ? "tre" : "";

                    command.Parameters.AddWithValue("@treHan", treHan);

                    //var rdr = command.ExecuteNonQuery(); // Sử dụng khi không trả về dữ liệu
                    var rdr = command.ExecuteReader(); // Sử dụng khi có trả về dữ liệu

                    List<View.rp_LsuMuon> listLsMuon = new List<View.rp_LsuMuon>();
                    while (rdr.Read())
                    {
                        // dùng rdr["<tên cột>"] để lấy dữ liệu trả về từ sp
                        listLsMuon.Add(new View.rp_LsuMuon()
                        {
                            //ma_tailieu, ma_bansao, ngay_hethan, ngay_muon, ngay_tra, ma_sinhvien, ma_nhanvien_dua, ma_nhanvien_nhan
                            ma_nhanvien_dua = func.utils.convertUTF8(rdr["ma_nhanvien_dua"].ToString()),
                            ma_nhanvien_nhan = func.utils.convertUTF8(rdr["ma_nhanvien_nhan"].ToString()),
                            ma_sinhvien = func.utils.convertUTF8(rdr["ma_sinhvien"].ToString()),

                            ngay_tra = DateTime.Parse(rdr["ngay_tra"].ToString()),
                            ngay_hethan = DateTime.Parse(rdr["ngay_hethan"].ToString()),
                            ngay_muon = DateTime.Parse(rdr["ngay_muon"].ToString()),

                            ma_tailieu = func.utils.convertUTF8(rdr["ma_tailieu"].ToString()),
                            ma_bansao = func.utils.convertUTF8(rdr["ma_bansao"].ToString()),
                        });
                    }

                    Reports.rp_LsMuon rpLsMuon = new Reports.rp_LsMuon();
                    rpLsMuon.SetDataSource(listLsMuon);

                    crystalRPNhanVien.ReportSource = rpLsMuon;
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

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            Report.main_report pg = new Report.main_report(connectionString);
            navService.Navigate(pg);
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            windowFormsHost.Height = gridRPNV.RowDefinitions[2].ActualHeight;
            windowFormsHost.Width = gridRPNV.ColumnDefinitions[1].ActualWidth * 6;
        }

        private void txtTaiLieu_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            cboTraHan.SelectedIndex = 0;
            txtDocGia.Text = "";
            txtNhanVien.Text = "";
            txtTaiLieu.Text = "";
        }

        private void btnTim_Click(object sender, RoutedEventArgs e)
        {
            get_LsMuon();
        }

        private void gridRPNV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                get_LsMuon();
        }
    }
}
