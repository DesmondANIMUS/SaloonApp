using Truudus.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Truudus.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class salMore : Page
    {        
        CommonUserResponse extraData;
        LoginInfo logData;

        public salMore()
        {
            this.InitializeComponent();            
        }
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            extraData = (CommonUserResponse)e.Parameter;            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {            
            logData = salProfile.log;

            if (logorReg.intent == true)
                editButton.Visibility = Visibility.Visible;
            else
                editButton.Visibility = Visibility.Collapsed;

            speicalBlock.Text = extraData.Email;
            descBlock.Text = extraData.ShortDesc;
        }

        private void goBack_Click(object sender, RoutedEventArgs e)
        {                        
            Frame.Navigate(typeof(salProfile), logData);
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(updatePassword), logData);
        }
    }
}
