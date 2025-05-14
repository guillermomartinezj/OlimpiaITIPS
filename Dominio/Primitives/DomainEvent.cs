using MediatR;

namespace Dominio.Primitives
{
    public record DomainEvent(Guid Id) : INotification;
}
