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
    public sealed partial class personReg : Page
    {
        CheckingType par;
        PersonInfo persona;        

        public personReg()
        {
            this.InitializeComponent();            
            persona = new PersonInfo();
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
        
        private async void goNext_Click(object sender, RoutedEventArgs e)
        {
            goNext.Content = "";
            welcomeRing.Visibility = Visibility.Visible;
            welcomeRing.IsActive = true;

            persona.FirstName = fname.Text;
            persona.LastName = lname.Text;
            persona.Email = emailBox.Text;
            persona.OTP = RandomNumber.RandomDigits(Constants.OTPLIM);

            try
            {
                var response = await CommonCall.RegisterYourselfAsync(par, null, persona);                
                if (response.response.Equals("Success"))
                {
                    ToastyTaost.ShowToastNotification("OTP", "OTP sent as E-Mail");

                    FirstName.Visibility = Visibility.Collapsed;
                    LastName.Visibility = Visibility.Collapsed;
                    Emailia.Visibility = Visibility.Collapsed;
                    fname.Visibility = Visibility.Collapsed;
                    lname.Visibility = Visibility.Collapsed;
                    emailBox.Visibility = Visibility.Collapsed;

                    goBack.Visibility = Visibility.Collapsed;
                    goNext.Visibility = Visibility.Collapsed;

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
                goNext.Content = "Submit";
            }
        }

        private async void otpbutton_Click(object sender, RoutedEventArgs e)
        {            
            if (otpdata.Text.Equals(persona.OTP))
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

                    FirstName.Visibility = Visibility.Visible;
                    LastName.Visibility = Visibility.Visible;
                    Emailia.Visibility = Visibility.Visible;
                    fname.Visibility = Visibility.Visible;
                    lname.Visibility = Visibility.Visible;
                    emailBox.Visibility = Visibility.Visible;

                    goBack.Visibility = Visibility.Visible;
                    goNext.Visibility = Visibility.Visible;

                    otpBLOCK.Visibility = Visibility.Collapsed;
                    otpbutton.Visibility = Visibility.Collapsed;
                    otpdata.Visibility = Visibility.Collapsed;

                    welcomeRing.Visibility = Visibility.Collapsed;
                    welcomeRing.IsActive = false;
                }
            }
        }
    }

    sealed class PersonInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string OTP { get; set; }
    }
}
