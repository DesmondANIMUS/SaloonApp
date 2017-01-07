using System;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI.Popups;

namespace Truudus.Managers
{
    public class LocationManagerCall
    {
        public async static Task<Geoposition> GetPositionAsync()
        {
            var accessTokenStatus = await Geolocator.RequestAccessAsync();
            Geoposition position = null;

            switch (accessTokenStatus)
            {
                case GeolocationAccessStatus.Denied:
                case GeolocationAccessStatus.Unspecified:
                    var dialog = new MessageDialog("Could not access location :(");
                    await dialog.ShowAsync();
                    position = null;
                    break;
                case GeolocationAccessStatus.Allowed:
                    var geolocator = new Geolocator { DesiredAccuracyInMeters = 0 };
                    // 0 means give me the best you can. 
                    position = await geolocator.GetGeopositionAsync();
                    break;
            }

            return position;
        }
    }
}
