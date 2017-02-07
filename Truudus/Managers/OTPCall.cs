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
    class OTPCall
    {
        private const string URI = Constants.VERIF;
        private static FormUrlEncodedContent parameters;
        internal static async Task<CommonResponse> VerifyYourOTPAsync(CheckingType log, string otp = null, LoginInfo loginInfo = null)
        {
            var http = new HttpClient();
            if (otp == null && loginInfo == null)
            {
                parameters = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("type", log.TypeUser),
                                                               new KeyValuePair<string, string>("user", log.Username)});
            }

            else
            {
                parameters = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("type", loginInfo.TypeLogin),
                                                               new KeyValuePair<string, string>("otp", otp),
                                                               new KeyValuePair<string, string>("user", loginInfo.Username)});
            }

            var response = await http.PostAsync(URI, parameters);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(CommonResponse));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));

            var datax = (CommonResponse)serializer.ReadObject(ms);

            return datax;
        }
    }
}
