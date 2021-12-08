using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FirstApiCS.Entity;
using Microsoft.AspNetCore.Mvc;

namespace FirstApiCS.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetProductAsync(Guid id);
        Task<IEnumerable<Product>> GetProductsAsync();
    
        Task SetProductAsync(Product product);
    
        Task UpdateProductAsync(Product product);
    
        Task DeleteProductAsync(Guid id);

        Task<Product> GetProductDefaultAsync();

    }
}

