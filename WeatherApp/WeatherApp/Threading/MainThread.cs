using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Threading
{
    public static class MainThread
    {
        public static T InvokeOnMainThreadIfNeeded<T>(Func<T> func)
        {
            var dispatcher = App.Current.Dispatcher;
            if (dispatcher.IsInvokeRequired)
            {
                T returnValue = default;
                dispatcher.BeginInvokeOnMainThread(() =>
                {
                    returnValue = func();
                });

                return returnValue;
            }

            return func();
        }

        public static T InvokeOnMainThread<T>(Func<T> func)
        {
            T returnValue = default;

            App.Current.Dispatcher.BeginInvokeOnMainThread(() =>
            {
                returnValue = func();
            });

            return returnValue;
        }

        public static void InvokeOnMainThreadIfNeeded(Action action)
        {
            var dispatcher = App.Current.Dispatcher;
            if (!dispatcher.IsInvokeRequired)
            {
                action();
                return;
            }
            
            dispatcher.BeginInvokeOnMainThread(() =>
            {
                action();
            });
        }
    }
}
