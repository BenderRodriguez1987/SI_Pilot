using SI_Master.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SI_Master.Settings
{
    public interface IAuthSettings
    {
        int Active { get; set; }
        bool Remove(string login);
        int AddOrUpdate(UserAuthData user);
        void Write(List<UserAuthData> users);
        List<UserAuthData> Read();

        UserAuthData ActiveUser();
    }
}
