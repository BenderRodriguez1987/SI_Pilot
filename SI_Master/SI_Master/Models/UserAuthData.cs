using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Xamarin.Forms;

namespace SI_Master.Models
{
    public class UserAuthData
    {
        public string Caption { get; set; }
        public string Client { get; set; }
        public string Token { get; set; }
        public bool isActive { get; set; }
        public bool IsSelected { get; set; }
        public string RemoveButtonText { get; set; } = "\ue609";
        public System.Drawing.Color SelectedUserBackground
        {
            get
            {
                if (IsSelected)
                {
                    return (Xamarin.Forms.Color)Application.Current.Resources["backgroundLightBlue"];
                }
                else
                {
                    return Xamarin.Forms.Color.Transparent;
                }
            }
        }
    }
}
