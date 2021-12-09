using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstApiCS.Entity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FirstApiCS.Repositories
{

    public class ProductsRepository : IProductRepository
    {
        private const string databaseName = "firstApiCS";
        
        private const string collectionName = "products";
        
        private readonly IMongoCollection<Product> productCollection;

        private readonly FilterDefinitionBuilder<Product> filterBuilder = Builders<Product>.Filter;
        public ProductsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            productCollection = database.GetCollection<Product>(collectionName);

        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await productCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task SetProductAsync(Product product)
        {
            await productCollection.InsertOneAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            var filter = filterBuilder.Eq(existingProduct => existingProduct.Id, product.Id);
            await productCollection.ReplaceOneAsync(filter, product);
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var filter = filterBuilder.Eq(product => product.Id, id);
            await productCollection.DeleteOneAsync(filter);
        }

        public async Task<Product> GetProductAsync(Guid id)
        {
            var filter = filterBuilder.Eq(product => product.Id, id);
            return await productCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<Product> GetProductDefaultAsync()
        {
            Product product = new()
            {
                Id = Guid.NewGuid(),
                Price = 10,
                Quantity = 10,
                Name = "Learn CSharp and Dotnet",
                CreatedAt = DateTimeOffset.UtcNow
            };
            return await Task.FromResult(product);
        }
    }
}