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
    /// Interaction logic for NhanVien.xaml
    /// </summary>
    public partial class NhanVien : Page
    {
        List<EF.ChiNhanh> listChiNhanh;
        List<EF.Quyen> listQuyen;
        List<EF.NhanVien> listNhanVien;
        EF.NhanVien nv;
        string connectionString;
        public NhanVien(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            get_NhanVien();
            get_Quyen();
            get_ChiNhanh();        
        }
        private void get_ChiNhanh()
        {
            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_get_chinhanh", conn)
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

                    listChiNhanh = new List<EF.ChiNhanh>();
                    List<string> lstCN = new List<string>();
                    while (rdr.Read())
                    {
                        // dùng rdr["<tên cột>"] để lấy dữ liệu trả về từ sp
                        listChiNhanh.Add(new EF.ChiNhanh()
                        {
                            ma_ChiNhanh = rdr["ma_ChiNhanh"].ToString(),
                        });

                        lstCN.Add(rdr["ma_ChiNhanh"].ToString());

                    }
                    cbChiNhanh.ItemsSource = lstCN;
                    //cbQuyen.DisplayMemberPath = "quyen";
                    //cbQuyen.SelectedValuePath = "quyen";
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
        private void get_Quyen()
        {
            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_get_quyen", conn)
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

                    listQuyen = new List<EF.Quyen>();
                    List<string> lstQ = new List<string>();
                    while (rdr.Read())
                    {
                        // dùng rdr["<tên cột>"] để lấy dữ liệu trả về từ sp
                        listQuyen.Add(new EF.Quyen()
                        {                          
                            quyen1 = rdr["quyen"].ToString(),
                        });

                        lstQ.Add(rdr["quyen"].ToString());

                    }
                    cbQuyen.ItemsSource = lstQ;
                    //cbQuyen.DisplayMemberPath = "quyen";
                    //cbQuyen.SelectedValuePath = "quyen";
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

                    dtgNhanVien.ItemsSource = listNhanVien;
                    dtgNhanVien.Items.Refresh();
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
            showOption pg = new showOption(connectionString);
            navService.Navigate(pg);
        }

        private void dtgNhanVien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dtgNhanVien_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            nv = listNhanVien[dtgNhanVien.SelectedIndex];
            txtMaNhanVien.Text = nv.ma_nhanvien ;
            cbQuyen.SelectedValue = nv.quyen;
            cbChiNhanh.SelectedValue = nv.ma_ChiNhanh;
            txtMatKhau.Password = nv.ma_nhanvien;
            txtSDT.Text = nv.sdt;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            themNhanVien();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

        }
        private void themNhanVien()
        {
            if (txtMatKhau.Password == "")
            {
                MessageBox.Show("Hãy nhập mật khẩu");
                txtMatKhau.Focus();
                return;
            }
            if (txtSDT.Text== "")
            {
                MessageBox.Show("Hãy nhập số điện thoại");
                txtSDT.Focus();
                return;
            }
            // add
            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_add_nhanvien", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                try
                {
                    conn.Open();
                    // Add params nếu có
                    //command.Parameters.AddWithValue("@ma_nhanvien", txtMaNhanVien.Text);
                    command.Parameters.AddWithValue("@quyen", cbQuyen.Text);
                    command.Parameters.AddWithValue("@ma_ChiNhanh", cbChiNhanh.Text);
                    command.Parameters.AddWithValue("@matkhau", txtMatKhau.Password);
                    command.Parameters.AddWithValue("@sdt", txtSDT.Text);

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
            txtMatKhau.Password = "";
            txtSDT.Text = "";
        }
    }
}
