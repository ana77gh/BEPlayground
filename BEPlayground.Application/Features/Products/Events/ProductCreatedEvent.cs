using MediatR;

namespace BEPlayground.Application.Features.Products.Events
{
    public record ProductCreatedEvent(
        Guid ProductId,
        string Name,
        decimal Price
    ) : INotification;
}


