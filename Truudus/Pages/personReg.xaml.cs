using Truudus.Managers;
using System;
using Windows.UI.Popups;
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

            try
            {
                var response = await CommonCall.RegisterYourselfAsync(par, null, persona);                
                if (response.response.Equals("Success"))
                    Frame.Navigate(typeof(logorReg), par.TypeUser);                
            }

            catch (Exception) { }

            finally
            {
                welcomeRing.Visibility = Visibility.Collapsed;
                welcomeRing.IsActive = false;
                goNext.Content = "Welcome";
            }
        }
    }

    sealed class PersonInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
