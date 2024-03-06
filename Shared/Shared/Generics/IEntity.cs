namespace Shared.Generics;

public interface IEntity { }

public record class Entity : IEntity {

    private readonly LinkedList<IDomainEvent> _domainEvents = new();

    protected void Raise(IDomainEvent domainEvent) {
        _domainEvents.AddLast(domainEvent);
    }

}
