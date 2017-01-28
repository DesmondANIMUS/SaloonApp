using Truudus.Managers;
using Truudus.Models;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Popups;
using System.Net.NetworkInformation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Truudus.Pages
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
            searchRing.Visibility = Visibility.Visible;
            searchRing.IsActive = true;

            try
            {
                var isConnected = NetworkInterface.GetIsNetworkAvailable();
                
                if (isConnected == true)
                {
                    var location = await LocationManagerCall.GetPositionAsync();

                    if (location == null)
                        await SearchCall.GetSaloonsAsync(searchedSaloons);
                    else
                    {
                        string city = null;
                        city = await GoogleRevGeoCall.GetCityNameAsync(location.Coordinate.Point.Position.Latitude.ToString(),
                                                                       location.Coordinate.Point.Position.Longitude.ToString());


                        await SearchCall.GetSaloonByLocation(searchedSaloons, city);

                        if (searchedSaloons.Count <= 0)
                        {
                            var dialog = new MessageDialog("Search results inconclusive :(");
                            await dialog.ShowAsync();
                            await SearchCall.GetSaloonsAsync(searchedSaloons);
                        }
                    }
                } 
                
                else
                {
                    var dialog = new MessageDialog("No internet, no gps :(");
                    await dialog.ShowAsync();
                    await SearchCall.GetSaloonsAsync(searchedSaloons);
                }               
            }

            catch (Exception) { }

            finally
            {
                searchRing.Visibility = Visibility.Collapsed;
                searchRing.IsActive = false;
            }
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
