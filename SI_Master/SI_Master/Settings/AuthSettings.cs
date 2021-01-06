using Newtonsoft.Json;
using Plugin.Settings;
using SI_Master.Models;
using SI_Master.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(AuthSettings))]
namespace SI_Master.Settings
{
    public class AuthSettings : IAuthSettings
    {
        public int Active
        {
            get
            {
                if (CrossSettings.Current.Contains("ActiveUser"))
                {
                    return CrossSettings.Current.GetValueOrDefault("ActiveUser", -1);
                }
                return -1;
            }
            set
            {
                CrossSettings.Current.AddOrUpdateValue("ActiveUser", value);
            }
        }

        public bool Remove(string login)
        {
            List<UserAuthData> users = Read();
            int idx = users.FindIndex(u => { return string.Equals(u.Client, login); });
            if (idx != -1)
            {
                users.RemoveAt(idx);
                if (Active == idx) Active = -1;
                Write(users);
                return true;
            }
            return false;
        }

        public int AddOrUpdate(UserAuthData user)
        {
            List<UserAuthData> users = Read();
            int idx = users.FindIndex(u => { return string.Equals(u.Token, user.Token); });
            if (idx != -1)
            {
                users.RemoveAt(idx);
                users.Insert(idx, user);
            }
            else
            {
                idx = users.Count;
                users.Add(user);
            }
            Write(users);
            return idx;
        }

        public void Write(List<UserAuthData> users)
        {
            string json = JsonConvert.SerializeObject(users, new JsonSerializerSettings() { StringEscapeHandling = StringEscapeHandling.EscapeNonAscii });
            CrossSettings.Current.AddOrUpdateValue("Users", json);
        }



        public List<UserAuthData> Read()
        {
            List<UserAuthData> usersList = new List<UserAuthData>();
            if (CrossSettings.Current.Contains("Users"))
            {
                try
                {
                    usersList = JsonConvert.DeserializeObject<List<UserAuthData>>(CrossSettings.Current.GetValueOrDefault("Users", ""),
                        new JsonSerializerSettings() { StringEscapeHandling = StringEscapeHandling.EscapeNonAscii });
                    return usersList;
                }
                catch
                { }
            }
            return usersList;
        }
        //захардкодил зареганного пользователя пользователя
        //    List<UserAuthData> userrsList = new List<UserAuthData>();
        //    UserAuthData currentUser = new UserAuthData()
        //    {
        //        Caption = "Ц0000820",
        //    Client = "329BD8",
        //    Token = "w+XZZ0xkFXGo3ojUJ2DciPxeWMuX45mPe9s7vV9tZHM="
        //};
        //    userrsList.Add(currentUser);
        //return userrsList;

        public UserAuthData ActiveUser()
        {
            List<UserAuthData> userlist = Read();
            UserAuthData currentUser = new UserAuthData();
            if (Active >= 0) {
                currentUser = userlist[Active];
            }
            return currentUser;
        }
    }
}
