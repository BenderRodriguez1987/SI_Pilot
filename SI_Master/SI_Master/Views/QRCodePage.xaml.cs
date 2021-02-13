using Acr.UserDialogs;
using SI_Master.Models;
using SI_Master.Services.PushService;
using SI_Master.Settings;
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
        IAuthSettings authsetting = DependencyService.Get<IAuthSettings>();
        ZXingBarcodeImageView qrCode;
        public const string STATUS_KEY = "status";
        public const string VISIT_ID_KEY = "visit_id";
        public const string QR_KEY = "qr";

        ZXingBarcodeImageView GenerateQR(string codeValue) {
            var qrCode = new ZXingBarcodeImageView {
                BarcodeFormat = BarcodeFormat.QR_CODE,
                BarcodeOptions = new QrCodeEncodingOptions {
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
        public QRCodePage() {
            Title = "QR код";
            InitializeComponent();
            BindingContext = this;
            GenerateButton.Clicked += async (sender, args) => {
                SetQRCode(true, true);
            };
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
                var visitstate = authsetting.ReadVisitId();
                if(visitstate.Count > 0) {
                VisitStatus status = await viewModel.LoadOrderState(visitstate[VISIT_ID_KEY]);

                int visit_int = status.Status;

                if (0 < visit_int & visit_int<= 5) {
                    SetQRCode(true, true);
                }
                if(visit_int >= 6 & visit_int < 10) {
                    SetWorksList(visitstate[VISIT_ID_KEY]);
                }
              if(visit_int >=10 & visit_int < 12) {
                    // сделано так, что если не получен пуш с кодом, то удалять заезд и начинать с начала ибо пока что более неоткуда брать этот код
                    if (visitstate.ContainsKey(QR_KEY)) {
                        SetWorksList(visitstate[VISIT_ID_KEY]);
                        ShowClosedQRCode(visitstate[QR_KEY]);
                    }
                    else {
                        authsetting.RemoveVisitId(visit_int.ToString());
                        SetQRCode(true, true);

                    }
                }
                if (visit_int == 12 || visit_int == 13) {
                    authsetting.RemoveVisitId(visit_int.ToString());
                    SetQRCode(true, true);
                }
            }
            else {
                SetQRCode(true, true);
            }
            MessagingCenter.Subscribe<PushService, string>(this, PushService.STATUS_6, (obj, visitId) => {
                SetWorksList(visitId);
            });
            MessagingCenter.Subscribe<PushService, string>(this, PushService.STATUS_10,  (obj, qr) => {
                 ShowClosedQRCode(qr);
            });
            MessagingCenter.Subscribe<PushService, string>(this, PushService.STATUS_12, (obj, visitId) => {
                SetQRCode(true, true);
            });
            MessagingCenter.Subscribe<PushService, string>(this, PushService.STATUS_13, (obj, visitId) => {
                SetQRCode(true, false);
            });

        }

        protected override void OnDisappearing()  {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<PushService, string>(this, PushService.STATUS_10);
            MessagingCenter.Unsubscribe<PushService, string>(this, PushService.STATUS_6);
            MessagingCenter.Unsubscribe<PushService, string>(this, PushService.STATUS_12);
            MessagingCenter.Unsubscribe<PushService, string>(this, PushService.STATUS_13);
            rootLayout.Children.Clear();
        }
        private void ShowClosedQRCode(string qr) {
            MainThread.BeginInvokeOnMainThread(async () => {
                StackLayout rootStack = new StackLayout() {
                    WidthRequest = 350,
                    HeightRequest = 350,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
                GenerateButton.IsVisible = false;
                rootLayout.Children.Add(rootStack);
                qrCode = null;
                qrCode = GenerateQR(qr);
                rootStack.Children.Add(qrCode);
            }); 
        }

        private void SetWorksList(string visitId) {
            MainThread.BeginInvokeOnMainThread(async ()  => {
                if (rootLayout.Children.Count > 0) {
                    rootLayout.Children.RemoveAt(0);
                }
                NegotiatedOrder order = await viewModel.LoadWorks(visitId);
                StackLayout rootStack = new StackLayout() {
                    WidthRequest = 350,
                    HeightRequest = 350,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
                GenerateButton.IsVisible = false;
                rootLayout.Children.Add(rootStack);
                NegotiatedWoirksView view = new NegotiatedWoirksView();
                view.Wheels = order.Wheels;
                view.AdditionalWorks = order.AdditionalWorks;
                rootStack.Children.Add(view);
            });
        }

        private void SetQRCode(bool isFinished, bool asd) {
            MainThread.BeginInvokeOnMainThread(async () => {
                try {
                    UserDialogs.Instance.ShowLoading();
                    string str = await viewModel.LoadQRCode();
                    if (rootLayout.Children.Count > 1)
                    {
                        rootLayout.Children.RemoveAt(0);
                    }
                    if (isFinished & rootLayout.Children.Count > 0)
                    {
                        rootLayout.Children.Clear();
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
                    if (asd)
                    {
                        qrCode = null;
                        qrCode = GenerateQR(str);
                        rootStack.Children.Add(qrCode);
                    }
                    else
                    {
                        Image img = new Image();
                        rootStack.Children.Add(img);
                    }
                    rootStack.Children.Add(GenerateButton);
                    UserDialogs.Instance.HideLoading();
                } catch(Exception e) {
                    System.Diagnostics.Debug.WriteLine(e);
                }

            });
        }
    }
}