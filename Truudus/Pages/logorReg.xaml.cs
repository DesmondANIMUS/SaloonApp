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
    public sealed partial class logorReg : Page
    {
        LoginInfo log;
        CommonResponse response;
        string type, privateKey, publicKey, otp;
        static internal bool intent;

        public logorReg()
        {
            this.InitializeComponent();
            log = new LoginInfo();
            intent = false;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {            
            base.OnNavigatedTo(e);

            type = e.Parameter.ToString();            
        }

        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void validate_Click(object sender, RoutedEventArgs e)
        {
            if (otp.Equals(otpBOX.Text))
            {
                welcomeRing.Visibility = Visibility.Visible;
                welcomeRing.IsActive = true;
                login.Content = "";

                try
                {
                    var response = OTPCall.VerifyYourOTPAsync(null, null, log);
                }
                catch (Exception) { }
                finally
                {
                    welcomeRing.Visibility = Visibility.Collapsed;
                    welcomeRing.IsActive = false;
                    login.Content = "Sign In";
                    otpBOX.Visibility = Visibility.Collapsed;

                    login.Visibility = Visibility.Visible;
                    validate.Visibility = Visibility.Collapsed;
                    passBox.Visibility = Visibility.Visible;
                    username.IsEnabled = true;
                    pasOtp.Text = "Password";                    
                }
            }
            else
            {
                ToastyTaost.ShowToastNotification("Invalid OTP", "Please Try Again");                
            }
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {            
            Frame.Navigate(typeof(commonRegister), type);
        }

        private async void login_Click(object sender, RoutedEventArgs e)
        {
            log.Username = username.Text;
            log.TypeLogin = type;

            #region For Encryption            
            privateKey = Constants.PRIVATEKEY;
            publicKey = Constants.PUBLICKEY;
            #endregion

            log.Password = EncryptionLayer1.CreateHash(privateKey, publicKey, passBox.Password);

            try
            {
                welcomeRing.Visibility = Visibility.Visible;
                welcomeRing.IsActive = true;
                login.Content = "";

                response = await CommonCall.RegisterYourselfAsync(null, null, null, log);

                var keepTemp = Windows.Storage.ApplicationData.Current.LocalSettings;

                if (response.response.Equals("Success"))
                {
                    keepTemp.Values["user"] = log.Username;
                    keepTemp.Values["type"] = log.TypeLogin;

                    if (type.Equals("EndUser"))
                        Frame.Navigate(typeof(userProfile), log);
                    else
                    {
                        Frame.Navigate(typeof(salProfile), log);
                        intent = true;
                    }
                }

                else if (response.response.Equals("Invalid Account"))
                {
                    ToastyTaost.ShowToastNotification("Invalid Account", "Please Validate Your Account");

                    login.Visibility = Visibility.Collapsed;
                    validate.Visibility = Visibility.Visible;
                    username.IsEnabled = false;
                    passBox.Visibility = Visibility.Collapsed;
                    pasOtp.Text = "OTP";
                    otpBOX.Visibility = Visibility.Visible;

                    try
                    {
                        otp = RandomNumber.RandomDigits(Constants.OTPLIM);
                        var response = OTPCall.VerifyYourOTPAsync(null, otp, log);
                    }

                    catch (Exception) { }
                }

                else { ToastyTaost.ShowToastNotification("Try Again", "Wrong Username or Password"); }
            }

            catch (Exception) { }

            finally
            {
                welcomeRing.Visibility = Visibility.Collapsed;
                welcomeRing.IsActive = false;
                //login.Content = "";
                login.Content = "Sign In";
            }
        }
        
    }

    sealed class LoginInfo
    {
        public string Username  { get; set; }
        public string Password  { get; set; }
        public string TypeLogin { get; set; }
    }    
}
