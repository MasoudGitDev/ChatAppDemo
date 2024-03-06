namespace Shared.Generics;

public enum DomainEventType {
    Added ,
    Modified ,
    Deleted ,
}
public interface IDomainEvent { }
public record DomainEvent(string MethodName , DomainEventType EventType , string? description = null) : IDomainEvent;
