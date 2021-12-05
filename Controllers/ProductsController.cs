using System;
using System.Collections.Generic;
using System.Linq;
using FirstApiCS.Dtos.Product;
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
        public IEnumerable<ProductOutputDto> GetProducts()
        {
            return productsRepository.GetProducts().Select(product => product.AsProductDto());
        }
        
        [HttpGet("{id}")]
        public ActionResult<ProductOutputDto> GetProduct(Guid id)
        {
            var product = productsRepository.GetProduct(id);
            if (product is null)
            {
                return NotFound();
            }

            return product.AsProductDto();
        }
        
        [HttpPost]
        public ActionResult<ProductOutputDto> CreateProduct(ProductInputDto productDto)
        {
            Product product = new()
            {
                Id = Guid.NewGuid(),
                Name = productDto.Name,
                Price = productDto.Price,
                Quantity = productDto.Quantity,
                CreatedAt = DateTimeOffset.Now
            };
            productsRepository.SetProduct(product);
            return CreatedAtAction(nameof(GetProduct), new {id = product.Id}, product.AsProductDto());
        }

        [HttpPut("{id}")]
        public ActionResult UpdateProduct(Guid id, ProductInputDto productDto)
        {
            var oldProduct = productsRepository.GetProduct(id);
            if (oldProduct is null)
            {
                return NotFound();
            }

            Product updatedProduct = oldProduct with
            {
                Name = productDto.Name,
                Price = productDto.Quantity,
                Quantity = productDto.Quantity
            };
            
            productsRepository.UpdateProduct(updatedProduct);
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(Guid id)
        {
            if (productsRepository.GetProduct(id) is null)
            {
                return NotFound();
            }
            
            productsRepository.DeleteProduct(id);
            return NoContent();
        }
    }
}