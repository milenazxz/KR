﻿#pragma checksum "..\..\..\Pages\WriteOffsPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "54025F723F29B3887E97D75DC86F3B5C55B7334439054D6BD7372E2BF6397F7C"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using ProductAccounting.Forms;
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


namespace ProductAccounting.Forms {
    
    
    /// <summary>
    /// WriteOffsPage
    /// </summary>
    public partial class WriteOffsPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 48 "..\..\..\Pages\WriteOffsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddWriteOffsBtn;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\Pages\WriteOffsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteWriteOffsBtn;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\Pages\WriteOffsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Close;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\Pages\WriteOffsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid writeOffsGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/ProductAccounting;component/pages/writeoffspage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\WriteOffsPage.xaml"
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
            this.AddWriteOffsBtn = ((System.Windows.Controls.Button)(target));
            
            #line 48 "..\..\..\Pages\WriteOffsPage.xaml"
            this.AddWriteOffsBtn.Click += new System.Windows.RoutedEventHandler(this.AddWriteOffsBtn_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.DeleteWriteOffsBtn = ((System.Windows.Controls.Button)(target));
            
            #line 49 "..\..\..\Pages\WriteOffsPage.xaml"
            this.DeleteWriteOffsBtn.Click += new System.Windows.RoutedEventHandler(this.DeleteWriteOffs);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Close = ((System.Windows.Controls.Button)(target));
            
            #line 50 "..\..\..\Pages\WriteOffsPage.xaml"
            this.Close.Click += new System.Windows.RoutedEventHandler(this.CloseWriteOffsPage);
            
            #line default
            #line hidden
            return;
            case 4:
            this.writeOffsGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

