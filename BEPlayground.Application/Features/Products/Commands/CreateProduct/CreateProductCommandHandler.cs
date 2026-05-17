using BEPlayground.Application.Common.Interfaces;
using BEPlayground.Application.Features.Products.Events;
using BEPlayground.Domain.Entities;
using MediatR;

namespace BEPlayground.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPublisher _publisher;

        public CreateProductCommandHandler(IApplicationDbContext context, IPublisher publisher)
        {
            _context = context;
            _publisher = publisher;
        }

        public async Task<Guid> Handle(
            CreateProductCommand request,
            CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Price = request.Price
            };

            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            await _publisher.Publish(
                new ProductCreatedEvent(
                    product.Id,
                    product.Name,
                    product.Price),
                cancellationToken);

            return product.Id;
        }
    }
}

