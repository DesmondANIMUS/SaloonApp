using Truudus.Managers;
using Truudus.Models;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Truudus.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class salComment : Page
    {
        LoginInfo log;
        CommentInfo com;        
        ObservableCollection<Response> comments;

        public salComment()
        {
            this.InitializeComponent();
            com = new CommentInfo();
            log = new LoginInfo();
            comments = new ObservableCollection<Response>();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var keepTemp = Windows.Storage.ApplicationData.Current.LocalSettings;
            
            com.Username = (string)keepTemp.Values["user"];
            var salExist = (string)keepTemp.Values["type"];
            com.Salonname = salProfile.data.SalonName;            

            if (salExist.Equals("Saloon"))
                giveStar.Visibility = Visibility.Collapsed;

            try
            {                
                await CommentCall.GetCommentsAsync(com, comments);                
            }            

            catch (Exception) { }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            log = (LoginInfo)e.Parameter;
        }

        private void moreButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(salProfile), log);                    
        }

        private void giveStar_Click(object sender, RoutedEventArgs e)
        {
            moreButton.Visibility = Visibility.Collapsed;            
            starList.Visibility = Visibility.Collapsed;
            commentsList.Visibility = Visibility.Collapsed;
            giveStar.Visibility = Visibility.Collapsed;

            starField.Visibility = Visibility.Visible;
            commentBox.Visibility = Visibility.Visible;
            makeCommentButton.Visibility = Visibility.Visible;
            back.Visibility = Visibility.Visible;
        }

        private async void makeCommentButton_Click(object sender, RoutedEventArgs e)
        {
            com.Upins = true;
            com.Comment = commentBox.Text;
            com.Star = starField.Value.ToString();

            try
            {
                await CommentCall.GetCommentsAsync(com, comments);
            }

            catch (Exception) { }

            com.Upins = false;

            starField.Visibility = Visibility.Collapsed;
            commentBox.Visibility = Visibility.Collapsed;
            makeCommentButton.Visibility = Visibility.Collapsed;
            back.Visibility = Visibility.Collapsed;

            moreButton.Visibility = Visibility.Visible;            
            starList.Visibility = Visibility.Visible;
            commentsList.Visibility = Visibility.Visible;
            giveStar.Visibility = Visibility.Visible;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            starField.Visibility = Visibility.Collapsed;
            commentBox.Visibility = Visibility.Collapsed;
            makeCommentButton.Visibility = Visibility.Collapsed;
            back.Visibility = Visibility.Collapsed;

            moreButton.Visibility = Visibility.Visible;            
            starList.Visibility = Visibility.Visible;
            commentsList.Visibility = Visibility.Visible;
            giveStar.Visibility = Visibility.Visible;
        }
    }

    sealed class CommentInfo
    {
        public string Username { get; set; }
        public string Salonname { get; set; }
        public string Comment { get; set; }
        public string Star { get; set; }
        public bool Upins { get; set; }
    }
}
