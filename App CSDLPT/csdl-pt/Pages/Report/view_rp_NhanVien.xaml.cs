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
    /// Interaction logic for view_rp_NhanVien.xaml
    /// </summary>
    public partial class view_rp_NhanVien : Page
    {
        string connectionString;
        public view_rp_NhanVien(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;

            get_NhanVien();
        }

        private void get_NhanVien()
        {
            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_get_dsNhanVien_tram", conn)
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

                    List<View.rp_NhanVien> listNhanVien = new List<View.rp_NhanVien>();
                    while (rdr.Read())
                    {
                        // dùng rdr["<tên cột>"] để lấy dữ liệu trả về từ sp
                        listNhanVien.Add(new View.rp_NhanVien()
                        {
                            ma_nhanvien = func.utils.convertUTF8(rdr["ma_nhanvien"].ToString()),
                            quyen = func.utils.convertUTF8(rdr["quyen"].ToString()),
                            diachi = func.utils.convertUTF8(rdr["diachi"].ToString()),
                            sdt = func.utils.convertUTF8(rdr["sdt"].ToString())
                        });
                    }

                    Reports.rp_NhanVien rpNV = new Reports.rp_NhanVien();
                    rpNV.SetDataSource(listNhanVien);

                    crystalRPNhanVien.ReportSource = rpNV;
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
            windowFormsHost.Height = gridRPNV.RowDefinitions[1].ActualHeight;
            windowFormsHost.Width = gridRPNV.ColumnDefinitions[1].ActualWidth;
        }
    }
}
