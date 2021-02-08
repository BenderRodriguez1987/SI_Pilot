using SI_Master.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SI_Master.REST
{
    public interface IAuthDataProvider
    {
        AuthData GetAuthData(bool isNavigator = false);
    }
}
