using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
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
    public partial class CardLimitsPopup : PopupPage
    {
        public Action OkClicked { get; set; }
        public Action ChangeLimitsClicked { get; set; }
        public Action BlockCard { get; set; }

        private List<Limits> _DataSource;
        public List<Limits> DataSource
        {
            get
            {
                return _DataSource;
            }
            set
            {
                _DataSource = value;
                OnPropertyChanged("DataSource");
            }
        }
        private bool _isLowed { get; set; } = false;
        public bool isChangeButtonAlowed
        {
            get {
                return _isLowed;
            }
            set {
                _isLowed = value;
                OnPropertyChanged("isChangeButtonAlowed");
            }
        }
        public CardLimitsPopup(List<Limits> limits, bool isChangesNeeded, bool archivePopup)
        {
            InitializeComponent();
            BindingContext = this;
            Title = "Лимиты карты";
            DataSource = limits;
            isChangeButtonAlowed = isChangesNeeded;
            if (limits == null | limits.Count == 0)
            {
                LimitsList.IsVisible = false;
                PlaceHolderLabel.IsVisible = true;
            }
            if (archivePopup)
            {
                BlockButton.IsVisible = false;
            } else
            {
                BlockButton.IsVisible = true;
            }
            if (isChangesNeeded){
                BlockButton.Text = "ЗАБЛОКИРОВАТЬ";
            } else {
                BlockButton.Text = "РАЗБЛОКИРОВАТЬ";
            }
        }

        private void Ok_Clicked(object sender, EventArgs e)
        {
            OkClicked?.Invoke();
            PopupNavigation.Instance.RemovePageAsync(this);
        }

        private void ChangeLimits_Clicked(object sender, EventArgs e)
        {
            ChangeLimitsClicked.Invoke();
            PopupNavigation.Instance.RemovePageAsync(this);
        }

        private void BlockCard_Clicked(object sender, EventArgs e)
        {
            BlockCard.Invoke();
            PopupNavigation.Instance.RemovePageAsync(this);
        }
    }
}