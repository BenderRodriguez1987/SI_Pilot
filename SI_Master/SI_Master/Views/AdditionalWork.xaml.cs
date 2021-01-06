using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SI_Master.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdditionalWork : ContentView
    {

        private readonly Models.AdditionalWork _work;

        public string WorkTitle => _work?.Title;
        public string WorkPrice => GetWorkPriceFormatted();

        public AdditionalWork(Models.AdditionalWork work, Action<Models.AdditionalWork> onTap = null)
        {
            InitializeComponent();
            _work = work;
            BindingContext = this;
        }

        private string GetWorkPriceFormatted()
        {
            return _work.Price.ToString().Replace(",", ".") + " руб.";
        }

    }
}