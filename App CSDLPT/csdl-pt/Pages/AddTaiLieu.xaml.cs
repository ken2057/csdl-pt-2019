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
    /// Interaction logic for AddTaiLieu.xaml
    /// </summary>
    public partial class AddTaiLieu : Page
    {
        string connectionString;
        TaiLieu _taiLieu;
        List<EF.TacGia> listTacGia;
        List<EF.TacGia> listTacGiaChecked;
        EF.LoaiTaiLieu LoaiTaiLieu;
        List<EF.LoaiTaiLieu> loaiTaiLieus;
        public AddTaiLieu(string connectionString, TaiLieu taiLieu)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            _taiLieu = taiLieu;
            Init();
        }

        private void Init()
        {
            Get_LoaiTaiLieu();
            get_dsTacGia();
            cbTheLoai.ItemsSource = loaiTaiLieus;
            cbTheLoai.DisplayMemberPath = "ten_loai";
        }

        private void Get_LoaiTaiLieu()
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
                    var rdr = command.ExecuteReader();
                    loaiTaiLieus = new List<EF.LoaiTaiLieu>();
                    while (rdr.Read())
                    {
                        LoaiTaiLieu = new EF.LoaiTaiLieu();
                        LoaiTaiLieu.ma_loai = rdr["ma_loai"].ToString();
                        LoaiTaiLieu.ten_loai = rdr["ten_loai"].ToString();
                        loaiTaiLieus.Add(LoaiTaiLieu);

                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error sp_get_dsLoai");
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public void get_dsTacGia()
        {
            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_get_dsTacGia", conn)
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

                    listTacGia = new List<EF.TacGia>();
                    while (rdr.Read())
                    {
                        // dùng rdr["<tên cột>"] để lấy dữ liệu trả về từ sp
                        listTacGia.Add(new EF.TacGia()
                        {
                            ma_tacgia = rdr["ma_tacgia"].ToString(),
                            ten_tacgia = rdr["ten_tacgia"].ToString(),
                            ghichu = rdr["ghichu"].ToString()
                        });
                    }

                    dtgTacGia.ItemsSource = listTacGia;
                    dtgTacGia.Items.Refresh();
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
        private void txtSearchTacGia_KeyUp(object sender, KeyEventArgs e)
        {
            var filtered = listTacGia.Where(tacGia => tacGia.ten_tacgia.ToLower().Contains(txtSearchTacGia.Text));
            dtgTacGia.ItemsSource = filtered;
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            navService.Navigate(_taiLieu);
        }
        public static T FindChild<T>(DependencyObject parent, string childName)
           where T : DependencyObject
        {
            // Confirm parent is valid.  
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child 
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree 
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child.  
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search 
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name 
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found. 
                    foundChild = (T)child;
                    break;
                }
            }
            return foundChild;
        }

        
        private void btnThemTaiLieu_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(((LoaiTaiLieu)cbTheLoai.SelectedItem).ma_loai.ToString());
            //CheckBox checkBox = FindChild<CheckBox>(dtgTacGia.SelectedItem, "chb");
            //if (checkBox != null && checkBox.IsChecked == true)
            //{
            //    //some code
            //}
            listTacGiaChecked = new List<EF.TacGia>(); 
            for (int i = 0; i < dtgTacGia.Items.Count; i++)
            {
                DataGridRow row = (DataGridRow)dtgTacGia.ItemContainerGenerator.ContainerFromIndex(i);
                CheckBox checkBox = FindChild<CheckBox>(row, "cbCheckTG");
                if (checkBox != null && checkBox.IsChecked == true)
                {
                    listTacGiaChecked.Add((EF.TacGia)row.Item);
                }
            }
            //string themp = "";
            //foreach (var item in listTacGiaChecked)
            //{
            //    themp += item.ma_tacgia + "\n";
            //}
            //themp += ((EF.LoaiTaiLieu)cbTheLoai.SelectedItem).ma_loai +"\n";
            //themp += txtNgonNgu.Text+"\n" + txtLoaiBia.Text+"\n"+txtSoLuong.Text+ "\n" + txtTenTaiLieu.Text+ "\n"+ txtTomTat.Text;

            //MessageBox.Show(themp);
            themTaiLieu();

            //MessageBox.Show(((TacGia)dtgTacGia.))
        }

        private void themTaiLieu()
        {
            if (txtTenTaiLieu.Text == "")
            {
                MessageBox.Show("Hãy tên tài liệu");
                txtTenTaiLieu.Focus();
                return;
            }
            if (cbTheLoai.Text == "")
            {
                MessageBox.Show("Hãy chọn thể loại");
                cbTheLoai.Focus();
                return;
            }
            if (txtNgonNgu.Text == "")
            {
                MessageBox.Show("Hãy nhập ngôn ngữ");
                txtNgonNgu.Focus();
                return;
            }
            if (txtLoaiBia.Text == "")
            {
                MessageBox.Show("Hãy nhập loại bìa");
                txtLoaiBia.Focus();
                return;
            }
            if (txtSoLuong.Text == "")
            {
                MessageBox.Show("Hãy nhập số lượng");
                txtSoLuong.Focus();
                return;
            }
            if (txtGia.Text == "")
            {
                MessageBox.Show("Hãy nhập giá");
                txtGia.Focus();
                return;
            }
            // add
            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_add_tailieu", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                try
                {
                    conn.Open();
                    // Add params nếu có

                    command.Parameters.AddWithValue("@maTG2", DBNull.Value);
                    command.Parameters.AddWithValue("@maTG3", DBNull.Value);
                    int i = 1;
                    foreach (var item in listTacGiaChecked)
                    {
                        command.Parameters.AddWithValue("@maTG"+i, item.ma_tacgia);
                        i++;
                    }
                    command.Parameters.AddWithValue("@maLoaiTL", ((LoaiTaiLieu)cbTheLoai.SelectedItem).ma_loai);
                    command.Parameters.AddWithValue("@ngonNgu", txtNgonNgu.Text);
                    command.Parameters.AddWithValue("@bia", txtLoaiBia.Text);
                    command.Parameters.AddWithValue("@tinhTrang", 'Y');
                    command.Parameters.AddWithValue("@giaTL", txtGia.Text);
                    command.Parameters.AddWithValue("@tenTaiLieu", txtTenTaiLieu.Text);
                    command.Parameters.AddWithValue("@soLuong", txtSoLuong.Text);
                    command.Parameters.AddWithValue("@tomTat", txtSoLuong.Text);
                    command.Parameters.AddWithValue("@ngayPhatHanh", dpNgayPhatHanh.SelectedDate);

                    var rdr = command.ExecuteNonQuery(); // Sử dụng khi không trả về dữ liệu
                    //var rdr = command.ExecuteReader(); // Sử dụng khi có trả về dữ liệ
                    MessageBox.Show("Thêm tài liệu thành công");
                    Init();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error sp_add_tailieu");
                }
                finally
                {
                    conn.Close();
                }

            }
        }

        private void btnThemTacGia_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            TacGia pg = new TacGia(connectionString, this);
            navService.Navigate(pg);
        }
    }
}
