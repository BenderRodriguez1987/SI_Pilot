using Refit;
using SI_Master.REST;
using Xamarin.Forms;

[assembly: Dependency(typeof(ApiProvider))]
namespace SI_Master.REST
{
    public class ApiProvider: IApiProvider
    {

        private const string BASE_ENDPOINT = "https://mobilebakend.soft-enginiring.ru";
        private readonly RefitSettings _settings = new RefitSettings
        {
            ContentSerializer = new NewtonsoftJsonContentSerializer()
        };

        private IAuthApi _authApi;
        public IAuthApi GetAuthApi()
        {
            if (_authApi == null)
            {
                _authApi = RestService.For<IAuthApi>(BASE_ENDPOINT, _settings);
            }
            return _authApi;
        }
    }
}
