   M S B A M L   `   `   `  ����B  >csdl-pt, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:csdl_pt.Pagescsdl_pt.Pages  5          csdl_pt.Pages.TaiLieu   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:csdl_pt.Pages     5        N� Title$  TaiLieu��5	      .��5      �  � RowDefinitions 5   
   �� 5      $
��0.2* �6   �� 5      $
��0.5* �6   �� 5      �� 5      �� 5      $
��0.2* �6    � ColumnDefinitions 5   
   �� 5      $
��0.2* �6   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      $
��0.2* �6   .+�5   
   �� .+�5      �� -   5        $��btnBack��5   (   $��
Quay lại��6   67   �� 5       $��Tài liệu��6   5!      �� 5#   
   -   5        $��btnThemTaGia��5$      $��Thêm��6!   $��1��5%      $��4��6   $
��10pxq�5&      5'      &   System.Windows.Controls.DataGrid  5)   
   -   5          Name$ 
dtgTaiLieu��5)      $��1��5*      $��2��6#   $��2��60   $��5��6A      AutoGenerateColumns .     CanUserAddRows .     Columns 5.      0  *System.Windows.Controls.DataGridTextColumn  5/      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 1* 6%      Header$ Tên tài liệu��60   	   
IsReadOnly	 . 1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6F   �� *ten_tailieu+6f     50      $ 1* 6%   $ Tên tác giả��60   	 . 
 6E   �� *ma_tacgia_1+6e     51      $ 1* 6%   $ Ngôn ngữ��60   	 . 
 6B   �� *	ngonngu+6^     52      $ 1* 6%   $ Số lượng��60   	 . 
 6B   �� *sl_kho+6]   54      55      56      