using UIKit;
using SI_Master.iOS;
using SI_Master.Common;

[assembly: Xamarin.Forms.Dependency(typeof(iOSStatusBar))]
namespace SI_Master.iOS
{
	class iOSStatusBar : IStatusBar
	{
		public int GetHeight()
		{
			return (int)UIApplication.SharedApplication.StatusBarFrame.Height;
		}
	}
}