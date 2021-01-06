using Android.App;
using SI_Master.Common;
using SI_Master.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(DroidStatusBar))]
namespace SI_Master.Droid
{
	class DroidStatusBar : IStatusBar
	{
		public static Activity Activity { get; set; }

		public int GetHeight()
		{
			int statusBarHeight = -1;
			int resourceId = Activity.Resources.GetIdentifier("status_bar_height", "dimen", "android");
			if (resourceId > 0)
			{
				statusBarHeight = Activity.Resources.GetDimensionPixelSize(resourceId);
			}
			return statusBarHeight;
		}
	}
}