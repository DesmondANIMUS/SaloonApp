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

        private async void goNext_Click(object sender, RoutedEventArgs e)
        {            
            parameters.SaloonName = sname.Text;
            parameters.Speciality = speciality.Text;
            parameters.ShortDesc = shortDescbox.Text;

            try
            {
                var response = await CommonCall.RegisterYourselfAsync(par, parameters);
                var dialog = new MessageDialog(response.response.ToString());
                await dialog.ShowAsync();

                Frame.Navigate(typeof(logorReg), par.TypeUser);
            }

            catch (Exception) { }
        }
    }

    sealed class AllSaloonInfo
    {
        public string SaloonName { get; set; }        
        public string Speciality { get; set; }
        public string ShortDesc { get; set; }
    }
}
