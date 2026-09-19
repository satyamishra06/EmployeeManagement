using EmployeeManagement.DTOs;

namespace EmployeeManagement.Services;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);

    Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
}