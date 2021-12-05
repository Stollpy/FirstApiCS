using FirstApiCS.Dtos.Product;
using FirstApiCS.Entity;

namespace FirstApiCS
{
    public static class Extensions
    {
        public static ProductOutputDto AsProductDto(this Product product)
        {
            return new ProductOutputDto()
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