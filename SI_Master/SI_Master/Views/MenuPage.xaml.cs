using SI_Master.Cells;
using SI_Master.Common;
using SI_Master.Models;
using SI_Master.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SI_Master.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {

        IAuthSettings authSettings = DependencyService.Get<IAuthSettings>();
        MainPage RootPage {get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;

        public MenuPage()
        {
            InitializeComponent();
            menuItems = new List<HomeMenuItem>
            {
                //new HomeMenuItem {Id = MenuItemType.Map, Title="Карта", Icon = "\ue65e", IconColor = "#fa71cd" },
                new HomeMenuItem {Id = MenuItemType.QRCode, Title="QR код", Icon = "\ue664", IconColor = "#f68084" },
               // new HomeMenuItem {Id = MenuItemType.BarCodeScaner, Title="Сканировать", Icon = "\ue64d", IconColor = "#3cba92" },
                //new HomeMenuItem {Id = MenuItemType.DeskTop, Title="Рабочий стол", Icon = "\ue65e", IconColor = "#fa71cd" },
                //new HomeMenuItem {Id = MenuItemType.Cards, Title="Топливные карты", Icon = "\ue664", IconColor = "#f68084" },
                //new HomeMenuItem {Id = MenuItemType.Transactions, Title="Транзакции", Icon = "\ue64d", IconColor = "#3cba92" },
                //new HomeMenuItem {Id = MenuItemType.Settlements, Title="Взаиморасчёты", Icon = "\ue68c", IconColor = "#96c93d" , IsSeparator = true},
                //new HomeMenuItem {Id = MenuItemType.ArchiveRequestsHeader, Title="Архивные запросы",  Icon = "\ue607", IconColor = "#f68084"},         
                //    //new HomeMenuItem {Id = MenuItemType.ArchiveCardLimits, Title="Заявки на лимиты", Icon = "\ue607", IconColor = "#f68084" },
                //    //new HomeMenuItem {Id = MenuItemType.ArchiveCardOrder, Title="Заявки на карты", Icon = "\ue659", IconColor = "#fa71cd" },
                //    //new HomeMenuItem {Id = MenuItemType.ArchiveCardBlock, Title="Заявки на блокировку", Icon = "\ue626", IconColor = "#96c93d" },
                //new HomeMenuItem {Id = MenuItemType.ArchiveCardLimits, Title="Заявки на лимиты" },
                //new HomeMenuItem {Id = MenuItemType.ArchiveCardOrder, Title="Заявки на карты"},
                //new HomeMenuItem {Id = MenuItemType.ArchiveCardBlock, Title="Заявки на блокировку", IsSeparator = true },
                //new HomeMenuItem {Id = MenuItemType.Dashboard, Title="Карточка предприятия", Icon = "\ue68f", IconColor = "#8ec5fc" },
                //new HomeMenuItem {Id = MenuItemType.ChangeUser, Title="Смена пользователя", Icon = "\ue605", IconColor = "#009efd" },
                //new HomeMenuItem {Id = MenuItemType.Exit, Title="Выход", Icon = "\ue6a8", IconColor = "#2a5298" }
            };

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    userInfo.Padding = new Thickness(10, 8 + DependencyService.Get<IStatusBar>().GetHeight(), 10, 20);
                    break;

                default:
                    userInfo.Padding = new Thickness(10, 8, 10, 20);
                    break;
            }
            ListViewMenu.ItemsSource = menuItems;
            ListViewMenu.SelectedItem = menuItems[0];

            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;
                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                if (id != 10) {
                    await RootPage.NavigateFromMenu(id);
                }
            };
            InfoLabel.Text = (authSettings.ActiveUser()).Caption;
        }

        private void UserInfo_Tapped(object sender, EventArgs e)
        {
            //TODO
        }
    }
}