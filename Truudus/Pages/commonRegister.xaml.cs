using System.Text.RegularExpressions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Truudus.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class commonRegister : Page
    {

        CheckingType parameters;
        string privateKey, publicKey;
        string state;

        public commonRegister()
        {
            this.InitializeComponent();
            parameters = new CheckingType();
            fillStateCombo();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            parameters.TypeUser = e.Parameter.ToString();
        }

        private void goNext_Click(object sender, RoutedEventArgs e)
        {                       
            parameters.Username = usernameBox.Text;
            parameters.City = Formatting(cityBox.Text);

            parameters.State = state;
            parameters.Pin = pinBox.Text;

            #region For Encryption            
            privateKey = Constants.PRIVATEKEY;
            publicKey = Constants.PUBLICKEY;
            # endregion

            parameters.Password = EncryptionLayer1.CreateHash(privateKey, publicKey, passBox.Password);

            if (!(usernameBox.Text.Equals(null) || usernameBox.Equals("") || usernameBox.Text.Equals(" ")))
            {
                if (!(passBox.Password.Equals(null) || passBox.Password.Equals("") || passBox.Password.Equals(" ")))
                {
                    if (parameters.TypeUser.Equals("EndUser"))
                        Frame.Navigate(typeof(personReg), parameters);
                    else
                        Frame.Navigate(typeof(saloonRegister), parameters);
                }
            }
        }

        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(logorReg), parameters.TypeUser);
        }

        private void stateBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            state = stateBox.SelectedValue as string;
        }

        #region states
        private void fillStateCombo()
        {
            stateBox.Items.Add("Andaman and Nicobar Islands");
            stateBox.Items.Add("Andhra Pradesh");
            stateBox.Items.Add("Arunachal Pradesh");
            stateBox.Items.Add("Assam");
            stateBox.Items.Add("Bihar");
            stateBox.Items.Add("Chandigarh");
            stateBox.Items.Add("Chhattisgarh");
            stateBox.Items.Add("Dadra and Nagar Haveli");
            stateBox.Items.Add("Daman and Diu");
            stateBox.Items.Add("Delhi");
            stateBox.Items.Add("Goa");
            stateBox.Items.Add("Gujarat");
            stateBox.Items.Add("Haryana");
            stateBox.Items.Add("Himachal Pradesh");
            stateBox.Items.Add("Jammu and Kashmir");
            stateBox.Items.Add("Jharkhand");
            stateBox.Items.Add("Karnataka");
            stateBox.Items.Add("Kerala");
            stateBox.Items.Add("Lakshadweep");
            stateBox.Items.Add("Madhya Pradesh");
            stateBox.Items.Add("Maharashtra");
            stateBox.Items.Add("Manipur");
            stateBox.Items.Add("Meghalaya");
            stateBox.Items.Add("Mizoram");
            stateBox.Items.Add("Nagaland");
            stateBox.Items.Add("Odisha");
            stateBox.Items.Add("Puducherry");
            stateBox.Items.Add("Punjab");
            stateBox.Items.Add("Rajasthan");
            stateBox.Items.Add("Sikkim");
            stateBox.Items.Add("Tamil Nadu");
            stateBox.Items.Add("Telangana");
            stateBox.Items.Add("Tripura");
            stateBox.Items.Add("Uttar Pradesh");
            stateBox.Items.Add("Uttarakhand");
            stateBox.Items.Add("West Bengal");
        }
        #endregion

        private string Formatting(string s)
        {            
            var rx = new Regex(@"(?<=\w)\w");
            var newString = rx.Replace(s, new MatchEvaluator(m => m.Value.ToLowerInvariant()));

            return newString;
        }
    }

    internal class CheckingType
    {
        public string TypeUser { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pin { get; set; }
    }
}