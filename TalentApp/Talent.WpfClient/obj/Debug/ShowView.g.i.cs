﻿#pragma checksum "..\..\ShowView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "285F22F9F7E1C738B920336E01CCF914"
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
using Talent.Domain;
using Talent.WpfClient;
using Talent.WpfClient.Converters;


namespace Talent.WpfClient {
    
    
    /// <summary>
    /// ShowView
    /// </summary>
    public partial class ShowView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 56 "..\..\ShowView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TitleTextBox;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\ShowView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox MpaaRatingComboBox;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\ShowView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox LengthTextBox;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\ShowView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker TheatricalReleaseDatePicker;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\ShowView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker DvdReleaseDatePicker;
        
        #line default
        #line hidden
        
        
        #line 128 "..\..\ShowView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox GenresTextBox;
        
        #line default
        #line hidden
        
        
        #line 139 "..\..\ShowView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button GenresEditButton;
        
        #line default
        #line hidden
        
        
        #line 153 "..\..\ShowView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid CreditsDataGrid;
        
        #line default
        #line hidden
        
        
        #line 162 "..\..\ShowView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridComboBoxColumn CreditTypeDropDownColumn;
        
        #line default
        #line hidden
        
        
        #line 171 "..\..\ShowView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridComboBoxColumn PersonDropDownColumn;
        
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
            System.Uri resourceLocater = new System.Uri("/Talent.WpfClient;component/showview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ShowView.xaml"
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
            this.TitleTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.MpaaRatingComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.LengthTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.TheatricalReleaseDatePicker = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 5:
            this.DvdReleaseDatePicker = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 6:
            this.GenresTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.GenresEditButton = ((System.Windows.Controls.Button)(target));
            
            #line 140 "..\..\ShowView.xaml"
            this.GenresEditButton.Click += new System.Windows.RoutedEventHandler(this.GenresEditButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.CreditsDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 9:
            this.CreditTypeDropDownColumn = ((System.Windows.Controls.DataGridComboBoxColumn)(target));
            return;
            case 10:
            this.PersonDropDownColumn = ((System.Windows.Controls.DataGridComboBoxColumn)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
