﻿#pragma checksum "..\..\..\..\Windows\AddEditReservation.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "E6B1B0B38A96FE2BFB58AC5E26C63386694C9C89"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using HotelReservations.Windows;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace HotelReservations.Windows {
    
    
    /// <summary>
    /// AddEditReservation
    /// </summary>
    public partial class AddEditReservation : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 7 "..\..\..\..\Windows\AddEditReservation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal HotelReservations.Windows.AddEditReservation AddEditReservationWindow;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\..\Windows\AddEditReservation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox RoomNumberComboBox;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\Windows\AddEditReservation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox GuestListBox;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\Windows\AddEditReservation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ReservationTypeCB;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\Windows\AddEditReservation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker StartDateTimePicker;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\..\Windows\AddEditReservation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker EndDateTimePicker;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\Windows\AddEditReservation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TotalValueTextBox;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.12.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/HotelReservations;component/windows/addeditreservation.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Windows\AddEditReservation.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.12.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.AddEditReservationWindow = ((HotelReservations.Windows.AddEditReservation)(target));
            return;
            case 2:
            this.RoomNumberComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 17 "..\..\..\..\Windows\AddEditReservation.xaml"
            this.RoomNumberComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.RoomNumberComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.GuestListBox = ((System.Windows.Controls.ListBox)(target));
            
            #line 23 "..\..\..\..\Windows\AddEditReservation.xaml"
            this.GuestListBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.GuestListBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ReservationTypeCB = ((System.Windows.Controls.ComboBox)(target));
            
            #line 36 "..\..\..\..\Windows\AddEditReservation.xaml"
            this.ReservationTypeCB.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ReservationTypeCB_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.StartDateTimePicker = ((System.Windows.Controls.DatePicker)(target));
            
            #line 41 "..\..\..\..\Windows\AddEditReservation.xaml"
            this.StartDateTimePicker.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.StartDateTime_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.EndDateTimePicker = ((System.Windows.Controls.DatePicker)(target));
            
            #line 46 "..\..\..\..\Windows\AddEditReservation.xaml"
            this.EndDateTimePicker.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.EndDateTime_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.TotalValueTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            
            #line 62 "..\..\..\..\Windows\AddEditReservation.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CancelBtn_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 63 "..\..\..\..\Windows\AddEditReservation.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveBtn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

