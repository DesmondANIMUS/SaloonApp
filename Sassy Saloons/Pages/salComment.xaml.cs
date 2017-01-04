using Sassy_Saloons.Managers;
using Sassy_Saloons.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Sassy_Saloons.Pages
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
            comments = new ObservableCollection<Response>();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {            
            log = salProfile.log;
            com.Salonname = salProfile.data.SalonName;
            com.Username = log.Username;
            com.Upins = makeComment.upins;
            // stars will come from this page, comments from makeComment                        
            
            try
            {
                await CommentCall.GetCommentsAsync(com, comments);
            }            

            catch (Exception) { }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            com.Comment = (string)e.Parameter;
        }

        private void moreButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(salProfile), log);                    
        }
    }

    sealed class CommentInfo
    {
        public string Username { get; set; }
        public string Salonname { get; set; }
        public string Comment { get; set; }
        public string Star { get; set; }
        public int Upins { get; set; }
    }
}
