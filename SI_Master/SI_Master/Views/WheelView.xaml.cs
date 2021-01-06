using SI_Master.Models;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SI_Master.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class WheelView : ContentView
    {

        private readonly Action<Wheel> _onTap;
        private readonly Wheel _wheel;

        public string WheelId => _wheel?.Id;
        public string Size => _wheel.Size.ToString();
        public List<ContentView> WorkViews => GetWorkViews();

        public WheelView(Wheel wheel, Action<Wheel> onTap = null)
        {
            _onTap = onTap;
            _wheel = wheel;
            InitializeComponent();
            BindingContext = this;
        }

        private List<ContentView> GetWorkViews()
        {
            var workViews = new List<ContentView>();
            foreach (WheelWorkItem workItem in _wheel.Works)
            {
                workViews.Add(new WorksView(workItem));
            }
            return workViews;
        }

        private void CellTapped(object sender, System.EventArgs e)
        {
            _onTap?.Invoke(_wheel);
        }
    }
}