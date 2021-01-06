using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SI_Master.Managers.StatusColorHandler
{
    interface IStatusColorHandler
    {
        Color GetColorFromStatus(string status);
    }
}
