﻿#pragma checksum "..\..\..\Pages\Muon.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "878940EB28BE09855FE523AFD0D802C11EDAA9DE65C8E687E3DCD88D592478BE"
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
using System.Windows.Forms.Integration;
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


namespace csdl_pt.Pages {
    
    
    /// <summary>
    /// Muon
    /// </summary>
    public partial class Muon : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 36 "..\..\..\Pages\Muon.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dtgMuon;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\Pages\Muon.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnBack;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\Pages\Muon.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMuon;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\Pages\Muon.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnUpdateMuon;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\Pages\Muon.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDangKy;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/csdl-pt;component/pages/muon.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\Muon.xaml"
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
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.dtgMuon = ((System.Windows.Controls.DataGrid)(target));
            
            #line 38 "..\..\..\Pages\Muon.xaml"
            this.dtgMuon.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.DtgMuon_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnBack = ((System.Windows.Controls.Button)(target));
            
            #line 55 "..\..\..\Pages\Muon.xaml"
            this.btnBack.Click += new System.Windows.RoutedEventHandler(this.BtnBack_Click_1);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnMuon = ((System.Windows.Controls.Button)(target));
            
            #line 56 "..\..\..\Pages\Muon.xaml"
            this.btnMuon.Click += new System.Windows.RoutedEventHandler(this.BtnMuon_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnUpdateMuon = ((System.Windows.Controls.Button)(target));
            
            #line 57 "..\..\..\Pages\Muon.xaml"
            this.btnUpdateMuon.Click += new System.Windows.RoutedEventHandler(this.BtnUpdateMuon_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnDangKy = ((System.Windows.Controls.Button)(target));
            
            #line 58 "..\..\..\Pages\Muon.xaml"
            this.btnDangKy.Click += new System.Windows.RoutedEventHandler(this.btnDangKy_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

