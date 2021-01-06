using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SI_Master.Models
{
    public enum MenuItemType
    {
        Map,
        QRCode,
        BarCodeScaner,
        DeskTop,
        Settlements,
        Cards,
        Transactions,
        ArchiveCardLimits,
        ArchiveCardOrder,
        ArchiveCardBlock,
        Dashboard,
        ChangeUser,
        Exit,
        ArchiveRequestsHeader
    }
    public class HomeMenuItem 
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }

        public string Icon { get; set; }
        public string IconColor { get; set; }
        public bool IsSeparator { get; set; } = false;
    }
}
