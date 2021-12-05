using System;
using System.Collections.Generic;
using System.Linq;
using FirstApiCS.Entity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace FirstApiCS.Repositories
{

    public class ProductsRepository : IProductRepository
    {
        private const string databaseName = "firstApiCS";
        private const string collectionName = "products";
        
        private readonly IMongoCollection<Product> productCollection;
        public ProductsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            productCollection = database.GetCollection<Product>(collectionName);

        }
        
        private readonly List<Product> products = new()
        {
            new Product {Id = Guid.NewGuid(), Name = "Learning CS", Price = 9, Quantity = 10, CreatedAt = DateTimeOffset.UtcNow},
            new Product {Id = Guid.NewGuid(), Name = "Learning PHP", Price = 9, Quantity = 10, CreatedAt = DateTimeOffset.UtcNow},
            new Product {Id = Guid.NewGuid(), Name = "Learning JavaScript", Price = 9, Quantity = 10, CreatedAt = DateTimeOffset.UtcNow},
        };

        public IEnumerable<Product> GetProducts()
        {
            return products;
        }

        public void SetProduct(Product product)
        {
            productCollection.InsertOne(product);
        }

        public void UpdateProduct(Product product)
        {
            var index = products.FindIndex(oldProduct => oldProduct.Id == product.Id);
            products[index] = product;
        }

        public void DeleteProduct(Guid id)
        {
            var index = products.FindIndex(oldProduct => oldProduct.Id == id);
            products.RemoveAt(index);
        }

        public Product? GetProduct(Guid id)
        {
            return products.Where(product => product.Id == id).SingleOrDefault();
        }
    }
}