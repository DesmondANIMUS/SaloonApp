using Truudus.Pages;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Truudus
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        LoginInfo log;

        public MainPage()
        {
            this.InitializeComponent();
            log = new LoginInfo();
        }

        private void personButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Pages.logorReg), "EndUser");
        }

        private void saloonButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Pages.logorReg), "Saloon");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            if ((localSettings.Values["user"] != null) && (localSettings.Values["type"] != null))
            {
                log.Username = (string)localSettings.Values["user"];
                log.TypeLogin = (string)localSettings.Values["type"];

                switch (log.TypeLogin)
                {
                    case "EndUser":
             //           Frame.Navigate(typeof(userProfile), log);
                        break;
                    case "Saloon":
             //           Frame.Navigate(typeof(salProfile), log);
                        break;
                }
            }
        }
    }
}
