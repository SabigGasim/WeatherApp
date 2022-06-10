using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPlacePage : ContentPage
    {
        public SearchPlacePage()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty FocusOriginCommandProperty =
           BindableProperty.Create(nameof(FocusOriginCommand), typeof(ICommand), typeof(SearchPlacePage), null, BindingMode.TwoWay);

        public ICommand FocusOriginCommand
        {
            get { return (ICommand)GetValue(FocusOriginCommandProperty); }
            set { SetValue(FocusOriginCommandProperty, value); }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (BindingContext != null)
            {
                FocusOriginCommand = new Command(OnOriginFocus);
            }
        }

        void OnOriginFocus()
        {
            destinationEntry.Focus();
        }
    }
}