﻿#pragma checksum "C:\Users\Desmond\Documents\Visual Studio 2017\Projects\Universal\Truudus\Truudus\Pages\salProfile.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "15FBFEF06D11F9AA679335FE7B4C63A5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Truudus.Pages
{
    partial class salProfile : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)(target);
                    #line 5 "..\..\..\Pages\salProfile.xaml"
                    ((global::Windows.UI.Xaml.Controls.Page)element1).Loaded += this.Page_Loaded;
                    #line default
                }
                break;
            case 2:
                {
                    this.userPic = (global::Windows.UI.Xaml.Shapes.Ellipse)(target);
                }
                break;
            case 3:
                {
                    this.NameBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 4:
                {
                    this.city = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 5:
                {
                    this.state = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 6:
                {
                    this.pin = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 7:
                {
                    this.CityBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 8:
                {
                    this.StateBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 9:
                {
                    this.PinBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 10:
                {
                    this.logoutBut = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 70 "..\..\..\Pages\salProfile.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.logoutBut).Click += this.logoutBut_Click;
                    #line default
                }
                break;
            case 11:
                {
                    this.proRing = (global::Windows.UI.Xaml.Controls.ProgressRing)(target);
                }
                break;
            case 12:
                {
                    this.moreButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 90 "..\..\..\Pages\salProfile.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.moreButton).Click += this.moreButton_Click;
                    #line default
                }
                break;
            case 13:
                {
                    this.commentButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 95 "..\..\..\Pages\salProfile.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.commentButton).Click += this.commentButton_Click;
                    #line default
                }
                break;
            case 14:
                {
                    this.searchButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 98 "..\..\..\Pages\salProfile.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.searchButton).Click += this.searchButton_Click;
                    #line default
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

