using System;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Truudus.Models;

namespace Truudus.Managers
{
    class GoogleRevGeoCall
    {
        private static string URI = Constants.GEOCO;

        private static async Task<GoogleRevGeoResponse> GetCityWrapperAsync(string lat, string lng)
        {
            var http = new HttpClient();

            URI = String.Format(URI, lat, lng);

            var response = await http.GetAsync(URI);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(GoogleRevGeoResponse));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));

            var datax = (GoogleRevGeoResponse)serializer.ReadObject(ms);

            return datax;
        }

        internal static async Task<string> GetCityNameAsync(string lat, string lng)
        {
            var datawrapper = await GetCityWrapperAsync(lat, lng);
            var smolwrapper = datawrapper.results;

            string city = null;

            for (int i = 0; i < smolwrapper.Count; i++)
                city = smolwrapper[i].formatted_address;

            string[] data = city.Split(',');

            city = data[5];

            city = city.Remove(0, 1);
                        
            return city;
        }
    }
}
