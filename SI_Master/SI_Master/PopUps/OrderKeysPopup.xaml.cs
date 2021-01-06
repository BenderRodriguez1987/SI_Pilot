using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SI_Master.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SI_Master.PopUps {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderKeysPopup : PopupPage {

        IDashboardManager dashboardManager = DependencyService.Get<IDashboardManager>();
        public int KeysAmount { get; set; } = 1;
        public OrderKeysPopup()
        {
            InitializeComponent();
            KeysAmountLabel.Text = KeysAmount.ToString();
            DecrementButton.Clicked += (sender, args) =>
            {
                if (KeysAmount > 1)
                {
                    KeysAmount = KeysAmount - 1;
                    KeysAmountLabel.Text = KeysAmount.ToString();
                }
            };
            IncrementButton.Clicked += (sender, args) =>
            {
                KeysAmount = KeysAmount + 1;
                KeysAmountLabel.Text = KeysAmount.ToString();
            };
        }

        private void Back_Clicked(object sender , EventArgs e)
        {
            _ = PopupNavigation.Instance.RemovePageAsync(this);
        }


        private async void Ok_Clicked(object sender, EventArgs e)
        {
            await dashboardManager.OrderKeys(KeysAmount);
            _ = PopupNavigation.Instance.RemovePageAsync(this);
        }
    }
}