using System;
using Android.Provider;
using SI_Master.Common;
using Xamarin.Forms;

[assembly: Dependency(typeof(SI_Master.Droid.AndroidDeviceInfo))]
namespace SI_Master.Droid
{
    public class AndroidDeviceInfo : IDeviceInfo
    {
        public string GetIdentifier()
        {
            return Android.Provider.Settings.Secure.GetString(Forms.Context.ContentResolver, Android.Provider.Settings.Secure.AndroidId);
        }
    }
}