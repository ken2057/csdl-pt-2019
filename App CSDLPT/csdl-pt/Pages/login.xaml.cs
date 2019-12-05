using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Page
    {
        string connectionString;
        public login()
        {
            InitializeComponent();
        }

        private void _login()
        {
            connectionString = ConfigurationManager.ConnectionStrings["qltv2"].ConnectionString;
            connectionString += "User ID=" + txtUsername.Text + "; Password=" + txtPassword.Password;

            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // open if login success
                    NavigationService navService = NavigationService.GetNavigationService(this);
                    showOption sO = new showOption(connectionString);
                    navService.Navigate(sO);
                }
                catch (Exception)
                {
                    //MessageBox.Show(exception.ToString());
                    MessageBox.Show("Login fail", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void login_to_all_sv()
        {
            bool isLogin = false;
            List<string> listCS = new List<string>();

            // get all connecitonString exist
            listCS.Add(ConfigurationManager.ConnectionStrings["qltv_maychu"].ConnectionString);
            listCS.Add(ConfigurationManager.ConnectionStrings["qltv_tram_1"].ConnectionString);
            listCS.Add(ConfigurationManager.ConnectionStrings["qltv_tram_2"].ConnectionString);
            listCS.Add(ConfigurationManager.ConnectionStrings["qltv_tram_3"].ConnectionString);
            listCS.Add(ConfigurationManager.ConnectionStrings["qltv_tram_4"].ConnectionString);

            // try login each of them
            foreach (var cS in listCS)
            {
                string connectionString = cS + "User ID=" + txtUsername.Text + "; Password=" + txtPassword.Password;
                using (var conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        // open if login success
                        NavigationService navService = NavigationService.GetNavigationService(this);
                        showOption sO = new showOption(connectionString);
                        navService.Navigate(sO);
                        isLogin = true;
                        break;
                    }
                    catch (Exception)
                    {
                        //MessageBox.Show(exception.ToString());
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }

            // after finish, still not login => show error
            if (!isLogin)
                MessageBox.Show("Tài khoản hoặc mật khẩu không hợp lệ", "Lỗi");
        }

        private void gridLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
                _login();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            _login();
        }
    }
}
