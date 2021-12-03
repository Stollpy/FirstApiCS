using System;
using System.Collections.Generic;
using FirstApiCS.Entity;
using FirstApiCS.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FirstApiCS.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository productsRepository;

        public ProductsController(IProductRepository productRepository)
        {
            this.productsRepository = productRepository;
        }
        
        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            return productsRepository.GetProducts();
        }
        
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(Guid id)
        {
            var product = productsRepository.GetProduct(id);
            if (product is null)
            {
                return NotFound();
            }

            return product;
        }
    }
}