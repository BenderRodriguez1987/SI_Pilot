using SI_Master.Common;
using SI_Master.Managers;
using SI_Master.Models;
using SI_Master.REST;
using SI_Master.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SI_Master.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private bool _tryLoginActive;

        private bool? _loginInput;

        INetworkService networkservide = DependencyService.Get<INetworkService>();
        IAuthSettings authSettings = DependencyService.Get<IAuthSettings>();
        IAuthManager authManager = DependencyService.Get<IAuthManager>();
        IDashboardManager dashboardManager = DependencyService.Get<IDashboardManager>();

        private ObservableCollection<UserAuthData> _users { get; set; }
        public ObservableCollection<UserAuthData> Users { get {
                return _users; 
            }
            set {
                _users = value;
                OnPropertyChanged("Users");
            } 
        }
        public string RemoveButtonText = "\ue608";
        public ICommand RemoveUserClicked { get; set; }

        public LoginPage(bool tryLoginActive = true)
        {
            InitializeComponent();
            _tryLoginActive = tryLoginActive;
            _loginInput = null;
            Users = new ObservableCollection<UserAuthData>(authSettings.Read());
            RemoveUserClicked = new Command(execute: (object arg) => { if (arg is UserAuthData) RemoveUser((UserAuthData)arg); });
            BindingContext = this;
            //usersListView.ItemSelected += (sender, args) =>
            // {
            //     ((UserAuthData)usersListView.SelectedItem).IsSelected = true;
            //     OnPropertyChanged("Users");
            // };
        }

        private async void RemoveUser(UserAuthData user)
        {
            if (await DisplayAlert("", "Удалить учетную запись?", "Да", "Нет"))
            {
                if (usersListView.SelectedItem == user)
                {
                    usersListView.SelectedItem = null;
                    selectButton.IsVisible = false;
                }
                Users.Remove(user);
                List<UserAuthData> tmplist = new List<UserAuthData>();
                tmplist.Add(user);
                authManager.ForgetFCMToken(tmplist);
                authSettings.Remove(user.Client);
                if (Users.Count == 0) ShowLoginInput();
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            int activeUser = authSettings.Active;
            if (_tryLoginActive && activeUser > -1 && activeUser < Users.Count)
            {
                _tryLoginActive = false;
                UserAuthData user = Users[activeUser];

                ShowBusy();
                //Answer answer = await authManager.CheckUserCredentials(null ,null);
                Answer answer = new Answer();
                if (answer.ResType == 0)
                {
                    Application.Current.MainPage = new MainPage(activeUser);
                    await Navigation.PopAsync();
                    return;
                } else
                {
                   authSettings.Active = -1;
                }

                ShowLoginInput();
                return;
            }

            if (_loginInput.HasValue)
            {
                if (_loginInput.Value) ShowLoginInput(); else ShowUserSelection();
            }
            else
            {
                if (Users.Count > 0) ShowUserSelection(); else ShowLoginInput();
            }
        }

        private void ShowBusy()
        {
            loginGroup.IsVisible = false;
            userSelectionGroup.IsVisible = false;
            busyIndicator.IsEnabled = true;
            busyIndicator.IsVisible = true;
            busyIndicator.IsRunning = true;
        }

        private void ShowInvalidCaptions(bool show)
        {
            loginInvalidCaption.IsVisible = show;
            passwordInvalidCaption.IsVisible = show;
            //codeInvalidCaption.IsVisible = show;
        }

        private void ShowLoginInput()
        {
            busyIndicator.IsEnabled = false;
            busyIndicator.IsVisible = false;
            busyIndicator.IsRunning = false;
            userSelectionGroup.IsVisible = false;
            ShowInvalidCaptions(false);
            Title = "Вход";
            loginGroup.IsVisible = true;
            _loginInput = true;
        }

        private void ShowUserSelection()
        {
            DependencyService.Get<IKeyboardHelper>().HideKeyboard();
            busyIndicator.IsEnabled = false;
            busyIndicator.IsVisible = false;
            busyIndicator.IsRunning = false;
            loginGroup.IsVisible = false;
            usersListView.SelectedItem = authSettings.ActiveUser();
            if (authSettings.Active >= 0) {
                Users[authSettings.Active].isActive = true;
            }
            Title = "Учетные записи";
            userSelectionGroup.IsVisible = true;
            UsersListView_ItemSelected(usersListView, null);
            _loginInput = false;

        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            #region validate input fields

            if (string.IsNullOrWhiteSpace(loginEntry.Text)) // min length ???
            {
                ShowInvalidCaptions(true);
                loginEntry.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(passwordEntry.Text)) // min length ???
            {
                ShowInvalidCaptions(true);
                passwordEntry.Focus();
                return;
            }
            //if (string.IsNullOrWhiteSpace(codeEntry.Text))
            //{
            //    ShowInvalidCaptions(true);
            //    codeEntry.Focus();
            //    return;
            //}

            #endregion

            ShowBusy();

            //Answer answer = await authManager.RegisterDevice(loginEntry.Text, passwordEntry.Text);
            //if (answer.ResType != NetworkService.OK ) 
            //{
            //    ShowLoginInput();
            //    await DisplayAlert("", answer.ResMsg, "OK");
            //    return;
            //}
            //if (answer.ResType == NetworkService.OK && answer.ResData is AuthData authdata)
            //{
            //UserAuthData user = new UserAuthData()
            //{
            //    Caption = "Новый пользователь", // ??? get from response.Data
            //    Client = authdata.Client,
            //    Token = authdata.Token
            //};
            UserAuthData user = new UserAuthData()
            {
                Caption = "Новый пользователь", // ??? get from response.Data
                Client = "sdfasdf",
                Token = "asdfasdf"
            };
            List<UserAuthData> authList = new List<UserAuthData>();
            authList.Add(user);
            //await authManager.MemorizeFCMTken(authList);
            authSettings.Active = authSettings.AddOrUpdate(user);
            //AboutOrganozation organisation = await dashboardManager.GetDashboard();
            AboutOrganozation organisation = new AboutOrganozation();
            user.Caption = organisation.FullCaption;
            authSettings.Active = authSettings.AddOrUpdate(user);
            Application.Current.MainPage = new MainPage(authSettings.Active);

            await Navigation.PopAsync();
            //}
        }

        private async void SelectButton_Clicked(object sender, EventArgs e)
        {
            if (usersListView.SelectedItem == null) return;
            UserAuthData user = (UserAuthData)usersListView.SelectedItem;
            usersListView.SelectedItem = null;

            ShowBusy();

            Answer answer = await authManager.CheckUserCredentials(user.Client, user.Token);
            if (answer.ResType !=  NetworkService.OK)
            {
                ShowUserSelection();
                await DisplayAlert("",  answer.ResMsg, "OK");
                return;
            }
            if(answer.ResType == NetworkService.OK)
            {
                authSettings.Active = Users.IndexOf(user);

                Application.Current.MainPage = new MainPage(authSettings.Active);
                await Navigation.PopAsync();
            }
        }

        private void LoginHelp_Tapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://soft-enginiring.ru/forget-password/"));
        }

        private void OtherAccounts_Tapped(object sender, EventArgs e)
        {
            ShowUserSelection();
        }

        private void NewAccount_Tapped(object sender, EventArgs e)
        {
            ShowLoginInput();
        }

        private void PhoneLabel_Tapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("tel:88003015715"));
        }

        private void PrivacyPolicyLabel_Tapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://soft-enginiring.ru/confidential.pdf"));
        }

        private void UsersListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            selectButton.IsVisible = (usersListView.SelectedItem != null);
        }
    }
}