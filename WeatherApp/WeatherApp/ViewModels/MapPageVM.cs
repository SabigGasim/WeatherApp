using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace WeatherApp.ViewModels
{
    public class MapPageVM : PropertyBinding
    {
        public MapPageVM()
        {
            ChangeMapTypeCommand = new Command(CommandParameter =>
            {
                MapType = (MapType)CommandParameter;
            });
        }

        public MapType MapType
        {
            get => mapType;
            set => SetValue(ref mapType, ref value);
        }

        public ICommand ChangeMapTypeCommand { private set; get; }

        private MapType mapType;
    }
    
}
