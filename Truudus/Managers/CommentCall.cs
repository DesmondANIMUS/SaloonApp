using Truudus.Models;
using Truudus.Pages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Truudus.Managers
{
    class CommentCall
    {
        private static string URI;
        static FormUrlEncodedContent parameters;

        private static async Task<CommentsResponse> GetWrapperAsync(CommentInfo com)
        {
            var http = new HttpClient();

            switch(com.Upins)
            {
                case false:
                    parameters = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("sname", com.Salonname) });
                    URI = Constants.GETCOM;
                    break;
                case true:
                    parameters = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("sname", com.Salonname),
                                                                   new KeyValuePair<string, string>("user", com.Username),
                                                                   new KeyValuePair<string, string>("comment", com.Comment),
                                                                   new KeyValuePair<string, string>("star", com.Star) });
                    URI = Constants.INCOM;
                    break;
            }

            var response = await http.PostAsync(URI, parameters);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(CommentsResponse));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));

            var datax = (CommentsResponse)serializer.ReadObject(ms);

            return datax;
        }   
        
        internal static async Task GetCommentsAsync(CommentInfo com, ObservableCollection<Response> allcom)
        {
            allcom.Clear();
            var datawrapper = await GetWrapperAsync(com);
            var getdata = datawrapper.response;            

            foreach (var d in getdata)
                allcom.Add(d);
        }
    }
}
