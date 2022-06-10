using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WeatherApp.CustomViews
{
    internal class PickerCell : ViewCell
    {
        private Label _label { get; set; }
        private Xamarin.Forms.View _picker { get; set; }
        private StackLayout _layout { get; set; }
        private double _fontSize;

        internal string Label
        {
            get
            {
                return _label.Text;
            }
            set
            {
                _label.Text = value;
            }
        }

        internal double FontSize
        {
            get => _fontSize;
            set =>_fontSize = value;
        }

        internal Xamarin.Forms.View Picker
        {
            set
            {
                //Remove picker if it exists
                if (_picker != null)
                {
                    _layout.Children.Remove(_picker);
                }

                //Set its value
                _picker = value;
                //Add to layout
                _layout.Children.Add(_picker);

            }
        }

        internal PickerCell()
        {
            _label = new Label()
            {
                VerticalOptions = LayoutOptions.Center,
                FontSize = _fontSize
            };
            _layout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Padding = new Thickness(15, 5),
                Children =
                {
                    _label
                }
            };

            this.View = _layout;

        }

    }
}
