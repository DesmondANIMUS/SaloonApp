using System;
using Truudus.Managers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Truudus.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class updatePassword : Page
    {
        LoginInfo log;

        public updatePassword()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            log = (LoginInfo)e.Parameter;
        }

        private async void resetButton_Click(object sender, RoutedEventArgs e)
        {
            resetRing.IsActive = true;
            resetRing.Visibility = Visibility.Visible;
            resetButton.Visibility = Visibility.Collapsed;

            try
            {
                #region For Encryption            
                var privateKey = Constants.PRIVATEKEY;
                var publicKey = Constants.PUBLICKEY;

                var oldPa = EncryptionLayer1.CreateHash(privateKey, publicKey, oldPassBox.Password);
                var newPa = EncryptionLayer1.CreateHash(privateKey, publicKey, newPassBox.Password);
                #endregion

                var response = await UpdateCommonPassword.UpdateYourPasswordAsync(oldPa, newPa, log);
                if (response.response.Equals("Success"))
                {
                    ToastyTaost.ShowToastNotification("Success", "Password udpate success");
                    if (log.TypeLogin.Equals("EndUser"))
                        Frame.Navigate(typeof(userProfile), log);
                    else
                        Frame.Navigate(typeof(salProfile), log);
                }
                else
                {
                    ToastyTaost.ShowToastNotification("Failed :(", "Password udpate failed :(");
                    oldPassBox.Password = "";
                    newPassBox.Password = "";
                }
            }

            catch (Exception) { }

            finally
            {
                resetRing.IsActive = false;
                resetRing.Visibility = Visibility.Collapsed;
                resetButton.Visibility = Visibility.Visible;
            }
        }
    }
}
