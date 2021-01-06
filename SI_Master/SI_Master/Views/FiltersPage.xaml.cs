using SI_Master.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SI_Master.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FiltersPage : ContentPage
    {
        public delegate void ApplyFilterDelegate();

        private event ApplyFilterDelegate _applyFilterEvent;
        private QueryMode _mode;
        private Dictionary<string, object> _filterParams;
        private string Filters = "filters";
        private List<string> statusPickerSource = new List<string>();

        public FiltersPage(QueryMode mode, Dictionary<string, object> filterParams, ApplyFilterDelegate applyFilterEvent)
        {
            InitializeComponent();
            _mode = mode;
            _filterParams = filterParams;
            _applyFilterEvent += applyFilterEvent;
            SetupFilters();
        }

        private void SetupFilters()
        {
            switch (_mode)
            {
                case QueryMode.Settlements:
                    SetupSuppliar();
                    SetupPeriod();
                    break;

                case QueryMode.Cards:
                    List<string> itemSource = new List<string>()
                    {
                        "Активна",
                        "Стоп лист"
                    };
                    cardStatusPicker.ItemsSource = itemSource;
                    statusPickerSource = itemSource;
                    SetupNumber();
                    SetupCardHolder();
                    SetupContractNumber();
                    SetupCardStatus();
                    break;

                case QueryMode.Transactions:
                    SetupCardNumber();
                    SetupSuppliar();
                    SetupPeriod();
                    break;

                case QueryMode.RequestLimits:
                    List<string> reqSource = new List<string>()
                    {
                        "Все",
                        "В обработке",
                        "Обработана",
                        "Не найдена",
                        "Отправлена",
                        "Не актуальна",
                        "Отменена оператором"
                    };
                    cardStatusPicker.ItemsSource = reqSource;
                    statusPickerSource = reqSource;
                    SetupPeriod();
                    SetupCardNumber();
                    SetupCardStatus();
                    break;

                case QueryMode.RequestCards:
                    List<string> codeReq = new List<string>()
                    {
                        "Все",
                        "В обработке",
                        "Обработана",
                        "Не найдена",
                        "Отправлена",
                        "Не актуальна",
                        "Отменена оператором"
                    };
                    cardStatusPicker.ItemsSource = codeReq;
                    statusPickerSource = codeReq;
                    SetupPeriod();
                    SetupCardStatus();
                    break;

                case QueryMode.RequestBlocking:
                    List<string> blockReq = new List<string>()
                    {
                        "Все",
                        "В обработке",
                        "Обработана",
                        "Не найдена",
                        "Отправлена",
                        "Не актуальна",
                        "Отменена оператором"
                    };
                    cardStatusPicker.ItemsSource = blockReq;
                    statusPickerSource = blockReq;
                    SetupPeriod();
                    SetupCardNumber();
                    SetupCardStatus();
                    break;
            }
        }

        private void SetupCardStatus()
        {
            object obj;
            if (_filterParams.TryGetValue(Filters +"[status]", out obj) && obj is string) cardStatusPicker.SelectedIndex = statusPickerSource.IndexOf(obj.ToString()); else cardStatusPicker.SelectedIndex = 0;
            cardStatusFrame.IsVisible = true;
        }

        private void SetupContractNumber()
        {
            object obj;
            if (_filterParams.TryGetValue(Filters + "[contract]", out obj) && obj is string) contractNumberEntry.Text = obj.ToString(); else contractNumberEntry.Text = "";
            contractNumberFrame.IsVisible = true;
        }

        private void SetupCardHolder()
        {
            object obj;
            if (_filterParams.TryGetValue(Filters + "[holder]", out obj) && obj is string) cardHolderEntry.Text = obj.ToString(); else cardHolderEntry.Text = "";
            cardHolderFrame.IsVisible = true;
        }
        private void SetupSuppliar()
        {
            object obj;
            if (_filterParams.TryGetValue(Filters + "[supplier]", out obj) && obj is string) cardHolderEntry.Text = obj.ToString(); else cardHolderEntry.Text = "";
            cardHolderFrame.IsVisible = true;
        }

        //private void SetupRequestStatus()
        //{
        //    object obj;

        //    if (_filterParams.TryGetValue("RequestStatus", out obj) && obj is int)
        //    {
        //        requestStatusPicker.SelectedIndex = (int)obj;
        //    }
        //    else
        //    {
        //        requestStatusPicker.SelectedIndex = 0;
        //    }

        //    requestStatusFrame.IsVisible = true;
        //}

        private void SetupNumber()
        {
            object obj;
            if (_filterParams.TryGetValue(Filters + "[number]", out obj) && obj is string) cardNumberEntry.Text = obj.ToString(); else cardNumberEntry.Text = "";
            cardNumberFrame.IsVisible = true;
        }

        private void SetupCardNumber()
        {
            object obj;
            if (_filterParams.TryGetValue(Filters + "[card_number]", out obj) && obj is string) cardNumberEntry.Text = obj.ToString(); else cardNumberEntry.Text = "";
            cardNumberFrame.IsVisible = true;
        }

        //private void SetupRequestId()
        //{
        //    object obj;
        //    if (_filterParams.TryGetValue("RequestId", out obj) && obj is string) requestIdEntry.Text = obj.ToString(); else requestIdEntry.Text = "";
        //    requestIdFrame.IsVisible = true;
        //}

        private void SetupPeriod()
        {
            object obj;
            if (_filterParams.TryGetValue(Filters + "[date_from]", out obj) && obj is DateTime)
            {
                periodStart.Date = (DateTime)obj;
            }
            else
            {
                periodStart.NullableDate = null;
            }
            if (_filterParams.TryGetValue(Filters + "[date_to]", out obj) && obj is DateTime)
            {
                periodEnd.Date = (DateTime)obj;
            }
            else
            {
                periodEnd.NullableDate = null;
            }
            periodFrame.IsVisible = true;
        }

        private async void ApplyFilterButton_Clicked(object sender, EventArgs e)
        {
            
            switch (_mode)
            {
                case QueryMode.Cards:
                    if (!string.IsNullOrEmpty(contractNumberEntry.Text))
                    {
                        _filterParams[Filters + "[contract]"] = contractNumberEntry.Text;
                    }
                    else { _filterParams.Remove(Filters + "[contract]"); }
                    break;
            }
            switch (_mode)
            {
                case QueryMode.Cards:
                    if (!string.IsNullOrEmpty(cardNumberEntry.Text))
                    {
                        _filterParams[Filters + "[number]"] = cardNumberEntry.Text;
                    }
                    else { _filterParams.Remove(Filters + "[number]"); }
                    break;
                case QueryMode.Transactions:
                    if (!string.IsNullOrEmpty(cardNumberEntry.Text))
                    {
                        _filterParams[Filters + "[card_number]"] = cardNumberEntry.Text;
                    }
                    else { _filterParams.Remove(Filters + "[card_number]"); }
                    break;
                case QueryMode.RequestLimits:
                    if (!string.IsNullOrEmpty(cardNumberEntry.Text))
                    {
                        _filterParams[Filters + "[card_number]"] = cardNumberEntry.Text;
                    }
                    else { _filterParams.Remove(Filters + "[card_number]"); }
                    break;
                case QueryMode.RequestBlocking:
                    if (!string.IsNullOrEmpty(cardNumberEntry.Text))
                    {
                        _filterParams[Filters + "[card_number]"] = cardNumberEntry.Text;
                    }  else { _filterParams.Remove(Filters + "[card_number]"); }
                    break;
            }
            switch (_mode)
            {
                case QueryMode.Cards:
                    if (!string.IsNullOrEmpty(cardHolderEntry.Text))
                    {
                        _filterParams[Filters + "[holder]"] = cardHolderEntry.Text;
                    } else { _filterParams.Remove(Filters + "[supplier]"); }
                    break;
                case QueryMode.Transactions:
                    if (!string.IsNullOrEmpty(cardHolderEntry.Text))
                    {
                        _filterParams[Filters + "[supplier]"] = cardHolderEntry.Text;
                    } else { _filterParams.Remove(Filters + "[supplier]"); }
                    break;
                case QueryMode.Settlements:
                    if (!string.IsNullOrEmpty(cardHolderEntry.Text))
                    {
                        _filterParams[Filters + "[supplier]"] = cardHolderEntry.Text;
                    } else { _filterParams.Remove(Filters + "[supplier]"); }
                    break;
            }

            if (!string.IsNullOrEmpty(cardStatusPicker.SelectedIndex.ToString()))
            {
                _filterParams[Filters + "[status]"] = cardStatusPicker.SelectedItem;
                if(cardStatusPicker.SelectedIndex == 0)
                {
                    _filterParams.Remove(Filters + "[status]");
                }
            }
            if (periodStart.NullableDate != null)
            {
                _filterParams[Filters + "[date_from]"] = periodStart.NullableDate;
            } else { _filterParams.Remove(Filters + "[date_from]"); }
            if (periodEnd.NullableDate != null)
            {
                _filterParams[Filters + "[date_to]"] = periodEnd.NullableDate;
            } else { _filterParams.Remove(Filters + "[date_to]"); }
            //_filterParams["RequestStatus"] = requestStatusPicker.SelectedIndex;
            //_filterParams["RequestId"] = requestIdEntry.Text;

            await Navigation.PopAsync(false);

            _applyFilterEvent?.Invoke();
        }
    }
}