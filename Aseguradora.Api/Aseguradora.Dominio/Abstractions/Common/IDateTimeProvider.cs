namespace Aseguradora.Domain.Abstractions.Common;

public interface IDateTimeProvider
{
    public DateTime UtcToday { get; }
    public DateTime UtcNow { get; }
}