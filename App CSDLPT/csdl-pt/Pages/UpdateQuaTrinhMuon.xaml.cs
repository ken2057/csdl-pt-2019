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
    /// Interaction logic for UpdateQuaTrinhMuon.xaml
    /// </summary>
    public partial class UpdateQuaTrinhMuon : Page

    {
        List<EF.NhanVien> listNhanVien;
        string connectionString;
        EF.QuaTrinhMuon qtm;
        EF.qltv db;
        public UpdateQuaTrinhMuon(string connectionString)
        {
            get_NhanVien();
            InitializeComponent();
            this.connectionString = connectionString;
            db = new EF.qltv();
            qtm = db.QuaTrinhMuons.Find(Pages.Muon.qtmuon);

            txtMaNhanVienGiao.Text = qtm.ma_nhanvien_dua;
            txtMaDocGia.Text = qtm.ma_sinhvien;
            txtMaTaiLieu.Text = qtm.ma_tailieu;
            txtBanSao.Text = qtm.ma_bansao;
            cbMaNhanVienNhan.Text = qtm.ma_nhanvien_nhan;
            txtMaDocGia.Text = qtm.ma_sinhvien;
            txtTienDaTra.Text = qtm.tien_datra.ToString();
            txtTienCoc.Text = qtm.tien_datcoc.ToString();
            txtTienMuon.Text = qtm.tien_muon.ToString();
            //dpNgayGioMuon.SelectedDate = qtm.ngayGio_muon;
            dpNgayHetHan.SelectedDate = qtm.ngay_hethan;
            dpNgayMuon.SelectedDate = qtm.ngay_muon;
            txtGhiChu.Text = qtm.ghichu;
        }


        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            Muon pg = new Muon(connectionString);
            navService.Navigate(pg);
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // add
            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_update_qtMuon", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                try
                {
                    conn.Open();
                    // Add params nếu có
                    command.Parameters.AddWithValue("@ma_nhanvien_nhan", cbMaNhanVienNhan.Text);
                    command.Parameters.AddWithValue("@ngaytra", dpNgayTra.SelectedDate);
                    command.Parameters.AddWithValue("@tienmuon", txtTienMuon.Text);
                    command.Parameters.AddWithValue("@tien_datra", txtTienDaTra.Text);
                    command.Parameters.AddWithValue("@ghichu",txtGhiChu.Text);

                    var rdr = command.ExecuteNonQuery(); // Sử dụng khi không trả về dữ liệu
                    //var rdr = command.ExecuteReader(); // Sử dụng khi có trả về dữ liệu

                    resetValue();
                    get_NhanVien();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        private void resetValue()
        {
            btnUpdate.IsEnabled = false;
            cbMaNhanVienNhan.Text = "";
            dpNgayTra.SelectedDate = null;
            txtTienMuon.Text = "";
            txtTienDaTra.Text = "";
            txtGhiChu.Text = "";
        }
        private void get_NhanVien()
        {
            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_get_dsNhanVien", conn)
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

                    listNhanVien = new List<EF.NhanVien>();
                    while (rdr.Read())
                    {
                        // dùng rdr["<tên cột>"] để lấy dữ liệu trả về từ sp
                        listNhanVien.Add(new EF.NhanVien()
                        {
                            ma_nhanvien = rdr["ma_nhanvien"].ToString(),
                            quyen = rdr["quyen"].ToString(),
                            ma_ChiNhanh = rdr["ma_ChiNhanh"].ToString(),
                            sdt = rdr["sdt"].ToString()
                        });
                    }
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
