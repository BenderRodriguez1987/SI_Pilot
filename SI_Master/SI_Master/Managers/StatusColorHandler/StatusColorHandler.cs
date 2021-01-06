using SI_Master.Managers.StatusColorHandler;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

[assembly:Dependency(typeof(StatusColorHandler))]
namespace SI_Master.Managers.StatusColorHandler
{
    class StatusColorHandler : IStatusColorHandler
    {


        public System.Drawing.Color GetColorFromStatus(string status)
        {
            Color backColor = Color.Transparent;
            switch (status)
            {
                case "В обработке":
                    backColor = (Color)Application.Current.Resources["backgroundBlue"];
                    break;
                case "Обработана":
                    backColor = (Color)Application.Current.Resources["PositiveBalanceBackgroundColor"];
                    break;
                case "Не найдена":
                    backColor = (Color)Application.Current.Resources["NegariveBalanceBackgroundColor"];
                    break;
                case "Отправлена":
                    backColor = (Color)Application.Current.Resources["backgroundLightBlue"];
                    break;
                case "Неактуальна":
                    backColor = (Color)Application.Current.Resources["backgroundGray"];
                    break;
                case "Отменена оператором":
                    backColor = (Color)Application.Current.Resources["backgroundGray"];
                    break;
            }
            return backColor;
        }
    }
}
