namespace Aseguradora.Domain.Abstractions.Common;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcToday => DateTime.UtcNow.Date;

    public DateTime UtcNow => DateTime.UtcNow;
}