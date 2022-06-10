using Android.Content;
using Android.Locations;
using WeatherApp.Droid.Services;
using WeatherApp.Services;
using Android.Provider;
using Android.App;
using Android.Widget;

[assembly: Xamarin.Forms.Dependency(typeof(GpsDependencyService))]
namespace WeatherApp.Droid.Services
{
    public class GpsDependencyService : IGpsDependencyService
    {
        public bool IsGpsEnabled()
        {
            LocationManager locationManager = (LocationManager)Application.Context.GetSystemService(Context.LocationService);
            return locationManager.IsProviderEnabled(LocationManager.GpsProvider);
        }

        public void OpenSettings()
        {
            Intent intent = new Intent(Settings.ActionLocat‌​ionSourceSettings);
            intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);

            try
            {
                Application.Context.StartActivity(intent);
            }
            catch (ActivityNotFoundException activityNotFoundException)
            {
                System.Diagnostics.Debug.WriteLine(activityNotFoundException.Message);
                Toast.MakeText(Application.Context, "Your device doesn't support GPS", ToastLength.Short).Show();
            }

        }
    }
}
