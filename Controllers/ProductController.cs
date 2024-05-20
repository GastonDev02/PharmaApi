using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Si es necesario para ApplicationDbContext
using PharmaApi.Data;
using PharmaApi.Dto;
using PharmaApi.Models;
using PharmaApi.Services;


namespace PharmaApi.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        //GET VERBS
        [HttpGet(Name = "get-products")]
        public ActionResult<IEnumerable<ProductModel>> GetProducts()
        {
            var products = _productService.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id}", Name = "get-product-by-id")]
        public ActionResult<ProductModel> GetProductById(int id)
        {
            var productById = _productService.GetProduct(id);
            if (productById == null)
            {
                return NotFound();
            }
            return Ok(productById);
        }

        [HttpGet("get-by-key", Name = "search-product-by-key")]
        public ActionResult<IEnumerable<ProductModel>> SearchProductsByKey([FromQuery] string key)
        {
            try
            {
                var products = _productService.SearchProduct(key);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al buscar productos por clave: {ex.Message}");
            }
        }


        //POST VERBS
        [HttpPost(Name = "create-product")]
        public ActionResult<ProductModel> CreateProduct([FromBody] ProductModel newProduct)
        {
            try
            {
                _productService.CreateProduct(newProduct);
                return CreatedAtAction(nameof(GetProductById), new { id = newProduct.id_producto }, newProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al crear el producto: {ex.Message}");
            }
        }

        //PUT VERBS
        [HttpPut("{id}", Name = "update-product")]
        public ActionResult UpdateProduct(int id, UpdateProductDto updateProduct)
        {
            try
            {
                _productService.UpdateProduct(id, updateProduct);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al actualizar el producto: {ex.Message}");
            }
        }

        //DELETE VERBS
        [HttpDelete("{id}", Name = "delete-product")]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                _productService.DeleteProduct(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al eliminar el producto: {ex.Message}");
            }
        }
    }
}
