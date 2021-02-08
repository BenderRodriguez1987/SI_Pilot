using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SI_Master.Droid;
using SI_Master.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(ToastNativeService))]
namespace SI_Master.Droid
{
    public class ToastNativeService: IToastService
    {
        public void ShowToast(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
        }
    }
}