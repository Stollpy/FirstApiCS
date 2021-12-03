using System;
using System.Collections.Generic;
using System.Linq;
using FirstApiCS.Entity;
using Microsoft.AspNetCore.Mvc;

namespace FirstApiCS.Repositories
{
    public interface IProductRepository
    {
        Product GetProduct(Guid id);
        IEnumerable<Product> GetProducts();
    }
    public class ProductsRepository : IProductRepository
    {
        private readonly List<Product> products = new()
        {
            new Product {Id = Guid.NewGuid(), Name = "Learning CS", Price = 9, CreatedAt = DateTimeOffset.UtcNow},
            new Product {Id = Guid.NewGuid(), Name = "Learning PHP", Price = 9, CreatedAt = DateTimeOffset.UtcNow},
            new Product {Id = Guid.NewGuid(), Name = "Learning JavaScript", Price = 9, CreatedAt = DateTimeOffset.UtcNow},
        };

        public IEnumerable<Product> GetProducts()
        {
            return products;
        }

        public Product? GetProduct(Guid id)
        {
            return products.Where(product => product.Id == id).SingleOrDefault();
        }
    }
}