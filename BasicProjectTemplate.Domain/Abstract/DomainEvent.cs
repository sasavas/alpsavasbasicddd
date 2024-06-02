namespace BasicProjectTemplate.Domain.Abstract;

public abstract class DomainEvent
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}