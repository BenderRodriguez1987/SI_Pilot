using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using SI_Master;
using SI_Master.iOS;
using SI_Master.Controls;

[assembly: ExportRenderer(typeof(NonSelectableListView), typeof(NonSelectableListViewRenderer))]
namespace SI_Master.iOS
{
	public class NonSelectableListViewRenderer : ListViewRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement != null)
			{
				// Unsubscribe from event handlers and cleanup any resources
			}

			if (e.NewElement != null)
			{
				// Configure the native control and subscribe to event handlers
				Control.AllowsSelection = e.NewElement.SelectionMode == ListViewSelectionMode.Single;
			}
		}
	}
}