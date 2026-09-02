using CRN_Technical_Assessment.Application.DTOs;
using CRN_Technical_Assessment.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CRN_Technical_Assessment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/products
        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> GetAll()
        //{
        //    var products = await _productService.GetAllProducts();

        //    return Ok(products);
        //}

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var products = await _productService.GetAllAsync(
                pageNumber,
                pageSize);

            return Ok(products);
        }

        // GET: api/products/id
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetById(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public async Task<IActionResult> Create(ProductDto request)
        {
            var username = User.FindFirstValue(ClaimTypes.Name);

            if (string.IsNullOrWhiteSpace(username))
                return Unauthorized();

            var product = await _productService.CreateProduct(
                request,
                username);

            return CreatedAtAction(
                nameof(GetById),
                new { id = product.Id },
                product);
        }

        // PUT: api/products/id
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            ProductDto request)
        {
            var username = User.FindFirstValue(ClaimTypes.Name);

            if (string.IsNullOrWhiteSpace(username))
                return Unauthorized();

            var product = await _productService.UpdateProduct(
                id,
                request,
                username);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // DELETE: api/products/id
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.DeleteProduct(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}