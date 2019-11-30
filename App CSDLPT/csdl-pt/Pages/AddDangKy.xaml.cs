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
    /// Interaction logic for AddDangKy.xaml
    /// </summary>
    public partial class AddDangKy : Page
    {
        string connectionString;
        List<EF.TaiLieu> listTL;
        List<EF.DocGia> listDG;
        EF.TaiLieu tailieu;
        EF.DocGia docgia;
        public AddDangKy(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            Init();
        }

        private void Init()
        {
            get_taiLieu();
            get_DocGia();
            DateTime current = DateTime.Now;
            txtNgayGio.Text = current.ToString();
        }

        private void get_taiLieu()
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

                    listTL = new List<EF.TaiLieu>();
                    while (rdr.Read())
                    {
                        // dùng rdr["<tên cột>"] để lấy dữ liệu trả về từ sp
                        listTL.Add(new EF.TaiLieu()
                        {
                            ma_tailieu = rdr["ma_tailieu"].ToString(),
                            ten_tailieu = rdr["ten_tailieu"].ToString(),
                            ma_tacgia_1 = rdr["ten_tacgia"].ToString(),
                            ngonngu = rdr["ngonngu"].ToString(),
                            sl_kho = byte.Parse(rdr["sl_kho"].ToString()),
                        });
                    }

                    dtgTaiLieu.ItemsSource = listTL;
                    //dtgSinhVien.SelectedIndex = 0;
                    dtgTaiLieu.Items.Refresh();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error get_taiLieu");
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

                    listDG = new List<EF.DocGia>();
                    while (rdr.Read())
                    {
                        // dùng rdr["<tên cột>"] để lấy dữ liệu trả về từ sp
                        listDG.Add(new EF.DocGia()
                        {
                            ma_sinhvien = rdr["ma_sinhvien"].ToString(),
                            hoten = rdr["hoten"].ToString(),
                            NgaySinh = DateTime.Parse(rdr["NgaySinh"].ToString()),
                            diachi = rdr["diachi"].ToString(),
                            sdt = rdr["sdt"].ToString()
                        });
                    }

                    dtgDocGia.ItemsSource = listDG;
                    //dtgSinhVien.SelectedIndex = 0;
                    dtgDocGia.Items.Refresh();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error get_docgia");
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void dtgTaiLieu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            tailieu = (EF.TaiLieu)dtgTaiLieu.SelectedItem;
            txtSearchTaiLieu.Text = tailieu.ten_tailieu;
        }

        private void dtgDocGia_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            docgia = (EF.DocGia)dtgDocGia.SelectedItem;
            txtSearchDocGia.Text = docgia.hoten;
        }

        private void btnThemDangKy_Click(object sender, RoutedEventArgs e)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_add_DangKy", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                try
                {
                    conn.Open();

                    command.Parameters.AddWithValue("@maTL",tailieu.ma_tailieu);
                    command.Parameters.AddWithValue("@maSV", docgia.ma_sinhvien);
                    command.Parameters.AddWithValue("@ngaygio", txtNgayGio.Text);
                    command.Parameters.AddWithValue("@ghichu", txtGhiChu.Text);

                    var rdr = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
                finally
                {
                    conn.Close();
                }
                doneFunction();
            }
        
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            doneFunction();
        }

        private void doneFunction()
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            DangKy pg = new DangKy(connectionString);
            navService.Navigate(pg);
        }

        private void txtSearchTaiLieu_KeyUp(object sender, KeyEventArgs e)
        {
            var filtered = listTL.Where(tailieu => tailieu.ten_tailieu.ToLower().Contains(txtSearchTaiLieu.Text));
            dtgTaiLieu.ItemsSource = filtered;
        }

        private void txtSearchDocGia_KeyUp(object sender, KeyEventArgs e)
        {
            var filtered = listDG.Where(docgia => docgia.hoten.ToLower().Contains(txtSearchDocGia.Text));
            dtgDocGia.ItemsSource = filtered;
        }
    }
}
