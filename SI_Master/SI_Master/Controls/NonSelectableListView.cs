using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SI_Master.Controls
{
    public class NonSelectableListView : ListView
    {
        public NonSelectableListView()
    : base(ListViewCachingStrategy.RetainElement)
        {
        }
    }
}
