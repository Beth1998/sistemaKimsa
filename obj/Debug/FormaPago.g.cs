﻿#pragma checksum "..\..\FormaPago.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7EE67A7A6FFAB798BED93EC120F2AD1865413EB054028054D46319EA72146E09"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using SISTEMA_KINSA;
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


namespace SISTEMA_KINSA {
    
    
    /// <summary>
    /// FormaPago
    /// </summary>
    public partial class FormaPago : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\FormaPago.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtHorasTrabajadas;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\FormaPago.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtHorasExtras;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\FormaPago.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtPagoHora;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\FormaPago.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtFormaPago;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\FormaPago.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dtpckFechaPago;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\FormaPago.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnGenerarPago;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\FormaPago.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbFormaPago;
        
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
            System.Uri resourceLocater = new System.Uri("/SISTEMA_KINSA;component/formapago.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\FormaPago.xaml"
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
            this.txtHorasTrabajadas = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.txtHorasExtras = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.txtPagoHora = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.txtFormaPago = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.dtpckFechaPago = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 6:
            this.btnGenerarPago = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\FormaPago.xaml"
            this.btnGenerarPago.Click += new System.Windows.RoutedEventHandler(this.btnGenerarPago_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.cmbFormaPago = ((System.Windows.Controls.ComboBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

