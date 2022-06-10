using System;

namespace WeatherApp.Threading
{
    public static class Lock
    {
        public static void LockAsync(Action action)
        {
            lock (Locker.locker)
            {
                if (Locker.isBusy)
                {
                    return;
                }

                Locker.isBusy = true;
            }

            action();
            Locker.isBusy = false;
        }
    }
}

