using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using WeatherApp.ViewModels;
using System.Threading.Tasks;
using WeatherApp.Threading;

namespace WeatherApp.ViewModels
{
    public class PropertyBinding : INotifyPropertyChanged
    {
        protected void OnPropertyChanged([CallerMemberName] string property = null)
        {
            MainThread.InvokeOnMainThreadIfNeeded(() =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
            });
        }

        public void UpdateBindings(string propertyName)
        {
            OnPropertyChanged(propertyName);
        }

        public void SetValue<T>(ref T property, ref T value, [CallerMemberName] string propertyName = null, Action method = null, bool checkEquality = true)
        {
            if (checkEquality)
            {
                if (EqualityComparer<T>.Default.Equals(property, value))
                {
                    return;
                }
            }
                
            property = value;
            if (method != null)
            {
                method();
            }

            OnPropertyChanged(propertyName);
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
