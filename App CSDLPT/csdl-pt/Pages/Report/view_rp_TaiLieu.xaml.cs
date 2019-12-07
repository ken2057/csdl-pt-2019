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
    /// Interaction logic for view_rp_TaiLieu.xaml
    /// </summary>
    public partial class view_rp_TaiLieu : Page
    {
        string connectionString;
        Dictionary<string, string> dictSLBanSao;

        public view_rp_TaiLieu(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;

            get_NhanVien();
        }

        private void get_NhanVien()
        {
            get_sl_BanSao();

            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_get_ThongTin_TaiLieu", conn)
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

                    List<View.rp_TaiLieu> listNhanVien = new List<View.rp_TaiLieu>();
                    while (rdr.Read())
                    {
                        // dùng rdr["<tên cột>"] để lấy dữ liệu trả về từ sp
                        listNhanVien.Add(new View.rp_TaiLieu()
                        {
                            ten_tailieu = func.utils.convertUTF8(rdr["ten_tailieu"].ToString()),
                            loai = func.utils.convertUTF8(rdr["ten_loai"].ToString()),
                            tinh_trang = func.utils.convertUTF8(rdr["tinhtrang"].ToString()),
                            gia = func.utils.outputMoney(func.utils.convertUTF8(rdr["gia"].ToString())),
                            ngonngu = func.utils.convertUTF8(rdr["ngonngu"].ToString()),
                            sl = dictSLBanSao[rdr["ma_tailieu"].ToString()]
                        });
                    }

                    Reports.rp_TaiLieu rpNV = new Reports.rp_TaiLieu();
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

        private void get_sl_BanSao()
        {
            using (var conn = new SqlConnection(connectionString)) 
            using (var command = new SqlCommand("sp_get_bansao_sl", conn)
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

                    dictSLBanSao = new Dictionary<string, string>();
                    while (rdr.Read())
                    {
                        dictSLBanSao.Add(rdr["ma_tailieu"].ToString(), rdr["sl"].ToString());
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
