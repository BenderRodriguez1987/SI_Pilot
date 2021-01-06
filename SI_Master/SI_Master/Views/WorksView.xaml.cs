using SI_Master.Models;
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
    public partial class WorksView : ContentView
    {
        private const string PICE = "шт.";

        private readonly WheelWorkItem _workItem;

        public string Title => _workItem.Title;
        public string Count => _workItem.Count.ToString() + PICE;

        public WorksView(WheelWorkItem workItem)
        {
            _workItem = workItem;
            InitializeComponent();
            BindingContext = this;
        }
    }
}