#pragma checksum "..\..\..\Pages\DocGia.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "DD343AAE485FB70506025AC43052E8DD46605649D523CFCE4B76E10052BAB2E4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using csdl_pt.Pages;


namespace csdl_pt.Pages
{


    /// <summary>
    /// DocGia
    /// </summary>
    public partial class DocGia : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector
    {


#line 12 "..\..\..\Pages\DocGia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gridNhanVien;

#line default
#line hidden


#line 30 "..\..\..\Pages\DocGia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtMaSinhVien;

#line default
#line hidden


#line 32 "..\..\..\Pages\DocGia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtHoVaTen;

#line default
#line hidden


#line 34 "..\..\..\Pages\DocGia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dpNgaySinh;

#line default
#line hidden


#line 50 "..\..\..\Pages\DocGia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtDiaChi;

#line default
#line hidden


#line 52 "..\..\..\Pages\DocGia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtSDT;

#line default
#line hidden


#line 53 "..\..\..\Pages\DocGia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dtgSinhVien;

#line default
#line hidden


#line 65 "..\..\..\Pages\DocGia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAdd;

#line default
#line hidden


#line 66 "..\..\..\Pages\DocGia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRefreshBeforeAdd;

#line default
#line hidden


#line 67 "..\..\..\Pages\DocGia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnUpdate;

#line default
#line hidden


#line 69 "..\..\..\Pages\DocGia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnBack;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/csdl-pt;component/pages/docgia.xaml", System.UriKind.Relative);

#line 1 "..\..\..\Pages\DocGia.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);

#line default
#line hidden
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.gridNhanVien = ((System.Windows.Controls.Grid)(target));
                    return;
                case 2:
                    this.txtMaSinhVien = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 3:
                    this.txtHoVaTen = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 4:
                    this.dpNgaySinh = ((System.Windows.Controls.DatePicker)(target));
                    return;
                case 5:
                    this.txtDiaChi = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 6:
                    this.txtSDT = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 7:
                    this.dtgSinhVien = ((System.Windows.Controls.DataGrid)(target));

#line 56 "..\..\..\Pages\DocGia.xaml"
                    this.dtgSinhVien.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.dtgSinhVien_MouseDoubleClick);

#line default
#line hidden

#line 56 "..\..\..\Pages\DocGia.xaml"
                    this.dtgSinhVien.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.dtgSinhVien_SelectionChanged);

#line default
#line hidden
                    return;
                case 8:
                    this.btnAdd = ((System.Windows.Controls.Button)(target));

#line 65 "..\..\..\Pages\DocGia.xaml"
                    this.btnAdd.Click += new System.Windows.RoutedEventHandler(this.btnAdd_Click);

#line default
#line hidden
                    return;
                case 9:
                    this.btnRefreshBeforeAdd = ((System.Windows.Controls.Button)(target));

#line 66 "..\..\..\Pages\DocGia.xaml"
                    this.btnRefreshBeforeAdd.Click += new System.Windows.RoutedEventHandler(this.btnRefreshBeforeAdd_Click);

#line default
#line hidden
                    return;
                case 10:
                    this.btnUpdate = ((System.Windows.Controls.Button)(target));

#line 67 "..\..\..\Pages\DocGia.xaml"
                    this.btnUpdate.Click += new System.Windows.RoutedEventHandler(this.btnUpdate_Click);

#line default
#line hidden
                    return;
                case 11:
                    this.btnDelete = ((System.Windows.Controls.Button)(target));

#line 68 "..\..\..\Pages\DocGia.xaml"
                    this.btnDelete.Click += new System.Windows.RoutedEventHandler(this.btnDelete_Click);

#line default
#line hidden
                    return;
                case 12:
                    this.btnBack = ((System.Windows.Controls.Button)(target));

#line 69 "..\..\..\Pages\DocGia.xaml"
                    this.btnBack.Click += new System.Windows.RoutedEventHandler(this.btnBack_Click);

#line default
#line hidden
                    return;
            }
            this._contentLoaded = true;
        }
    }
}

