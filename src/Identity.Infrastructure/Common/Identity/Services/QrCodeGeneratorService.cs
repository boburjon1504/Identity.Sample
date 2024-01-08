using Identity.Application.Common.Identity.Services;

namespace Identity.Infrastructure.Common.Identity.Services;

public class QrCodeGeneratorService : IQrCodeGeneratorService
{
    public ValueTask<bool> GenerateToken(string message)
    {
        throw new NotImplementedException();
    }
}