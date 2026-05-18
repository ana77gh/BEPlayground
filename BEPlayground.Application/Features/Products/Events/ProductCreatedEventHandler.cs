using MediatR;
using BEPlayground.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace BEPlayground.Application.Features.Products.Events
{
    public class ProductCreatedEventHandler : INotificationHandler<ProductCreatedEvent>
    {
        private readonly ILogger<ProductCreatedEventHandler> _logger;
        private readonly IBackgroundTaskQueue _backgroundTaskQueue;
        
        public ProductCreatedEventHandler(
            ILogger<ProductCreatedEventHandler> logger,
            IBackgroundTaskQueue backgroundTaskQueue)
        {
            _logger = logger;
            _backgroundTaskQueue = backgroundTaskQueue;
        }

        public async Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Product created event received. ProductId: {ProductId}, Name: {Name}, Price: {Price}", 
                notification.ProductId, 
                notification.Name, 
                notification.Price);

            await _backgroundTaskQueue.QueueAsync(async token =>
            {
                await Task.Delay(TimeSpan.FromSeconds(5), token);
                _logger.LogInformation(
                    "Background task processed ProductCreatedEvent. ProductId: {ProductId}, Name: {Name}, Price: {Price}",
                    notification.ProductId,
                    notification.Name,
                    notification.Price);
            });
        }
    }
}

