using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SI_Master.Models;
using SI_Master.Views;

namespace SI_Master.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();

        private int _currentUserId { get; set; }
        public MainPage(int currentUserId)
        {
            InitializeComponent();
            _currentUserId = currentUserId;
            NavigationPage.SetHasNavigationBar(this, false);
            MasterBehavior = MasterBehavior.Popover;
            MenuPages.Add((int)MenuItemType.DeskTop, (NavigationPage)Detail);
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    //case (int)MenuItemType.Map:
                    //    MenuPages.Add(id, new NavigationPage(new MapPage()));
                    //    break;
                    case (int)MenuItemType.QRCode:
                        MenuPages.Add(id, new NavigationPage(new QRCodePage()));
                        break;
                    case (int)MenuItemType.BarCodeScaner:
                        MenuPages.Add(id, new NavigationPage(new BARCodeScanerPage()));
                        break;
                    case (int)MenuItemType.DeskTop:
                        MenuPages.Add(id, new NavigationPage(new DeskTopPage()));
                        break;
                    case (int)MenuItemType.Settlements:
                        MenuPages.Add(id, new NavigationPage(new SettlementsPage()));
                        break;
                    case (int)MenuItemType.Cards:
                        MenuPages.Add(id, new NavigationPage(new CardsPage()));
                        break;
                    case (int)MenuItemType.Transactions:
                        MenuPages.Add(id, new NavigationPage(new TransactionsPage()));
                        break;
                    case (int)MenuItemType.ArchiveCardLimits:
                        MenuPages.Add(id, new NavigationPage(new ArchiveCardLimitsPage()));
                        break;
                    case (int)MenuItemType.ArchiveCardOrder:
                        MenuPages.Add(id, new NavigationPage(new ArchiveCardOrderPage()));
                        break;
                    case (int)MenuItemType.ArchiveCardBlock:
                        MenuPages.Add(id, new NavigationPage(new ArchiveCardBlockPage()));
                        break;
                    case (int)MenuItemType.Dashboard:
                        MenuPages.Add(id, new NavigationPage(new DashboardPage()));
                        break;
                    case (int)MenuItemType.ChangeUser:
                        MenuPages.Add(id, new NavigationPage(new LoginPage()));
                        break;
                    case (int)MenuItemType.Exit:
                        if (await DisplayAlert("Выход", "Закрыть приложение?", "OK", "Отмена")) System.Diagnostics.Process.GetCurrentProcess().Kill();
                        //MenuPages.Add(id, new NavigationPage(new AboutPage()));
                        break;
                }
            }

            var newPage = MenuPages[id];
            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);
                IsPresented = false;
            }
        }
    }
}