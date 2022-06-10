using WeatherApp.Services;
using CoreLocation;
using Foundation;
using WeatherApp.iOS.Services;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(GpsDependencyService))]
namespace WeatherApp.iOS.Services
{
    public class GpsDependencyService : IGpsDependencyService
    {
        public bool IsGpsEnabled()
        {
            if (CLLocationManager.Status == CLAuthorizationStatus.Denied)
            {
                return false;
            }

            return true;
        }

        public void OpenSettings()
        {
            var wifi_Url = new NSUrl("prefs:root=WIFI");

            if (UIApplication.SharedApplication.CanOpenUrl(wifi_Url))
            {   
                //> Pre iOS 10
                UIApplication.SharedApplication.OpenUrl(wifi_Url);
                return;
            }

            //> iOS 10
            UIApplication.SharedApplication.OpenUrl(new NSUrl("App-Prefs:root=WIFI"));
        }
    }
}
