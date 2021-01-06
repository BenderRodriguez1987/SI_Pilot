using Acr.UserDialogs;
using SI_Master.Models;
using SI_Master.Services.PushService;
using SI_Master.ViewModels.QRCodePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Net.Mobile.Forms;
using ZXing.QrCode;

namespace SI_Master.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QRCodePage : ContentPage {

        IQRCodePageViewModel viewModel = DependencyService.Get<IQRCodePageViewModel>();
        ZXingBarcodeImageView qrCode;

        ZXingBarcodeImageView GenerateQR(string codeValue)
        {
            var qrCode = new ZXingBarcodeImageView
            {
                BarcodeFormat = BarcodeFormat.QR_CODE,
                BarcodeOptions = new QrCodeEncodingOptions
                {
                    Height = 350,
                    Width = 350
                },
                BarcodeValue = codeValue,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            // Workaround for iOS
            qrCode.WidthRequest = 350;
            qrCode.HeightRequest = 350;
            return qrCode;
        }
        public QRCodePage()
        {
            Title = "QR код";
            InitializeComponent();
            BindingContext = this;
            GenerateButton.Clicked += async (sender, args) =>
            {
                await SetQRCode(true);
            };
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if(qrCode == null)
            {
                await SetQRCode(false);
            }
            MessagingCenter.Subscribe<PushService, string>(this, PushService.STATUS_6, async (obj, visitId) =>
            {
                await SetWorksList(visitId);
            });
            MessagingCenter.Subscribe<PushService, string>(this, PushService.STATUS_10,  (obj, qr) =>
            {
                 ShowClosedQRCode(qr);
            });
            MessagingCenter.Subscribe<PushService, string>(this, PushService.STATUS_12, async (obj, visitId) =>
            {
                await SetQRCode(true);
            });
            MessagingCenter.Subscribe<PushService, string>(this, PushService.STATUS_13, async (obj, visitId) =>
            {
                await SetQRCode(true);
            });

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<PushService, string>(this, PushService.STATUS_10);
            MessagingCenter.Unsubscribe<PushService, string>(this, PushService.STATUS_6);
            MessagingCenter.Unsubscribe<PushService, string>(this, PushService.STATUS_12);
            MessagingCenter.Unsubscribe<PushService, string>(this, PushService.STATUS_13);
        }
        void ShowClosedQRCode(string qr)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                StackLayout rootStack = new StackLayout()
                {
                    WidthRequest = 350,
                    HeightRequest = 350,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
                GenerateButton.IsVisible = false;
                rootLayout.Children.Clear();
                rootLayout.Children.Add(rootStack);
                qrCode = null;
                qrCode = GenerateQR(qr);
                rootStack.Children.Add(qrCode);
            });
        }
        async Task SetWorksList(string visitId)
        {
            MainThread.BeginInvokeOnMainThread(async ()  =>
            {
                NegotiatedOrder order = await viewModel.LoadWorks(visitId);

                StackLayout rootStack = new StackLayout()
                {
                    WidthRequest = 350,
                    HeightRequest = 350,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
                GenerateButton.IsVisible = false;
                rootLayout.Children.Clear();
                rootLayout.Children.Add(rootStack);
                NegotiatedWoirksView view = new NegotiatedWoirksView();
                view.Wheels = order.Wheels;
                view.AdditionalWorks = order.AdditionalWorks;
                rootStack.Children.Add(view);
            });
        }

        async Task SetQRCode(bool isFinished)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                UserDialogs.Instance.ShowLoading();
                string str = await viewModel.LoadQRCode();
                if (rootLayout.Children.Count > 1)
                {
                    rootLayout.Children.RemoveAt(0);
                }
                if (isFinished)
                {
                    rootLayout.Children.RemoveAt(0);
                }
                StackLayout rootStack = new StackLayout()
                {
                    WidthRequest = 350,
                    HeightRequest = 350,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
                GenerateButton.IsVisible = true;
                rootLayout.Children.Insert(0, rootStack);
                qrCode = GenerateQR(str);
                rootStack.Children.Add(qrCode);
                rootStack.Children.Add(GenerateButton);
                UserDialogs.Instance.HideLoading();
            });
        }
    }
}