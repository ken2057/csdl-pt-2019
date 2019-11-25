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
