
using SI_Master.Common;

[assembly: Xamarin.Forms.Dependency(typeof(SI_Master.iOS.iOSDeviceInfo))]
namespace SI_Master.iOS
{
    public class iOSDeviceInfo : IDeviceInfo
    {
        public string GetIdentifier()
        {
			return UIKit.UIDevice.CurrentDevice.IdentifierForVendor.AsString();
		}
	}
}