using FirstApiCS.Dtos;
using FirstApiCS.Entity;

namespace FirstApiCS
{
    public static class Extensions
    {
        public static ProductDto AsProductDto(this Product product)
        {
            return new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                CreatedAt = product.CreatedAt
            };
        }
    }
}