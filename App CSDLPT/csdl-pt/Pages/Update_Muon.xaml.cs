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
    /// Interaction logic for Update_Muon.xaml
    /// </summary>
    public partial class Update_Muon : Page
    {
        public string connectionString;
        List<EF.NhanVien> listNhanVien;
        List<EF.TaiLieu> listTaiLieu;
        List<EF.BanSao> listBanSao;
        List<EF.DocGia> listDocGia;
        public Update_Muon(string connectionString)
        {           
            EF.Muon muon;
            EF.qltv db;
            InitializeComponent();
            this.connectionString = connectionString;
            db = new EF.qltv();
            muon = db.Muons.Find(Pages.Borrow.dsMuon);

            cbMaBanSao.SelectedItem = muon.ma_bansao;
            cbMaDocGia.SelectedItem = muon.ma_sinhvien;
            cbMaNhanVienDua.SelectedItem = muon.ma_nhanvien_dua;
            cbMaTaiLieu.SelectedItem = muon.ma_tailieu;
            dpNgayMuon.SelectedDate = muon.ngay_muon;
            txtTienDatCoc.Text = muon.tien_datcoc.ToString();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if( txtTienDatCoc.Text == "")
            {
                MessageBox.Show("Hãy nhập tiền cọc");
                txtTienDatCoc.Focus();
                return;
            }
            // add
            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_update_Muon", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                try
                {
                    conn.Open();
                    // Add params nếu có
                    command.Parameters.AddWithValue("@ma_nhanvien_dua", cbMaNhanVienDua.Text);
                    command.Parameters.AddWithValue("@ma_bansao", cbMaBanSao.Text);
                    command.Parameters.AddWithValue("@ma_tailieu", cbMaTaiLieu.Text);
                    command.Parameters.AddWithValue("@ma_sinhvien", cbMaDocGia.Text);
                    command.Parameters.AddWithValue("@tien_datcoc", txtTienDatCoc.Text);

                    var rdr = command.ExecuteNonQuery(); // Sử dụng khi không trả về dữ liệu
                    //var rdr = command.ExecuteReader(); // Sử dụng khi có trả về dữ liệu

                    resetValue();
                    get_NhanVien();
                    get_BanSao();
                    get_DocGia();
                    get_TaiLieu();
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

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            Borrow pg = new Borrow(connectionString);
            navService.Navigate(pg);
        }
        private void resetValue()
        {
            cbMaDocGia.Text = "";
            cbMaNhanVienDua.Text = "";
            cbMaBanSao.Text = "";
            cbMaTaiLieu.Text = "";
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
                    List<string> lstNV = new List<string>();
                    while (rdr.Read())
                    {
                        // dùng rdr["<tên cột>"] để lấy dữ liệu trả về từ sp
                        listNhanVien.Add(new EF.NhanVien()
                        {
                            ma_nhanvien = rdr["ma_nhanvien"].ToString(),
                        });
                        lstNV.Add(rdr["ma_nhanvien"].ToString());
                    }
                    cbMaNhanVienDua.ItemsSource = lstNV;
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
        private void get_TaiLieu()
        {
            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_get_dsTaiLieu", conn)
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

                    listTaiLieu = new List<EF.TaiLieu>();
                    List<string> lstTL = new List<string>();
                    while (rdr.Read())
                    {
                        // dùng rdr["<tên cột>"] để lấy dữ liệu trả về từ sp
                        listTaiLieu.Add(new EF.TaiLieu()
                        {
                            ma_tailieu = rdr["ma_tailieu"].ToString(),
                        });
                        lstTL.Add(rdr["ma_tailieu"].ToString());
                    }
                    cbMaTaiLieu.ItemsSource = lstTL;
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
        private void get_BanSao()
        {
            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_get_dsBanSao", conn)
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

                    listBanSao = new List<EF.BanSao>();
                    List<string> lstBS = new List<string>();
                    while (rdr.Read())
                    {
                        // dùng rdr["<tên cột>"] để lấy dữ liệu trả về từ sp
                        listBanSao.Add(new EF.BanSao()
                        {
                            ma_bansao = rdr["ma_bansao"].ToString(),
                        });
                        lstBS.Add(rdr["ma_bansao"].ToString());
                    }
                    cbMaBanSao.ItemsSource = lstBS;
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
        private void get_DocGia()
        {
            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_get_dsDocGia", conn)
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

                    listDocGia = new List<EF.DocGia>();
                    List<string> lstDG = new List<string>();
                    while (rdr.Read())
                    {
                        // dùng rdr["<tên cột>"] để lấy dữ liệu trả về từ sp
                        listDocGia.Add(new EF.DocGia()
                        {
                            ma_sinhvien = rdr["ma_sinhvien"].ToString(),
                        });
                        lstDG.Add(rdr["ma_sinhvien"].ToString());
                    }
                    cbMaDocGia.ItemsSource = lstDG;
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
