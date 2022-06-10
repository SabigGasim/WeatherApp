public static class Locker
{
    public static readonly object locker = new object();
    public static bool isBusy = false;
}