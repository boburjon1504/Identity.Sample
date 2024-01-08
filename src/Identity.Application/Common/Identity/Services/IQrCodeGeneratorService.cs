namespace Identity.Application.Common.Identity.Services;

public interface IQrCodeGeneratorService
{
    ValueTask<bool> GenerateToken(string message);
}