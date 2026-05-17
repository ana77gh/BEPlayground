using MediatR;
using Microsoft.Extensions.Logging;

namespace BEPlayground.Application.Features.Products.Events
{
    public class ProductCreatedEventHandler : INotificationHandler<ProductCreatedEvent>
    {
        private readonly ILogger<ProductCreatedEventHandler> _logger;
        
        public ProductCreatedEventHandler(ILogger<ProductCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Product created event received. ProductId: {ProductId}, Name: {Name}, Price: {Price}", 
                notification.ProductId, 
                notification.Name, 
                notification.Price);
            return Task.CompletedTask;
        }
    }
}

