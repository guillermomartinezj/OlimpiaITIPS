using FrontBlazor.Models;

namespace FrontBlazor.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(LoginRequest loginRequest);
        Task Logout();
        Task<bool> IsAuthenticated();
    }
}
