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
    /// Interaction logic for DocGia.xaml
    /// </summary>
    public partial class DocGia : Page
    {
        string connectionString;
        List<EF.DocGia> listDocGia;
        EF.DocGia docgia;
        public DocGia(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            Init();
        }

        private void Init()
        {
            resetValue();
            get_DocGia();
            InitState();
        }

        private void InitState()
        {
            txtMaSinhVien.IsEnabled = false;
            btnAdd.Visibility = Visibility.Hidden;
            btnRefreshBeforeAdd.Visibility = Visibility.Visible;
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
                    while (rdr.Read())
                    {
                        // dùng rdr["<tên cột>"] để lấy dữ liệu trả về từ sp
                        listDocGia.Add(new EF.DocGia()
                        {
                            ma_sinhvien = rdr["ma_sinhvien"].ToString(),
                            hoten = rdr["hoten"].ToString(),
                            NgaySinh = DateTime.Parse(rdr["NgaySinh"].ToString()),
                            diachi = rdr["diachi"].ToString(),
                            sdt = rdr["sdt"].ToString()
                        });
                    }

                    dtgSinhVien.ItemsSource = listDocGia;
                    //dtgSinhVien.SelectedIndex = 0;
                    dtgSinhVien.Items.Refresh();
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

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            showOption pg = new showOption(connectionString);
            navService.Navigate(pg);
        }
        private void dtgSinhVien_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void dtgSinhVien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InitState();
            if (dtgSinhVien.SelectedIndex == -1)
            {
                resetValue();
                return;
            }
            docgia = listDocGia[dtgSinhVien.SelectedIndex];
            txtMaSinhVien.Text = docgia.ma_sinhvien;
            txtHoVaTen.Text = docgia.hoten;
            dpNgaySinh.SelectedDate = docgia.NgaySinh;
            txtDiaChi.Text = docgia.diachi;
            txtSDT.Text = docgia.sdt;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            suaDocGia();
            Init();
        }

        private void suaDocGia()
        {
            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_update_docgia", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                try
                {
                    conn.Open();
                    // Add params nếu có
                    command.Parameters.AddWithValue("@maSinhVien", txtMaSinhVien.Text);
                    command.Parameters.AddWithValue("@hoTen", txtHoVaTen.Text);
                    command.Parameters.AddWithValue("@ngaySinh", dpNgaySinh.Text);
                    command.Parameters.AddWithValue("@diaChi", txtDiaChi.Text);
                    command.Parameters.AddWithValue("@sDT", txtSDT.Text);

                    //var rdr = command.ExecuteNonQuery(); // Sử dụng khi không trả về dữ liệu
                    var rdr = command.ExecuteReader(); // Sử dụng khi có trả về dữ liệu
                    while (rdr.Read())
                    {
                        MessageBox.Show(
                        rdr["ma_sinhvien"].ToString() + "\n" +
                        rdr["hoten"].ToString() + "\n" +
                        rdr["NgaySinh"].ToString() + "\n" +
                        rdr["diachi"].ToString() + "\n" +
                        rdr["sdt"].ToString()
                        , "Sửa thành công đọc giả"
                        );
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error sua_docgia");
                }
                finally
                {
                    conn.Close();
                }

            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            themDocGia();
        }

        private void themDocGia()
        {
            if (txtMaSinhVien.Text == "")
            {
                MessageBox.Show("Hãy nhập mã độc giả");
                txtMaSinhVien.Focus();
                return;
            }
            if (txtHoVaTen.Text == "")
            {
                MessageBox.Show("Hãy nhập họ và tên");
                txtHoVaTen.Focus();
                return;
            }
            if (txtDiaChi.Text == "")
            {
                MessageBox.Show("Hãy nhập địa chỉ");
                txtDiaChi.Focus();
                return;
            }
            if (txtSDT.Text == "")
            {
                MessageBox.Show("Hãy nhập số điện thoại");
                txtSDT.Focus();
                return;
            }
            // add
            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_add_docgia", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                try
                {
                    conn.Open();
                    // Add params nếu có
                    command.Parameters.AddWithValue("@maSinhVien", txtMaSinhVien.Text);
                    command.Parameters.AddWithValue("@hoTen", txtHoVaTen.Text);
                    command.Parameters.AddWithValue("@ngaySinh", dpNgaySinh.Text);
                    command.Parameters.AddWithValue("@diaChi", txtDiaChi.Text);
                    command.Parameters.AddWithValue("@sDT", txtSDT.Text);

                    var rdr = command.ExecuteNonQuery(); // Sử dụng khi không trả về dữ liệu
                    //var rdr = command.ExecuteReader(); // Sử dụng khi có trả về dữ liệu

                    Init();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error them_docgia");
                }
                finally
                {
                    conn.Close();
                }

            }
        }

        private void resetValue()
        {
            txtMaSinhVien.Text = "";
            txtHoVaTen.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
        }

        private void btnRefreshBeforeAdd_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Bạn muốn thêm độc giả", "Thêm độc giả", MessageBoxButton.YesNoCancel);
            switch (messageBoxResult)
            {
                case MessageBoxResult.Yes:
                    txtMaSinhVien.IsEnabled = true;
                    resetValue();
                    btnAdd.Visibility = Visibility.Visible;
                    btnRefreshBeforeAdd.Visibility = Visibility.Hidden;
                    break;
                case MessageBoxResult.No:
                    txtMaSinhVien.IsEnabled = false;
                    return;
                default:
                    txtMaSinhVien.IsEnabled = false;
                    return;
            }
        }
    }
}
