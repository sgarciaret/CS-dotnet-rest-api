using ApiRest.DTO;
using ApiRest.Modelo;
using ApiRest.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ApiRest.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductsInMemory repository;

        public ProductController(IProductsInMemory repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IEnumerable<ProductDTO> GetProducts()
        {
            var productsList = repository.GiveProducts().Select(p=>p.convertDTO());
            return productsList;
        }

        [HttpGet("{codProduct}")]
        public ActionResult<ProductDTO> GetProduct(string codProduct)
        {
            var product = repository.GiveProduct(codProduct).convertDTO();

            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        [HttpPost]
        public ActionResult<ProductDTO> CreateProduct(ProductDTO p)
        {
            Product product = new Product
            {
                Id = repository.GiveProducts().Max(x=>x.Id) + 1,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                SKU = p.SKU,
                RegistrationDate = DateTime.Now,
            };

            repository.CreateProduct(product);
            return product.convertDTO();
        }

        [HttpPut]
        public ActionResult<ProductDTO> ModifyProduct(string codProduct, ProductUpdateDTO p)
        {
            Product productExist = repository.GiveProduct(codProduct);

            if (productExist == null)
            {
                return NotFound();
            }

            productExist.Name = p.Name;
            productExist.Description = p.Description;
            productExist.Price = p.Price;

            repository.ModifyProduct(productExist);

            return productExist.convertDTO();
        }

        [HttpDelete]
        public ActionResult<ProductDTO> DeleteProduct(string codProduct)
        {
            Product productExist = repository.GiveProduct(codProduct);

            if (productExist == null)
            {
                return NotFound();
            }

            repository.DeleteProduct(codProduct);

            return  NoContent();
        }
    }
}
