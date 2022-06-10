using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

public static class PushPopExtensions
{
    public static async Task LockPushAsync(this Page parentPage, Page childPage, bool animated = true)
    {
        lock (Locker.locker)
        {
            if (Locker.isBusy)
            {
                return;
            }

            Locker.isBusy = true;
        }
        await parentPage.Navigation.PushAsync(childPage, animated);
        Locker.isBusy = false;
    }

    public static async Task LockPopAsync(this Page childPage, bool animated = true)
    {
        lock (Locker.locker)
        {
            if (Locker.isBusy)
            {
                return;
            }

            Locker.isBusy = true;
        }
        await ((Page)childPage.Parent).Navigation.PopAsync(animated);
        Locker.isBusy = false;
    }

    public static async Task LockPushModalAsync(this Page parentPage, Page childPage, bool animated = true)
    {
        lock (Locker.locker)
        {
            if (Locker.isBusy)
            {
                return;
            }

            Locker.isBusy = true;
        }
        try
        {
            await parentPage.Navigation.PushModalAsync(childPage, animated);
        }
        catch (Exception exception) 
        {
            Locker.isBusy = false;
            throw exception;
        }

        Locker.isBusy = false;
    }

    public static async Task LockPopModalAsync(this Page childPage, bool animated = true)
    {
        lock (Locker.locker)
        {
            if (Locker.isBusy)
            {
                return;
            }

            Locker.isBusy = true;
        }
        await ((Page)childPage.Parent).Navigation.PopModalAsync(animated);
        Locker.isBusy = false;
    }


    public static async Task LockPushModalAsync(this INavigation navigation, Page childPage, bool animated = true)
    {
        lock (Locker.locker)
        {
            if (Locker.isBusy)
            {
                return;
            }

            Locker.isBusy = true;
        }
        try
        {
            await navigation.PushModalAsync(childPage, animated);
        }
        catch (Exception exception) 
        {
            Locker.isBusy = false;
            throw exception;
        }

        Locker.isBusy = false;
    }

    public static async Task LockPopModalAsync(this INavigation navigation, bool animated = true)
    {
        lock (Locker.locker)
        {
            if (Locker.isBusy)
            {
                return;
            }

            Locker.isBusy = true;
        }
        try
        {
            await navigation.PopModalAsync(animated);
        }
        catch (Exception exception) 
        {
            Locker.isBusy = false;
            throw exception;
        }

        Locker.isBusy = false;
    }

    public static async Task LockPushAsync(this INavigation navigation, Page childPage, bool animated = true)
    {
        lock (Locker.locker)
        {
            if (Locker.isBusy)
            {
                return;
            }

            Locker.isBusy = true;
        }
        try
        {
            await navigation.PushAsync(childPage, animated);
        }
        catch (Exception exception) 
        {
            Locker.isBusy = false;
            throw exception;
        }

        Locker.isBusy = false;
    }

    public static async Task LockPopAsync(this INavigation navigation, bool animated = true)
    {
        lock (Locker.locker)
        {
            if (Locker.isBusy)
            {
                return;
            }

            Locker.isBusy = true;
        }
        try
        {
            await navigation.PopAsync(animated);
        }
        catch (Exception exception) 
        {
            Locker.isBusy = false;
            throw exception;
        }

        Locker.isBusy = false;
    }

    public static async Task LockPushPopupAsync(this INavigation navigation, PopupPage childPage, bool animated = true)
    {
        lock (Locker.locker)
        {
            if (Locker.isBusy)
            {
                return;
            }

            Locker.isBusy = true;
        }
        try
        {
            await navigation.PushPopupAsync(childPage, animated);
        }
        catch (Exception exception) 
        {
            Locker.isBusy = false;
            throw exception;
        }

        Locker.isBusy = false;
    }

    public static async Task LockPopPopupAsync(this INavigation navigation, bool animated = true)
    {
        lock (Locker.locker)
        {
            if (Locker.isBusy)
            {
                return;
            }

            Locker.isBusy = true;
        }
        try
        {
            await navigation.PopPopupAsync(animated);
        }
        catch (Exception exception) 
        {
            Locker.isBusy = false;
            throw exception;
        }

        Locker.isBusy = false;
    }
    
    public static async Task LockRemovePopupPageAsync(this INavigation navigation, PopupPage childPage, bool animated = true)
    {
        lock (Locker.locker)
        {
            if (Locker.isBusy)
            {
                return;
            }

            Locker.isBusy = true;
        }
        try
        {
            await navigation.RemovePopupPageAsync(childPage, animated);
        }
        catch (Exception exception) 
        {
            Locker.isBusy = false;
            throw exception;
        }

        Locker.isBusy = false;
    }

    public static async Task LockPopAllPopupAsync(this INavigation navigation, bool animated = true)
    {
        lock (Locker.locker)
        {
            if (Locker.isBusy)
            {
                return;
            }

            Locker.isBusy = true;
        }
        try
        {
            await navigation.PopAllPopupAsync(animated);
        }
        catch (Exception exception) 
        {
            Locker.isBusy = false;
            throw exception;
        }

        Locker.isBusy = false;
    }
}

