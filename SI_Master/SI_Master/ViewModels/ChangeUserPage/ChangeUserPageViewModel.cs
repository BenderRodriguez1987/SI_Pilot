using SI_Master.Managers;
using SI_Master.Models;
using SI_Master.ViewModels.ChangeUserPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(ChangeUserPageViewModel))]
namespace SI_Master.ViewModels.ChangeUserPage
{
    public class ChangeUserPageViewModel : IChangeUserPageViewModel
    {

        IAuthManager authManager = DependencyService.Get<IAuthManager>();
        public ObservableCollection<UserAuthData> Users { get; set; }
        public string Title { get; set; } = "Сменипть пользователя";

        public ChangeUserPageViewModel()
        {

        }

        public async Task<bool> Login(UserAuthData user)
        {
            Answer answer = await authManager.Login(user);
            if (answer.ResType == 0)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
