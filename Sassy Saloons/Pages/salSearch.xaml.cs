using Sassy_Saloons.Managers;
using Sassy_Saloons.Models;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Sassy_Saloons.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class salSearch : Page
    {
        LoginInfo log;
        ObservableCollection<SearchResult> searchedSaloons;

        public salSearch()
        {
            this.InitializeComponent();
            log = new LoginInfo();
            searchedSaloons = new ObservableCollection<SearchResult>();            
        }
        
        private void user_Click(object sender, RoutedEventArgs e)
        {            
            var loginData = Windows.Storage.ApplicationData.Current.LocalSettings;

            log.Username = (string)loginData.Values["user"];
            log.TypeLogin = (string)loginData.Values["type"];

            if (log.TypeLogin.Equals("EndUser"))
                Frame.Navigate(typeof(userProfile), log);
            else
            {
                logorReg.intent = true;
                Frame.Navigate(typeof(salProfile), log);                
            } 
        }        

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await SearchCall.GetSaloonsAsync(searchedSaloons);
            }

            catch (Exception) { }
        }

        private void saloonsSearchedList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            log.Username = gimmeProfile.Text;
            log.TypeLogin = "Saloon";
            logorReg.intent = false;            

            Frame.Navigate(typeof(salProfile), log);
        }
    }
}
