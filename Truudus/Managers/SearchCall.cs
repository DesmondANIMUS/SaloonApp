using Truudus.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Truudus.Managers
{
    class SearchCall
    {
        private static string URI = Constants.GETSAL;

        private static async Task<SaloonResponse> GetSaloonWrapperAsync()
        {
            var http = new HttpClient();

            var response = await http.GetAsync(URI);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(SaloonResponse));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));

            var datax = (SaloonResponse)serializer.ReadObject(ms);

            return datax;
        }

        internal static async Task GetSaloonsAsync(ObservableCollection<SearchResult> searchResults)
        {
            searchResults.Clear();
            var datawrapper = await GetSaloonWrapperAsync();
            var actual = datawrapper.SearchResults;
            foreach (var a in actual)
                searchResults.Add(a);
        }

        internal static async Task GetSaloonByLocation(ObservableCollection<SearchResult> searchResults, string city)
        {
            searchResults.Clear();
            var datawrapper = await GetSaloonWrapperAsync();
            var actual = datawrapper.SearchResults;

            foreach (var a in actual)
                if (a.City.Equals(city))
                    searchResults.Add(a);
        }
    }
}
