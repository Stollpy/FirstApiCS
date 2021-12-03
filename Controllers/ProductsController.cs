using System;
using System.Collections.Generic;
using System.Linq;
using FirstApiCS.Dtos;
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
        public IEnumerable<ProductDto> GetProducts()
        {
            return productsRepository.GetProducts().Select(product => product.AsProductDto());
        }
        
        [HttpGet("{id}")]
        public ActionResult<ProductDto> GetProduct(Guid id)
        {
            var product = productsRepository.GetProduct(id);
            if (product is null)
            {
                return NotFound();
            }

            return product.AsProductDto();
        }
    }
}