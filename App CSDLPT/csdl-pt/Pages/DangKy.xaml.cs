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
    /// Interaction logic for DangKy.xaml
    /// </summary>
    public partial class DangKy : Page
    {
        string connectionString;
        List<EF.DangKy> listDangKy;
        EF.DangKy dangKy;
        EF.DocGia docgia;

        public DangKy(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;

            get_dsDangKy();
        }

        private void get_dsDangKy()
        {
            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_get_dsDangKy", conn) 
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                try
                {
                    conn.Open();
                    var rdr = command.ExecuteReader();

                    listDangKy = new List<EF.DangKy>();
                    while (rdr.Read())
                    {
                        listDangKy.Add(new EF.DangKy()
                        {
                            ngaygio_dk = DateTime.Parse(rdr["ngaygio_dk"].ToString()),
                            ma_sinhvien = rdr["ma_sinhvien"].ToString(),
                            ma_tailieu = rdr["ma_tailieu"].ToString(),
                            ghichu = rdr["ghichu"].ToString()
                        });
                    }
                    dtgDangKy.ItemsSource = listDangKy; // tam
                    dtgDangKy.Items.Refresh();
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

        private void btnThemDK_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            AddDangKy pg = new AddDangKy(connectionString);
            navService.Navigate(pg);
        }

        private void btnXoaDK_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có Chắc muốn xóa đăng ký này không?", "My App", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    xoaDangKy();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void xoaDangKy()
        {
            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_xoa_dangky", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                try
                {
                    conn.Open();
                    //Add params
                    command.Parameters.AddWithValue("@maSV", dangKy.ma_sinhvien);
                    command.Parameters.AddWithValue("@maTL", dangKy.ma_tailieu);

                    var rdr = command.ExecuteNonQuery();
                    resetValue();
                    get_dsDangKy();
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

        private void btnChoMuon_Click(object sender, RoutedEventArgs e)
        {
            //Xóa đăng ký này đồng thời thêm vào bản Muon thông tin
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            Muon pg = new Muon(connectionString);
            navService.Navigate(pg);
        }

        private void dtgDangKy_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dangKy = (EF.DangKy)dtgDangKy.SelectedItem;
            txtTenDocGia.Text = dangKy.ma_sinhvien;
            txtTenTaiLieu.Text = dangKy.ma_tailieu;
            txtNgayGioDK.Text = dangKy.ngaygio_dk.ToString();
            txtGhiChu.Text = dangKy.ghichu;
            btnXoaDK.IsEnabled = true;
            //đếm số lượng bản sao tài liệu nếu >0 thì btnChoMuon.IsEnable = true
        }

        private void resetValue()
        {
            btnXoaDK.IsEnabled = false;
            btnChoMuon.IsEnabled = false;
            txtTenDocGia.Text = "";
            txtTenTaiLieu.Text = "";
            txtNgayGioDK.Text = "";
            txtGhiChu.Text = "";
        }
    }
}
