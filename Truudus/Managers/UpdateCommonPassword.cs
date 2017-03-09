using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Truudus.Models;
using Truudus.Pages;

namespace Truudus.Managers
{
    class UpdateCommonPassword
    {
        private static string URI;
        public static async Task<CommonResponse> UpdateYourPasswordAsync(string old, string newty, LoginInfo log)
        {
            var http = new HttpClient();

            var parameters = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("user", log.Username),
                                                               new KeyValuePair<string, string> ("oldPass", old),
                                                               new KeyValuePair<string, string>("newPass", newty)});

            if (log.TypeLogin.Equals("EndUser"))
                URI = Constants.UPUSER;
            else
                URI = Constants.UPDSAL;

            var response = await http.PostAsync(URI, parameters);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(CommonResponse));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));

            var datax = (CommonResponse)serializer.ReadObject(ms);

            return datax;            
        }
    }
}
