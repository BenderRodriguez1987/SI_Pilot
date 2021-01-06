using Acr.UserDialogs;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using SI_Master.Managers.SendLimitsManager;
using SI_Master.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SI_Master.PopUps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardBlockPopup : PopupPage
    {
        public enum CardAction
        {
            Block,
            Unlock
        }
        private bool ActionState;
        private const int ADDITIONAL_REASON_HEUGHT = 36;
        private const int ADDITIONAL_COMMENT_HEIGHT =36;
        private double DEFAULT_POPUP_HEIGHT;
        public string Title { get{
                return _title;
            }
            set {
                _title = value;
            } }
        private string _title { get; set; }
        private List<string> _reasonDatasource = new List<string>(){ "Другая", "Стоп-лист", "Потеряна" };
        public List<string> ReasonDataSource {
            get { return _reasonDatasource; }
            set {
                _reasonDatasource = value;
            }
        }

        private string _cardNumber { get; set; }
        ISendLimitsManager cardManager = DependencyService.Get<ISendLimitsManager>();

        public CardBlockPopup(CardAction cardAction, string cardNumber )
        {
            InitializeComponent();
            BindingContext = this;
            _cardNumber = cardNumber;
            switch (cardAction)
            {
                case CardAction.Unlock:
                    TitleLabel.Text = "Разблокировать карту?";
                    changeReasonStack.IsVisible = false;
                    commentStack.IsVisible = false;
                    ActionState = true;
                    break;
                case CardAction.Block:
                    TitleLabel.Text = "Заблокировать карту? ";
                    changeReasonPicker.ItemsSource = ReasonDataSource;
                    changeReasonStack.IsVisible = true;
                    DEFAULT_POPUP_HEIGHT = rootStack.Height;
                    changeReasonPicker.SelectedIndexChanged += (sender, args) =>
                    {
                        if(changeReasonPicker.SelectedIndex == 0) {
                            commentStack.IsVisible = true;
                            rootStack.HeightRequest = rootStack.Height + ADDITIONAL_COMMENT_HEIGHT;
                        } else {
                            commentStack.IsVisible = false;
                            if(rootStack.Height != DEFAULT_POPUP_HEIGHT) {
                                rootStack.HeightRequest = DEFAULT_POPUP_HEIGHT;
                            }
                        }
                    };
                    ActionState = false;
                    break;
            }

        }

        private async void Ok_Clicked(object sender, EventArgs args) {
            Dictionary<string, Dictionary<string, string>> cardLock = new Dictionary<string, Dictionary<string, string>>();
            Dictionary<string, string> sendObject = new Dictionary<string, string>();
            if (ActionState){
                sendObject.Add("action", "Разблокировка");
            } else {
                sendObject.Add("action", "Блокировка");
                sendObject.Add("reason", (string)changeReasonPicker.SelectedItem);
                if (changeReasonPicker.SelectedIndex == 0) {
                    sendObject.Add("comment", reasonEntry.Text);
                }
            }
            cardLock.Add(_cardNumber , sendObject);
            UserDialogs.Instance.ShowLoading();
            await cardManager.LockOrUnlockCard(cardLock);
            UserDialogs.Instance.HideLoading();
            _ = PopupNavigation.Instance.RemovePageAsync(this);
        }
        private void Cancel_Clicked(object sender, EventArgs args) {
            _ = PopupNavigation.Instance.RemovePageAsync(this);
        }
    }
}