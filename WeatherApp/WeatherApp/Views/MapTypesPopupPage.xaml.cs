using WeatherApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp.Views;
[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class MapTypesPopupPage : Rg.Plugins.Popup.Pages.PopupPage
{
    public MapTypesPopupPage()
    {
        InitializeComponent();

        BindingContext = DependencyService.Get<MapPageVM>();
    }

    private async void Button_Clicked(object sender, System.EventArgs e)
    {
        await this.Navigation.LockPopPopupAsync();
    }
}
