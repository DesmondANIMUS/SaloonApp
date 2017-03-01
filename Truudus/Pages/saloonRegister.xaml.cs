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
    public sealed partial class saloonRegister : Page
    {
        AllSaloonInfo parameters;
        CheckingType par;             

        public saloonRegister()
        {
            this.InitializeComponent();
            parameters = new AllSaloonInfo();      
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            par = (CheckingType)e.Parameter;            
        }

        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(commonRegister), par.TypeUser);
        }
        
        private async void otpbutton_Click(object sender, RoutedEventArgs e)
        {
            if (otpdata.Text.Equals(parameters.OTP))
            {
                welcomeRing.Visibility = Visibility.Visible;
                welcomeRing.IsActive = true;

                try
                {
                    var response = await OTPCall.VerifyYourOTPAsync(par);
                    if (response.response.Equals("Success"))
                        Frame.Navigate(typeof(logorReg), par.TypeUser);
                }

                catch (Exception) { }
                finally
                {
                    nameingblock.Visibility = Visibility.Visible;
                    aboutBlock.Visibility = Visibility.Visible;
                    emaila.Visibility = Visibility.Visible;
                    shortDescbox.Visibility = Visibility.Visible;
                    email.Visibility = Visibility.Visible;
                    sname.Visibility = Visibility.Visible;

                    goBack.Visibility = Visibility.Visible;
                    submit.Visibility = Visibility.Visible;

                    otpBLOCK.Visibility = Visibility.Collapsed;
                    otpbutton.Visibility = Visibility.Collapsed;
                    otpdata.Visibility = Visibility.Collapsed;

                    welcomeRing.Visibility = Visibility.Collapsed;
                    welcomeRing.IsActive = false;
                }
            }
        }

        private async void submit_Click(object sender, RoutedEventArgs e)
        {
            submit.Content = "";
            welcomeRing.Visibility = Visibility.Visible;
            welcomeRing.IsActive = true;            

            parameters.SaloonName = sname.Text;
            parameters.Email = email.Text;
            parameters.ShortDesc = shortDescbox.Text;
            parameters.OTP = RandomNumber.RandomDigits(Constants.OTPLIM);

            try
            {
                var response = await CommonCall.RegisterYourselfAsync(par, parameters);
                if (response.response.Equals("Success"))
                {
                    ToastyTaost.ShowToastNotification("OTP", "OTP sent as E-Mail");

                    nameingblock.Visibility = Visibility.Collapsed;
                    aboutBlock.Visibility = Visibility.Collapsed;
                    emaila.Visibility = Visibility.Collapsed;
                    shortDescbox.Visibility = Visibility.Collapsed;
                    email.Visibility = Visibility.Collapsed;
                    sname.Visibility = Visibility.Collapsed;

                    goBack.Visibility = Visibility.Collapsed;
                    submit.Visibility = Visibility.Collapsed;

                    otpBLOCK.Visibility = Visibility.Visible;
                    otpbutton.Visibility = Visibility.Visible;
                    otpdata.Visibility = Visibility.Visible;
                }
                else { ToastyTaost.ShowToastNotification("Failed :(", response.response); }                
            }

            catch (Exception) { }

            finally
            {
                welcomeRing.Visibility = Visibility.Collapsed;
                welcomeRing.IsActive = false;
                submit.Content = "Submit";
            }
        }
    }

    sealed class AllSaloonInfo
    {
        public string SaloonName { get; set; }        
        public string Email { get; set; }
        public string ShortDesc { get; set; }
        public string OTP { get; set; }
    }
}
