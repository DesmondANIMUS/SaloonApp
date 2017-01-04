using Sassy_Saloons.Managers;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Sassy_Saloons.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class personReg : Page
    {
        CheckingType par;
        PersonInfo persona;
        string gender;

        public personReg()
        {
            this.InitializeComponent();
            FillCombo();
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

        void FillCombo()
        {

            sexComboSe.Items.Add("Female");
            sexComboSe.Items.Add("Gender Fluid");
            sexComboSe.Items.Add("Male");            
        }

        private void sexComboSe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            gender = sexComboSe.SelectedValue as string;
        }

        private async void goNext_Click(object sender, RoutedEventArgs e)
        {
            persona.FirstName = fname.Text;
            persona.LastName = lname.Text;
            persona.Sex = gender;

            try
            {
                var response = await CommonCall.RegisterYourselfAsync(par, null, persona);
                var dialog = new MessageDialog(response.response.ToString());
                await dialog.ShowAsync();

                Frame.Navigate(typeof(logorReg), par.TypeUser);
            }

            catch (Exception) { }
        }
    }

    sealed class PersonInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
    }
}
