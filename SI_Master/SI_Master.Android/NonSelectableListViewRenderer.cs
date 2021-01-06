using Android.Content;
using Xamarin.Forms.Platform.Android;
using SI_Master;
using SI_Master.Droid;
using Xamarin.Forms;
using SI_Master.Common;
using SI_Master.Controls;

[assembly: ExportRenderer(typeof(NonSelectableListView), typeof(NonSelectableListViewRenderer))]
namespace SI_Master.Droid
{
	public class NonSelectableListViewRenderer : ListViewRenderer
	{
		public NonSelectableListViewRenderer(Context context) : base(context)
		{
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null)
			{
				// Unsubscribe from event handlers and cleanup any resources
			}

			if (e.NewElement != null)
			{
				// Configure the native control and subscribe to event handlers
				Control.SetSelector(Android.Resource.Color.Transparent);
				Control.CacheColorHint = Xamarin.Forms.Color.Transparent.ToAndroid();
			}

		}
	}
}