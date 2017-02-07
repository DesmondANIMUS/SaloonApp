using System.Text;
using System.Threading.Tasks;
using Truudus.Models;
using System.Net.Http;
using System.IO;
using System.Runtime.Serialization.Json;
using Truudus.Pages;
using System.Collections.Generic;

namespace Truudus.Managers
{
    class CommonCall
    {
        private static string URI;        
        static FormUrlEncodedContent parameters;        
        
        public static async Task<CommonResponse> RegisterYourselfAsync(CheckingType check = null, AllSaloonInfo saloonInfo = null, 
            PersonInfo perInfo = null, LoginInfo log = null)
        {
            var http = new HttpClient();            

            if (saloonInfo != null)
            {
                parameters = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("user", check.Username),
                                                               new KeyValuePair<string, string>("city", check.City),
                                                               new KeyValuePair<string, string>("state", check.State),
                                                               new KeyValuePair<string, string>("pass", check.Password),
                                                               new KeyValuePair<string, string>("pin", check.Pin),

                                                               new KeyValuePair<string, string>("sname", saloonInfo.SaloonName),
                                                               new KeyValuePair<string, string>("desc", saloonInfo.ShortDesc),
                                                               new KeyValuePair<string, string>("otp", saloonInfo.OTP),
                                                               new KeyValuePair<string, string>("email", saloonInfo.Email)});
                URI = Constants.SALREG;
            }
            
            else if (perInfo != null)
            {

                parameters = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("user", check.Username),
                                                               new KeyValuePair<string, string>("city", check.City),
                                                               new KeyValuePair<string, string>("state", check.State),
                                                               new KeyValuePair<string, string>("pass", check.Password),
                                                               new KeyValuePair<string, string>("pin", check.Pin),

                                                               new KeyValuePair<string, string>("fname", perInfo.FirstName),
                                                               new KeyValuePair<string, string>("lname", perInfo.LastName),
                                                               new KeyValuePair<string, string>("otp", perInfo.OTP),
                                                               new KeyValuePair<string, string>("email", perInfo.Email)});
                URI = Constants.REGUSE;
            }

            else 
            {
                parameters = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("user", log.Username),
                                                               new KeyValuePair<string, string> ("pass", log.Password),
                                                               new KeyValuePair<string, string>("type", log.TypeLogin)});
                URI = Constants.LOGIN;
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
