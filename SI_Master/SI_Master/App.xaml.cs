using Xamarin.Forms;
using SI_Master.Services;
using SI_Master.Views;
using SI_Master.Settings;
using SI_Master.Managers;
using Newtonsoft.Json;

namespace SI_Master
{
    public partial class App : Application
    {
        private readonly IAuthService _authService = DependencyService.Get<IAuthService>();
        IAuthSettings authSettings = DependencyService.Get<IAuthSettings>();
        IAuthManager authManager = DependencyService.Get<IAuthManager>();
        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            };

            DependencyService.Register<MockDataStore>();
            if (_authService.IsAuthorized())
            {
                MainPage = new NavigationPage(new MainPage(0));
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
