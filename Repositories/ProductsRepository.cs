using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Product> GetProducts()
        {
            return productCollection.Find(new BsonDocument()).ToList();
        }

        public void SetProduct(Product product)
        {
            productCollection.InsertOne(product);
        }

        public void UpdateProduct(Product product)
        {
            var filter = filterBuilder.Eq(existingProduct => existingProduct.Id, product.Id);
            productCollection.ReplaceOne(filter, product);
        }

        public void DeleteProduct(Guid id)
        {
            var filter = filterBuilder.Eq(product => product.Id, id);
            productCollection.DeleteOne(filter);
        }

        public Product? GetProduct(Guid id)
        {
            var filter = filterBuilder.Eq(product => product.Id, id);
            return productCollection.Find(filter).SingleOrDefault();
        }
    }
}