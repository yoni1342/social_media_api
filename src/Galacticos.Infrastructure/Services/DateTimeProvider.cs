using Galacticos.Application.Common.Interface.Services;

namespace Galacticos.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}