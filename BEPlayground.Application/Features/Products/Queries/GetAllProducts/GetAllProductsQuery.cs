using MediatR;

namespace BEPlayground.Application.Features.Products.Queries.GetAllProducts
{
    public record GetAllProductsQuery : IRequest<List<ProductDto>>;
}

