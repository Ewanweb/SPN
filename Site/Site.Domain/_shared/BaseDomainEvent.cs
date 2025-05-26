using System.ComponentModel.DataAnnotations.Schema;
using MediatR;

namespace Site.Domain._shared;

public abstract class BaseDomainEvent : INotification
{
    [NotMapped]
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}