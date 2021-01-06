using SI_Master.Models;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SI_Master.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NegotiatedWoirksView : ContentView
    {

        public static readonly BindableProperty WheelsProperty =
            BindableProperty.Create("Wheels", typeof(List<Wheel>), typeof(NegotiatedWoirksView), new List<Wheel>(), propertyChanged: UpdateWheels);

        public List<Wheel> Wheels
        {
            get { return (List<Wheel>)GetValue(WheelsProperty); }
            set { SetValue(WheelsProperty, value); }
        }

        private static void UpdateWheels(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue is List<Wheel> wheels && bindable is NegotiatedWoirksView nestedListView)
            {
                // TODO fill wheels nested list view
                if (wheels != null && wheels.Count > 0)
                {
                    nestedListView.WheelWorksList.ChildrenList = wheels
                        .Select(w => {
                            var works = new List<WheelWorkItem>();
                            if (w.Works != null && w.Works.Count() > 0)
                            {
                                works = w.Works.Select(work => new WheelWorkItem
                                {
                                    Count = work.Count,
                                    Title = work.Title
                                }).ToList();
                            }
                            return new WheelView(new Wheel
                            {
                                Id = w.Id,
                                Size = w.Size,
                                Works = works
                            }) as ContentView;
                        }).ToList();
                }
            }
        }






        public static readonly BindableProperty AdditionalWorksProperty =
            BindableProperty.Create("AdditionalWorks", typeof(List<Models.AdditionalWork>), typeof(NegotiatedWoirksView), new List<Models.AdditionalWork>(), propertyChanged: UpdateAdditionalWorks);

        public List<Models.AdditionalWork> AdditionalWorks
        {
            get { return (List<Models.AdditionalWork>)GetValue(AdditionalWorksProperty); }
            set { SetValue(AdditionalWorksProperty, value); }
        }

        private static void UpdateAdditionalWorks(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue is List<Models.AdditionalWork> wheels && bindable is NegotiatedWoirksView nestedListView)
            {
                // TODO fill additional works nested list view
                // TODO hide "Дополнительные работы" title if additional works is null or empty

                if (wheels != null && wheels.Count > 0)
                {
                    nestedListView.AdditionalWorksStack.IsVisible = true;
                    nestedListView.AdditionalWorksList.ChildrenList = wheels
                        .Select(aw => new Views.AdditionalWork(
                            new Models.AdditionalWork
                            {
                                Title = aw.Title,
                                Price = aw.Price
                            }
                        ) as ContentView)
                        .ToList();
                }
                else
                {
                    nestedListView.AdditionalWorksStack.IsVisible = false;
                }
            }
        }

        public NegotiatedWoirksView()
        {
            InitializeComponent();
        }
    }
}