using Xamarin.Forms;
using SI_Master.Services;
using SI_Master.Views;
using SI_Master.Settings;
using SI_Master.Managers;

namespace SI_Master
{
    public partial class App : Application
    {

        IAuthSettings authSettings = DependencyService.Get<IAuthSettings>();
        IAuthManager authManager = DependencyService.Get<IAuthManager>();
        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new NavigationPage(new LoginPage());
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
