//using SIBSAPI.Models;

using SIBSAPI.DTOs;

namespace SIBSAPI.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(TLogin user);
    }
}
