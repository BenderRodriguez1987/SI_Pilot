using SI_Master.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace SI_Master.ViewModels.ChangeUserPage
{
    public interface IChangeUserPageViewModel
    {
        ObservableCollection<UserAuthData> Users { get; set; }
        Task<bool> Login(UserAuthData user);
        string Title { get; set; }
    }
}
