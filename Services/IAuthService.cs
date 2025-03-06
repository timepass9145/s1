using SIBSAPI.DTOs;
using SIBSAPI.Global;

namespace SIBSAPI.Services
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDto registerDto);
        Task<LoginResponse> LoginAsync(TLogin login);
        // L Task<object> LoginAsync(TLogin login);

        //Task<object> LoginAsync(TLogin login);

        //Task<string> CheckLoginExists(string loginName);
    }
}
