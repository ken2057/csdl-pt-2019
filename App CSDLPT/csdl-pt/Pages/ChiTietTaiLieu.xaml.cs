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
    /// Interaction logic for ChiTietTaiLieu.xaml
    /// </summary>
    public partial class ChiTietTaiLieu : Page
    {
        string connectionString;
        TaiLieu _taiLieu;
        public ChiTietTaiLieu(string connectionString, TaiLieu taiLieu)
        {
            InitializeComponent();
            _taiLieu = taiLieu;
            this.connectionString = connectionString;
            show_CTTaiLieu();
        }

        private void show_CTTaiLieu()
        {
            get_CTTaiLieu();
            txtTenTaiLieu.Text = TaiLieu.taiLieu.ten_tailieu;
            lbMaTaiLieu.Content = TaiLieu.taiLieu.ma_tailieu;
            lbTheLoai.Content += ": " + TaiLieu.taiLieu.LoaiTaiLieu.ten_loai;
            lbTacGia.Content = TaiLieu.taiLieu.TacGia.ten_tacgia + ", " + TaiLieu.taiLieu.TacGia1.ten_tacgia + ", " + TaiLieu.taiLieu.TacGia2.ten_tacgia;
            lbNgonNgu.Content += ": " + TaiLieu.taiLieu.ngonngu;
            lbLoaiBia.Content += ": " + TaiLieu.taiLieu.bia;
            lbNgayPhatHanh.Content += ": " + TaiLieu.taiLieu.ngay_phathanh;
            lbSoLuongTon.Content += ": " + TaiLieu.taiLieu.sl_kho;
        }

        private void get_CTTaiLieu()
        {
            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_get_tailieu", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                try
                {
                    conn.Open();
                    // Add params nếu có
                    //command.Parameters.AddWithValue("@masp", masp);
                    command.Parameters.AddWithValue("@maTaiLieu", TaiLieu.taiLieu.ma_tailieu);
                    //var rdr = command.ExecuteNonQuery(); // Sử dụng khi không trả về dữ liệu
                    var rdr = command.ExecuteReader(); // Sử dụng khi có trả về dữ liệu
                    while (rdr.Read())
                    {
                        TaiLieu.taiLieu.LoaiTaiLieu = new EF.LoaiTaiLieu();
                        TaiLieu.taiLieu.TacGia = new EF.TacGia();
                        TaiLieu.taiLieu.TacGia1 = new EF.TacGia();
                        TaiLieu.taiLieu.TacGia2 = new EF.TacGia();

                        TaiLieu.taiLieu.LoaiTaiLieu.ten_loai = rdr["ten_loai"].ToString();
                        TaiLieu.taiLieu.TacGia.ten_tacgia = rdr["ten_tacgia"].ToString();
                        TaiLieu.taiLieu.TacGia1.ten_tacgia = rdr["ten_tacgia1"].ToString();
                        TaiLieu.taiLieu.TacGia2.ten_tacgia = rdr["ten_tacgia2"].ToString();
                        TaiLieu.taiLieu.bia = rdr["bia"].ToString();
                        TaiLieu.taiLieu.ngay_phathanh = DateTime.Parse(rdr["ngay_phathanh"].ToString());
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error get_CTTaiLieu");
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void dtgCTTaiLieu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void dtgCTTaiLieu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            navService.Navigate(_taiLieu);
        }
    }
}
