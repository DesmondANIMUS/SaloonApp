using Truudus.Models;
using Truudus.Pages;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Truudus.Managers
{
    class CommonGettingUsersCall
    {
        private static string URI = Constants.GETUSER;

        internal static async Task<CommonUserResponse> GetUserInfo(LoginInfo log)
        {
            var http = new HttpClient();
            var parameters = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("user", log.Username),
                                                               new KeyValuePair<string, string>("type", log.TypeLogin)});

            var response = await http.PostAsync(URI, parameters);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(CommonUserResponse));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));

            var datax = (CommonUserResponse)serializer.ReadObject(ms);

            return datax;
        }
    }
}
