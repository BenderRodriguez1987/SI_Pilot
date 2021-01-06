using System;
using Xamarin.Forms;
using SI_Master;
using SI_Master.Droid;
using Xamarin.Forms.Platform.Android;
using Android.Views.InputMethods;
using Android.App;
using Android.Content;
using SI_Master.Common;

[assembly: Xamarin.Forms.Dependency(typeof(DroidKeyboardHelper))]
namespace SI_Master.Droid
{
    public class DroidKeyboardHelper : IKeyboardHelper
    {
        public void HideKeyboard()
        {
            var context = Forms.Context;
            var inputMethodManager = context.GetSystemService(Context.InputMethodService) as InputMethodManager;
            if (inputMethodManager != null && context is Activity)
            {
                var activity = context as Activity;
                var token = activity.CurrentFocus?.WindowToken;
                inputMethodManager.HideSoftInputFromWindow(token, HideSoftInputFlags.None);

                activity.Window.DecorView.ClearFocus();
            }
        }
    }
}