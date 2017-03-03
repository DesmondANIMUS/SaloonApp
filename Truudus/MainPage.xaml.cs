using Truudus.Pages;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Truudus
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        LoginInfo log;
        int x1, x2;

        public MainPage()
        {
            this.InitializeComponent();
            log = new LoginInfo();

            ManipulationMode = ManipulationModes.TranslateRailsX | ManipulationModes.TranslateRailsY;
            ManipulationStarted += (s, e) => x1 = (int)e.Position.X;
            ManipulationCompleted += (s, e) =>
            {
                x2 = (int)e.Position.X;
                if (x1 > x2)
                    swypeActionResult(false);
                else
                    swypeActionResult(true);
            };
        }

        private void personButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(logorReg), "EndUser");
        }

        private void saloonButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(logorReg), "Saloon");
        }

        private void swypeActionResult(bool leftRight)
        {
            switch (leftRight)
            {
                case false:
                    Frame.Navigate(typeof(logorReg), "EndUser");
                    break;
                case true:
                    Frame.Navigate(typeof(logorReg), "Saloon");
                    break;
            }
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
                        Frame.Navigate(typeof(userProfile), log);
                        break;
                    case "Saloon":
                        Frame.Navigate(typeof(salProfile), log);
                        logorReg.intent = true;
                        break;
                }
            }
        }
    }
}
