﻿using System;
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
    /// Interaction logic for TacGia.xaml
    /// </summary>
    public partial class TacGia : Page
    {
        string connectionString;
        List<EF.TacGia> listTacGia;
        EF.TacGia tacGia;
        public TacGia(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;

            get_dsTacGia();
        }

        private void get_dsTacGia()
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
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navService = NavigationService.GetNavigationService(this);
            showOption pg = new showOption(connectionString);
            navService.Navigate(pg);
        }

        private void btnThemTaGia_Click(object sender, RoutedEventArgs e)
        {
            themTacGia();
        }
        private void themTacGia()
        {
            if (txtTenTacGia.Text == "")
            {
                MessageBox.Show("Hãy nhập tên tác giả!");
                txtTenTacGia.Focus();
                return;
            }
            // add
            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_add_tacgia", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                try
                {
                    conn.Open();
                    // Add params nếu có
                    command.Parameters.AddWithValue("@tentacgia", txtTenTacGia.Text);
                    command.Parameters.AddWithValue("@ghiChu", txtGhiChuTacGia.Text);

                    var rdr = command.ExecuteNonQuery(); // Sử dụng khi không trả về dữ liệu
                    //var rdr = command.ExecuteReader(); // Sử dụng khi có trả về dữ liệu

                    resetValue();
                    get_dsTacGia();
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

        private void btnSuaTacGia_Click(object sender, RoutedEventArgs e)
        {
            if (txtTenTacGia.Text == "")
            {
                MessageBox.Show("Hãy nhập tên tác giả!");
                txtTenTacGia.Focus();
                return;
            }
            using (var conn = new SqlConnection(connectionString))
            using (var command = new SqlCommand("sp_update_tacgia", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                try
                {
                    conn.Open();
                    //Add params
                    command.Parameters.AddWithValue("@ma", tacGia.ma_tacgia);
                    command.Parameters.AddWithValue("@ten", txtTenTacGia.Text);
                    command.Parameters.AddWithValue("@ghichu", txtGhiChuTacGia.Text);

                    var rdr = command.ExecuteNonQuery();
                    resetValue();
                    get_dsTacGia();
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

        private void dtgTacGia_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            tacGia = listTacGia[dtgTacGia.SelectedIndex];
            txtTenTacGia.Text = tacGia.ten_tacgia; ;
            txtGhiChuTacGia.Text = tacGia.ghichu;
            btnSuaTacGia.IsEnabled = true;
        }
        private void resetValue()
        {
            btnSuaTacGia.IsEnabled = false;
            txtGhiChuTacGia.Text = "";
            txtTenTacGia.Text = "";
        }

        private void gridTacGia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) themTacGia();
        }
    }
}
