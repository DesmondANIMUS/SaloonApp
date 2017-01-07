using Truudus.Managers;
using Truudus.Models;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Truudus.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class salProfile : Page
    {        
        internal static LoginInfo log;
        internal static CommonUserResponse data;
                
        public salProfile()
        {
            this.InitializeComponent();
            data = new CommonUserResponse();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (logorReg.intent == false)
                logoutBut.Visibility = Visibility.Collapsed;

            try
            {                
                data = await CommonGettingUsersCall.GetUserInfo(log);

                NameBlock.Text = data.SalonName;
                CityBlock.Text = data.City;
                StateBlock.Text = data.State;
                PinBlock.Text = data.PinCode;
            }

            catch (Exception) { }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            log = (LoginInfo)e.Parameter;
        }

        private void moreButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(salMore), data);
        }

        private void commentButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(salComment));
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(salSearch));
        }

        private void logoutBut_Click(object sender, RoutedEventArgs e)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            localSettings.Values["user"] = null;
            localSettings.Values["type"] = null;

            Frame.Navigate(typeof(MainPage));
        }
    }
}
