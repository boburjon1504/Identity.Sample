namespace Identity.Application.Common.Identity.Services;

public interface IPasswordHasherService
{
    string PasswordHasher(string password);

    bool ValidatePassword(string password, string hashedPassword);
}