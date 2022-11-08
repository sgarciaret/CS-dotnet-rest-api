using ApiRest.Modelo;

namespace ApiRest.Repository
{
    public interface IProductsInMemory
    {
        Task<IEnumerable<Product>> GiveProductsAsync(int pag, int reg);

        Task<Product> GiveProductAsync(string SKU);

        Task CreateProductAsync(Product product);

        Task ModifyProductAsync(Product product);

        Task DeleteProductAsync(string SKU);
    }
}
