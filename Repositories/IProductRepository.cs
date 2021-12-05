using System;
using System.Collections.Generic;
using FirstApiCS.Entity;

namespace FirstApiCS.Repositories
{
    public interface IProductRepository
    {
        Product GetProduct(Guid id);
        IEnumerable<Product> GetProducts();
    
        void SetProduct(Product product);
    
        void UpdateProduct(Product product);
    
        void DeleteProduct(Guid id);
    
    }
}

