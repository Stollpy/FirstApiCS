using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IEnumerable<ProductOutputDto>> GetProductsAsync()
        {
            return (await productsRepository.GetProductsAsync())
                    .Select(product => product.AsProductDto());
        }
        
        [HttpGet("{id:guid}", Name = nameof(GetProductAsync))]
        [ActionName("GetProductAsync")]
        public async Task<ActionResult<ProductOutputDto>> GetProductAsync(Guid id)
        {
            var product = await productsRepository.GetProductAsync(id);
            if (product is null)
            {
                return NotFound();
            }

            return product.AsProductDto();
        }
        
        [HttpPost]
        public async Task<ActionResult<ProductOutputDto>> CreateProductAsync(ProductInputDto productDto)
        {
            Product product = new()
            {
                Id = Guid.NewGuid(),
                Name = productDto.Name,
                Price = productDto.Price,
                Quantity = productDto.Quantity,
                CreatedAt = DateTimeOffset.Now
            };
            await productsRepository.SetProductAsync(product);
            return CreatedAtAction(nameof(GetProductAsync), new {id = product.Id}, product.AsProductDto());
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateProductAsync(Guid id, ProductInputDto productDto)
        {
            var oldProduct = await productsRepository.GetProductAsync(id);
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
            
            await productsRepository.UpdateProductAsync(updatedProduct);
            return NoContent();
        }
        
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteProductAsync(Guid id)
        {
            if (await productsRepository.GetProductAsync(id) is null)
            {
                return NotFound();
            }
            
            await productsRepository.DeleteProductAsync(id);
            return NoContent();
        }
        
        [HttpGet("/product/default")]
        public async Task<ActionResult<ProductOutputDto>> GetProductDefault()
        {
            return (await productsRepository.GetProductDefaultAsync()).AsProductDto();
        }
    }
}