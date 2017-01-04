using Sassy_Saloons.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Sassy_Saloons.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class salMore : Page
    {        
        CommonUserResponse extraData;
        LoginInfo logData;

        public salMore()
        {
            this.InitializeComponent();            
        }
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            extraData = (CommonUserResponse)e.Parameter;            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {            
            logData = salProfile.log;

            if (logorReg.intent == true)
                editButton.Visibility = Visibility.Visible;
            else
                editButton.Visibility = Visibility.Collapsed;

            speicalBlock.Text = extraData.Speciality;
            descBlock.Text = extraData.ShortDesc;
        }

        private void goBack_Click(object sender, RoutedEventArgs e)
        {                        
            Frame.Navigate(typeof(salProfile), logData);
        }
    }
}
