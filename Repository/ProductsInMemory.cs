using ApiRest.Modelo;

namespace ApiRest.Repository
{
    public class ProductsInMemory : IProductsInMemory
    {
        private readonly List<Product> products = new List<Product>()
        {
            new Product{ Id = 1, Name = "Martillo", Description = "Martillo super preciso", Price = 12.99, RegistrationDate = DateTime.Now, SKU ="MART01" },
            new Product{ Id = 2, Name = "Caja de clavos", Description = "100 unidades de clavos", Price = 11, RegistrationDate = DateTime.Now, SKU = "CLAV01" },
            new Product{ Id = 3, Name = "Destornillador", Description = "Excelente destornillador", Price = 9.99, RegistrationDate = DateTime.Now, SKU = "DEST01" },
            new Product{ Id = 4, Name = "Bombilla", Description = "Bombilla muy luminosa", Price = 3, RegistrationDate = DateTime.Now, SKU = "BOMB01" }
        };

        public async Task<IEnumerable<Product>> GiveProductsAsync()
        {
            return await Task.FromResult(products);
        }

        public async Task<Product> GiveProductAsync(string SKU)
        {
            return await Task.FromResult(products.Where(p => p.SKU == SKU).SingleOrDefault());
        }

        public async Task CreateProductAsync(Product product)
        {
            products.Add(product);
            await Task.CompletedTask;
        }

        public async Task ModifyProductAsync(Product p)
        {
            int index = products.FindIndex(productExixt=>productExixt.Id== p.Id);
            products[index] = p;
            await Task.CompletedTask;
        }

        public async Task DeleteProductAsync(string SKU)
        {
            int index = products.FindIndex(productExixt => productExixt.SKU == SKU);
            products.RemoveAt(index);
            await Task.CompletedTask;
        }
    }
}
