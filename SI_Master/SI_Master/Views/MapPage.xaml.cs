using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace SI_Master.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage {
        public MapPage()
        {
            InitializeComponent();
            Title = "Карта";
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await GetGeoLocatio();
        }

        async Task GetGeoLocatio()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);
                //Position position = new Position(location.Latitude, location.Longitude);
                Position position = new Position(56.796975, 53.230891);
                MapSpan mapSpan = new MapSpan(position, 0.01, 0.01);
                ScreenMap.MoveToRegion(mapSpan);
                List<Pin> pins = new List<Pin>();
                pins.Add(new Pin
                {
                    Position = new Position(56.796975, 53.230891),
                    Label = "   "
                });
                //pins.Add(new Pin
                //{
                //    Position = new Position(56.8429, 53.2867),
                //    Label = "АЗС"
                //});
                //pins.Add(new Pin
                //{
                //    Position = new Position(56.8347, 53.1203),
                //    Label = "АЗС"
                //});
                //pins.Add(new Pin
                //{
                //    Position = new Position(56.8347, 53.1203),
                //    Label = "АЗС"
                //});
                //pins.Add(new Pin
                //{
                //    Position = new Position(56.8349113, 53.1116808),
                //    Label = "АЗС"
                //});
                //pins.Add(new Pin
                //{
                //    Position = new Position(56.822781, 53.1270012),
                //    Label = "АЗС"
                //});
                //pins.Add(new Pin
                //{
                //    Position = new Position(56.8293204, 53.127043),
                //    Label = "АЗС"
                //});
                //pins.Add(new Pin
                //{
                //    Position = new Position(56.8288236, 53.1286666),
                //    Label = "АЗС"
                //});
                //pins.Add(new Pin
                //{
                //    Position = new Position(56.8255776, 53.1178199),
                //    Label = "АЗС"
                //});
                //pins.Add(new Pin
                //{
                //    Position = new Position(56.8251125, 53.1263353),
                //    Label = "АЗС"
                //});
                //pins.Add(new Pin
                //{
                //    Position = new Position(56.829349, 53.1298458),
                //    Label = "АЗС"
                //});
                //pins.Add(new Pin
                //{
                //    Position = new Position(56.8279394, 53.1044006),
                //    Label = "АЗС"
                //});
                //pins.Add(new Pin
                //{
                //    Position = position,
                //    Label = "I am here"
                //});
                foreach (var p in pins)
                {
                    ScreenMap.Pins.Add(p);
                }

                ScreenMap.IsShowingUser = true;

            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }
    }
}