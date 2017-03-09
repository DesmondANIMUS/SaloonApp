﻿using Truudus.Managers;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using System.Threading.Tasks;

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
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            log = (LoginInfo)e.Parameter;
        }

        private void HamBut_Click(object sender, RoutedEventArgs e)
        {
            splitHam.IsPaneOpen = !splitHam.IsPaneOpen;
        }

        private async void listHam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (searchSalListItem.IsSelected) { Frame.Navigate(typeof(salSearch)); }
            else if (logoutListItem.IsSelected) { await LogOutActionAsync(); }            
            else if (editPassItem.IsSelected) { Frame.Navigate(typeof(updatePassword), log); }
        }

        private async Task LogOutActionAsync()
        {

            var title = "Pending changes";
            var content = "Are you sure that you wish to logout?";

            var yesCommand = new UICommand("Yes", null);
            var noCommand = new UICommand("No", null);

            var dialog = new MessageDialog(content, title);
            dialog.Options = MessageDialogOptions.None;
            dialog.Commands.Add(yesCommand);

            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 0;

            if (noCommand != null)
            {
                dialog.Commands.Add(noCommand);
                dialog.CancelCommandIndex = (uint)dialog.Commands.Count - 1;
            }

            var command = await dialog.ShowAsync();

            if (command == yesCommand)
            {
                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

                localSettings.Values["user"] = null;
                localSettings.Values["type"] = null;

                Frame.Navigate(typeof(MainPage));
            }
            else
                ToastyTaost.ShowToastNotification("Cancelled", "User cancelled logout");
        }

        private void splitHam_PaneClosing(SplitView sender, SplitViewPaneClosingEventArgs args)
        {
            listHam.SelectedIndex = -1;
        }
    }
}
