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
            proRing.Visibility = Visibility.Visible;
            proRing.IsActive = true;

            NameBlock.Visibility = Visibility.Collapsed;
            city.Visibility = Visibility.Collapsed;
            pin.Visibility = Visibility.Collapsed;
            state.Visibility = Visibility.Collapsed;
            userPic.Visibility = Visibility.Collapsed;
            UserProfileBut.Visibility = Visibility.Collapsed;
            SaloonSearchBut.Visibility = Visibility.Collapsed;            

            try
            {
                var data = await CommonGettingUsersCall.GetUserInfo(log);

                NameBlock.Text = data.FirstName + " " + data.LastName; 
                CityBlock.Text = data.City;
                StateBlock.Text = data.State;
                PinBlock.Text = data.PinCode;
            }

            catch (Exception) { }

            finally
            {
                proRing.Visibility = Visibility.Collapsed;
                proRing.IsActive = false;

                NameBlock.Visibility = Visibility.Visible;
                city.Visibility = Visibility.Visible;
                state.Visibility = Visibility.Visible;
                pin.Visibility = Visibility.Visible;
                userPic.Visibility = Visibility.Visible;
                UserProfileBut.Visibility = Visibility.Visible;
                SaloonSearchBut.Visibility = Visibility.Visible;
            }
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
