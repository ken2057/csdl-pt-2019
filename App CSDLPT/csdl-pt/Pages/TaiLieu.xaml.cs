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
    /// Interaction logic for TaiLieu.xaml
    /// </summary>
    public partial class TaiLieu : Page
    {
        string connectionString;
        List<EF.TaiLieu> listTaiLieu;
        public static EF.TaiLieu taiLieu;
        NavigationService navService;
        public TaiLieu(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            Init();
        }

        private void Init()
        {
            get_taiLieu();
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

                    listTaiLieu = new List<EF.TaiLieu>();
                    while (rdr.Read())
                    {
                        // dùng rdr["<tên cột>"] để lấy dữ liệu trả về từ sp
                        listTaiLieu.Add(new EF.TaiLieu()
                        {
                            ma_tailieu = rdr["ma_tailieu"].ToString(),
                            ten_tailieu = rdr["ten_tailieu"].ToString(),
                            ma_tacgia_1 = rdr["ten_tacgia"].ToString(),
                            ngonngu = rdr["ngonngu"].ToString(),
                            sl_kho = byte.Parse(rdr["sl_kho"].ToString()),
                        });
                    }

                    dtgTaiLieu.ItemsSource = listTaiLieu;
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

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            showOption pg = new showOption(connectionString);
            navService.Navigate(pg);
        }

        private void dtgTaiLieu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            taiLieu = (EF.TaiLieu)dtgTaiLieu.SelectedItem;
            navService = NavigationService.GetNavigationService(this);
            ChiTietTaiLieu pg = new ChiTietTaiLieu(connectionString);
            navService.Navigate(pg);
        }

        private void dtgTaiLieu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
