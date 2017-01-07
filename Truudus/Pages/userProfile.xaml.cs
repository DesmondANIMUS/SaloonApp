using Truudus.Managers;
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
    public sealed partial class userProfile : Page
    {        
        LoginInfo log;

        public userProfile()
        {
            this.InitializeComponent();            
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var data = await CommonGettingUsersCall.GetUserInfo(log);

                NameBlock.Text = data.FirstName + " " + data.LastName; 
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

        private void SaloonSearchBut_Click(object sender, RoutedEventArgs e)
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
