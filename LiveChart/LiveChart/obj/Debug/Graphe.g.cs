﻿#pragma checksum "..\..\Graphe.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "F9E68D43E5540506BAFD9E8B7D993021979F1290"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using LiveChart;
using LiveCharts.Wpf;
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


namespace LiveChart {
    
    
    /// <summary>
    /// Graphe
    /// </summary>
    public partial class Graphe : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\Graphe.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LiveCharts.Wpf.CartesianChart Chart;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\Graphe.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LiveCharts.Wpf.LineSeries X;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\Graphe.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LiveCharts.Wpf.Axis axisX;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\Graphe.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LiveCharts.Wpf.Axis Y;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\Graphe.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox wilaya;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\Graphe.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker date1;
        
        #line default
        #line hidden
        
        
        #line 115 "..\..\Graphe.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker date2;
        
        #line default
        #line hidden
        
        
        #line 121 "..\..\Graphe.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox TemperatureCheckbox;
        
        #line default
        #line hidden
        
        
        #line 122 "..\..\Graphe.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox HumiditeCheckbox;
        
        #line default
        #line hidden
        
        
        #line 123 "..\..\Graphe.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox VitesseVentCheckbox;
        
        #line default
        #line hidden
        
        
        #line 124 "..\..\Graphe.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox DirectionVentCheckbox;
        
        #line default
        #line hidden
        
        
        #line 125 "..\..\Graphe.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox PrecipitationCheckbox;
        
        #line default
        #line hidden
        
        
        #line 130 "..\..\Graphe.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock titre;
        
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
            System.Uri resourceLocater = new System.Uri("/LiveChart;component/graphe.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Graphe.xaml"
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
            this.Chart = ((LiveCharts.Wpf.CartesianChart)(target));
            return;
            case 2:
            this.X = ((LiveCharts.Wpf.LineSeries)(target));
            return;
            case 3:
            this.axisX = ((LiveCharts.Wpf.Axis)(target));
            return;
            case 4:
            this.Y = ((LiveCharts.Wpf.Axis)(target));
            return;
            case 5:
            this.wilaya = ((System.Windows.Controls.ComboBox)(target));
            
            #line 57 "..\..\Graphe.xaml"
            this.wilaya.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Wilaya_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.date1 = ((System.Windows.Controls.DatePicker)(target));
            
            #line 111 "..\..\Graphe.xaml"
            this.date1.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.Date1_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.date2 = ((System.Windows.Controls.DatePicker)(target));
            
            #line 115 "..\..\Graphe.xaml"
            this.date2.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.Date2_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.TemperatureCheckbox = ((System.Windows.Controls.CheckBox)(target));
            
            #line 121 "..\..\Graphe.xaml"
            this.TemperatureCheckbox.Checked += new System.Windows.RoutedEventHandler(this.TemperatureCheckbox_Checked);
            
            #line default
            #line hidden
            
            #line 121 "..\..\Graphe.xaml"
            this.TemperatureCheckbox.Unchecked += new System.Windows.RoutedEventHandler(this.TemperatureCheckbox_Unchecked_1);
            
            #line default
            #line hidden
            return;
            case 9:
            this.HumiditeCheckbox = ((System.Windows.Controls.CheckBox)(target));
            
            #line 122 "..\..\Graphe.xaml"
            this.HumiditeCheckbox.Checked += new System.Windows.RoutedEventHandler(this.HumiditeCheckbox_Checked);
            
            #line default
            #line hidden
            
            #line 122 "..\..\Graphe.xaml"
            this.HumiditeCheckbox.Unchecked += new System.Windows.RoutedEventHandler(this.HumiditeCheckbox_Unchecked);
            
            #line default
            #line hidden
            return;
            case 10:
            this.VitesseVentCheckbox = ((System.Windows.Controls.CheckBox)(target));
            
            #line 123 "..\..\Graphe.xaml"
            this.VitesseVentCheckbox.Checked += new System.Windows.RoutedEventHandler(this.VitesseVentCheckbox_Checked);
            
            #line default
            #line hidden
            
            #line 123 "..\..\Graphe.xaml"
            this.VitesseVentCheckbox.Unchecked += new System.Windows.RoutedEventHandler(this.VitesseVentCheckbox_Unchecked);
            
            #line default
            #line hidden
            return;
            case 11:
            this.DirectionVentCheckbox = ((System.Windows.Controls.CheckBox)(target));
            
            #line 124 "..\..\Graphe.xaml"
            this.DirectionVentCheckbox.Checked += new System.Windows.RoutedEventHandler(this.DirectionVentCheckbox_Checked);
            
            #line default
            #line hidden
            
            #line 124 "..\..\Graphe.xaml"
            this.DirectionVentCheckbox.Unchecked += new System.Windows.RoutedEventHandler(this.DirectionVentCheckbox_Unchecked);
            
            #line default
            #line hidden
            return;
            case 12:
            this.PrecipitationCheckbox = ((System.Windows.Controls.CheckBox)(target));
            
            #line 125 "..\..\Graphe.xaml"
            this.PrecipitationCheckbox.Checked += new System.Windows.RoutedEventHandler(this.PrecipitationCheckbox_Checked);
            
            #line default
            #line hidden
            
            #line 125 "..\..\Graphe.xaml"
            this.PrecipitationCheckbox.Unchecked += new System.Windows.RoutedEventHandler(this.PrecipitationCheckbox_Unchecked);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 128 "..\..\Graphe.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            this.titre = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

