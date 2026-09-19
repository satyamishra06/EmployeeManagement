using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EmployeeManagement.Authentication;
using EmployeeManagement.Data;
using EmployeeManagement.DTOs;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeManagement.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly JwtSettings _jwtSettings;
    private readonly PasswordHasher<User> _passwordHasher;

    public AuthService(
        AppDbContext context,
        IOptions<JwtSettings> jwtSettings)
    {
        _context = context;
        _jwtSettings = jwtSettings.Value;
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task<AuthResponseDto> RegisterAsync(
        RegisterDto registerDto)
    {
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u =>
                u.Email == registerDto.Email);

        if (existingUser != null)
        {
            throw new InvalidOperationException(
                "A user with this email already exists.");
        }

        var user = new User
        {
            Username = registerDto.Username,
            Email = registerDto.Email,
            Role = "User"
        };

        user.PasswordHash = _passwordHasher.HashPassword(
            user,
            registerDto.Password);

        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return new AuthResponseDto
        {
            Token = GenerateJwtToken(user),
            Username = user.Username,
            Email = user.Email,
            Role = user.Role
        };
    }

    public async Task<AuthResponseDto?> LoginAsync(
        LoginDto loginDto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u =>
                u.Email == loginDto.Email);

        if (user == null)
        {
            return null;
        }

        var result = _passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            loginDto.Password);

        if (result == PasswordVerificationResult.Failed)
        {
            return null;
        }

        return new AuthResponseDto
        {
            Token = GenerateJwtToken(user),
            Username = user.Username,
            Email = user.Email,
            Role = user.Role
        };
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(
                JwtRegisteredClaimNames.Sub,
                user.Id.ToString()),

            new Claim(
                JwtRegisteredClaimNames.Email,
                user.Email),

            new Claim(
                ClaimTypes.Name,
                user.Username),

            new Claim(
                ClaimTypes.Role,
                user.Role)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtSettings.Key));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                _jwtSettings.ExpirationMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}