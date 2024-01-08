using Identity.Application.Common.Identity.Services;
using Bc = BCrypt.Net.BCrypt;
namespace Identity.Infrastructure.Common.Identity.Services;

public class PasswordHasherService : IPasswordHasherService
{
    public string PasswordHasher(string password) =>
        Bc.HashPassword(password);

    public bool ValidatePassword(string password, string hashedPassword) =>
        Bc.Verify(password, hashedPassword);

    public PasswordHasherService()
    {
        
    }
}