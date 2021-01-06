using System;
using Xamarin.Forms;
using SI_Master;
using SI_Master.iOS;
using UIKit;
using SI_Master.Common;

[assembly: Dependency(typeof(iOSKeyboardHelper))]
namespace SI_Master.iOS
{
    public class iOSKeyboardHelper : IKeyboardHelper
    {
        public void HideKeyboard()
        {
            UIApplication.SharedApplication.KeyWindow.EndEditing(true);
        }
    }
}