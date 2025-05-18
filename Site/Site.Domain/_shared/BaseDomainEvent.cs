using MediatR;

namespace Site.Domain._shared;

public abstract class BaseDomainEvent : INotification
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}