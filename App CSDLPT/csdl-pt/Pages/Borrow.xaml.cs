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
    /// Interaction logic for Borrow.xaml
    /// </summary>
    public partial class Borrow : Page
    {
        string connectionString;
        
        List<EF.Muon> listMuon;
        public static string dsMuon;
        public Borrow(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            get_dsMuon();
        }
        private void get_dsMuon()
        {

            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_get_dsMuon", conn)
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

                    listMuon = new List<EF.Muon>();
                    while (rdr.Read())
                    {
                        // dùng rdr["<tên cột>"] để lấy dữ liệu trả về từ sp
                        listMuon.Add(new EF.Muon()
                        {
                            ma_tailieu = rdr["ma_tailieu"].ToString(),
                            ma_bansao = rdr["ma_bansao"].ToString(),
                            ma_sinhvien = rdr["ma_sinhvien"].ToString(),
                            ma_nhanvien_dua = rdr["ma_nhanvien_dua"].ToString(),
                            ngay_hethan = DateTime.Parse(rdr["ngay_hethan"].ToString()),
                            ngay_muon = DateTime.Parse(rdr["ngay_muon"].ToString()),
                            tien_datcoc = Decimal.Parse(rdr["tien_datcoc"].ToString()),
                        });
                    }

                    dtgBorrow.ItemsSource = listMuon;
                    dtgBorrow.Items.Refresh();
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
    
        private void DtgBorrow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            var obj = dtgBorrow.SelectedItem;
            dsMuon = (obj as EF.Muon).ma_tailieu;
            dsMuon = (obj as EF.Muon).ma_bansao;
            dsMuon = (obj as EF.Muon).ma_sinhvien;
            dsMuon = (obj as EF.Muon).ma_nhanvien_dua;
            dsMuon = (obj as EF.Muon).ngay_muon.ToString();
            dsMuon = (obj as EF.Muon).ngay_hethan.ToString();
            dsMuon = (obj as EF.Muon).tien_datcoc.ToString();
            dsMuon = (obj as EF.Muon).tien_datcoc.ToString();

            btnUpdate_Muon.IsEnabled = true;
            btnDelete_Muon.IsEnabled = true;
        }

        private void BtnAdd_Muon_Click(object sender, RoutedEventArgs e)
        {

            NavigationService navService = NavigationService.GetNavigationService(this);
            Add_Muon pg = new Add_Muon(connectionString);
            navService.Navigate(pg);
        }

        private void BtnUpdate_Muon_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            Update_Muon pg = new Update_Muon(connectionString);
            navService.Navigate(pg);
        }

        private void BtnDelete_Muon_Click(object sender, RoutedEventArgs e)
        {
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            Muon pg = new Muon(connectionString);
            navService.Navigate(pg);
        }
    }
}
