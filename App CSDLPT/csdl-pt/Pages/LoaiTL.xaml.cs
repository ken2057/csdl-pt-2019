                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    using csdl_pt.EF;
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
    /// Interaction logic for LoaiTL.xaml
    /// </summary>
    public partial class LoaiTL : Page
    {
        string connectionString;
        List<LoaiTaiLieu> listLoai;
        LoaiTaiLieu loai;

        public LoaiTL(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;

            get_dsLoai();
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            showOption pg = new showOption(connectionString);
            navService.Navigate(pg);
        }

        private void get_dsLoai()
        {
            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_get_dsLoai", conn)
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

                    listLoai = new List<LoaiTaiLieu>();
                    while (rdr.Read())
                    {
                        // dùng rdr["<tên cột>"] để lấy dữ liệu trả về từ sp
                        listLoai.Add(new LoaiTaiLieu()
                        {
                            ma_loai = rdr["ma_loai"].ToString(),
                            ten_loai = rdr["ten_loai"].ToString(),
                            ghichu = rdr["ghichu"].ToString()
                        });
                    }

                    dtgLoaiTL.ItemsSource = listLoai;
                    dtgLoaiTL.Items.Refresh();
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

        private void resetValue()
        {
            btnSuaLoai.IsEnabled = false;
            btnXoaLoai.IsEnabled = false;
            txtGhiChuLoai.Text = "";
            txtTenLoai.Text = "";
        }

        private void btnThemLoai_Click(object sender, RoutedEventArgs e)
        {
            themLoai();
        }

        private void themLoai()
        {
            if (txtTenLoai.Text == "")
            {
                MessageBox.Show("Hãy nhập tên loại");
                txtTenLoai.Focus();
                return;
            }
            // add
            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_add_loai", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                try
                {
                    conn.Open();
                    // Add params nếu có
                    command.Parameters.AddWithValue("@tenLoai", txtTenLoai.Text);
                    command.Parameters.AddWithValue("@ghiChu", txtGhiChuLoai.Text);

                    var rdr = command.ExecuteNonQuery(); // Sử dụng khi không trả về dữ liệu
                    //var rdr = command.ExecuteReader(); // Sử dụng khi có trả về dữ liệu

                    resetValue();
                    get_dsLoai();
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

        private void btnSuaLoai_Click(object sender, RoutedEventArgs e)
        {
            if (txtTenLoai.Text == "")
            {
                MessageBox.Show("Hãy nhập tên loại");
                txtTenLoai.Focus();
                return;
            }
            // add
            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_update_loai", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                try
                {
                    conn.Open();
                    // Add params nếu có
                    command.Parameters.AddWithValue("@maLoai", loai.ma_loai);
                    command.Parameters.AddWithValue("@tenLoai", txtTenLoai.Text);
                    command.Parameters.AddWithValue("@ghiChu", txtGhiChuLoai.Text);

                    var rdr = command.ExecuteNonQuery(); // Sử dụng khi không trả về dữ liệu
                    //var rdr = command.ExecuteReader(); // Sử dụng khi có trả về dữ liệu

                    resetValue();
                    get_dsLoai();
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

        private void dtgLoaiTL_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            loai = listLoai[dtgLoaiTL.SelectedIndex];
            txtTenLoai.Text = loai.ten_loai; ;
            txtGhiChuLoai.Text = loai.ghichu;

            btnSuaLoai.IsEnabled = true;
            btnXoaLoai.IsEnabled = true;
        }

        private void gridLoai_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                themLoai();
        }

        private void btnXoaLoai_Click(object sender, RoutedEventArgs e)
        {
            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_delete_loai", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                try
                {
                    conn.Open();
                    // Add params nếu có
                    command.Parameters.AddWithValue("@ma_loai", loai.ma_loai);

                    var rdr = command.ExecuteNonQuery(); // Sử dụng khi không trả về dữ liệu
                    //var rdr = command.ExecuteReader(); // Sử dụng khi có trả về dữ liệu

                    resetValue();
                    get_dsLoai();
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
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  