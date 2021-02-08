using System.Threading.Tasks;

namespace SI_Master.Services
{
    public interface IAuthService
    {
        Task Login(string login, string phone);

        Task Verify(string login, string code, string phone);

        void LogOut();

        bool IsAuthorized();
    }
}
