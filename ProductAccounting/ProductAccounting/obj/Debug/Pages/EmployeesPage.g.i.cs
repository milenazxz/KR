﻿#pragma checksum "..\..\..\Pages\EmployeesPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7F05EFC22ECCCD82A643B6200304FBBE53F6383DF92824A72DABD768D3285511"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using ProductAccounting.Pages;
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


namespace ProductAccounting.Pages {
    
    
    /// <summary>
    /// EmployeesPage
    /// </summary>
    public partial class EmployeesPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 47 "..\..\..\Pages\EmployeesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddEmployeeBtn;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\Pages\EmployeesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ChangeEmployee;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\Pages\EmployeesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteEmployeeBtn;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\Pages\EmployeesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Close;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\Pages\EmployeesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid employeersGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/ProductAccounting;component/pages/employeespage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\EmployeesPage.xaml"
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
            this.AddEmployeeBtn = ((System.Windows.Controls.Button)(target));
            
            #line 47 "..\..\..\Pages\EmployeesPage.xaml"
            this.AddEmployeeBtn.Click += new System.Windows.RoutedEventHandler(this.AddEmployee);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ChangeEmployee = ((System.Windows.Controls.Button)(target));
            
            #line 48 "..\..\..\Pages\EmployeesPage.xaml"
            this.ChangeEmployee.Click += new System.Windows.RoutedEventHandler(this.ChangeSupplierBtn);
            
            #line default
            #line hidden
            return;
            case 3:
            this.DeleteEmployeeBtn = ((System.Windows.Controls.Button)(target));
            
            #line 49 "..\..\..\Pages\EmployeesPage.xaml"
            this.DeleteEmployeeBtn.Click += new System.Windows.RoutedEventHandler(this.DeleteEmployee);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Close = ((System.Windows.Controls.Button)(target));
            
            #line 50 "..\..\..\Pages\EmployeesPage.xaml"
            this.Close.Click += new System.Windows.RoutedEventHandler(this.CloseEmployeesPage);
            
            #line default
            #line hidden
            return;
            case 5:
            this.employeersGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

