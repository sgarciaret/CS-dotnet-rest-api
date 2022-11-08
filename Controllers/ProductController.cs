using ApiRest.DTO;
using ApiRest.Modelo;
using ApiRest.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiRest.Controllers
{
    [Route("products")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductController : Controller
    {
        private readonly IProductsInMemory repository;

        public ProductController(IProductsInMemory repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var productsList = (await repository.GiveProductsAsync()).Select(p=>p.convertDTO());

            if (productsList.Count() == 0)
            {
                return (IEnumerable<ProductDTO>)NotFound();
            }

            return productsList;
        }

        [HttpGet("{codProduct}")]
        [Authorize]
        public async Task<ActionResult<ProductDTO>> GetProduct(string codProduct)
        {
            var product = (await repository.GiveProductAsync(codProduct)).convertDTO();

            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ProductDTO>> CreateProduct(ProductDTO p)
        {
            Product product = new Product
            {
                //Id = repository.GiveProducts().Max(x => x.Id) + 1,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                SKU = p.SKU,
                RegistrationDate = DateTime.Now,
            };

            await repository.CreateProductAsync(product);
            return product.convertDTO();
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<ProductDTO>> ModifyProduct(string codProduct, ProductUpdateDTO p)
        {
            Product productExist = await repository.GiveProductAsync(codProduct);

            if (productExist == null)
            {
                return NotFound();
            }

            productExist.Name = p.Name;
            productExist.Description = p.Description;
            productExist.Price = p.Price;

            await repository.ModifyProductAsync(productExist);

            return productExist.convertDTO();
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<ProductDTO>> DeleteProduct(string codProduct)
        {
            Product productExist = await repository.GiveProductAsync(codProduct);

            if (productExist == null)
            {
                return NotFound();
            }

            await repository.DeleteProductAsync(codProduct);

            return  NoContent();
        }
    }
}
