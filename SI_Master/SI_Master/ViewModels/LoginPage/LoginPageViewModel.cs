using SI_Master.Models;
using SI_Master.REST;
using SI_Master.Settings;
using SI_Master.ViewModels.LoginPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

[assembly: Dependency(typeof(LoginPageViewModel))]
namespace SI_Master.ViewModels.LoginPage
{
    public class LoginPageViewModel : BaseViewModel, ILoginPageViewModel
    {

        private bool _tryLoginActive;

        private bool? _loginInput;

        INetworkService networkservide = DependencyService.Get<INetworkService>();
        IAuthSettings authSettings = DependencyService.Get<IAuthSettings>();

        public ObservableCollection<UserAuthData> Users { get; set; }

        public ICommand RemoveUserClicked { get; set; }


    }
}
