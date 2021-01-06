using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SI_Master.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BARCodeScanerPage : ContentPage {
        public BARCodeScanerPage()
        {
            InitializeComponent();
            Scan();
            ScanButton.Clicked += (sender, args) =>
            {
                Scan();
            };
         }
        public  async Task Scan()
        {
#if __ANDROID__
	// Initialize the scanner first so it can track the current context
	MobileBarcodeScanner.Initialize (Application);
#endif

            var scanner = new ZXing.Mobile.MobileBarcodeScanner();

            var result = await scanner.Scan();

            if (result != null)
            {
                
                Label label = new Label();
                label.FontSize = 22;
                label.VerticalOptions = LayoutOptions.CenterAndExpand;
                label.HorizontalOptions = LayoutOptions.CenterAndExpand;
                label.Text = result.Text;
                rootStack.Children.Add(label);
            }
        }
    }
}