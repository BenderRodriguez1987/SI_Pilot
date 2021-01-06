using SI_Master.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SI_Master.Cells
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardListViewCell : ContentView
    {

        public Action OkCLicked { get; set; }
        public CardListViewCell()
        {
            InitializeComponent();
            CardLimitsButton.Clicked += (sender, args) =>
            {
                OkCLicked.Invoke();
            };
        }
        
    }
}