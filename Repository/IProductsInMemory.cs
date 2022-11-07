using ApiRest.Modelo;

namespace ApiRest.Repository
{
    public interface IProductsInMemory
    {
        IEnumerable<Product> GiveProducts();

        Product GiveProduct(string SKU);

        void CreateProduct(Product product);

        void ModifyProduct(Product product);

        void DeleteProduct(string SKU);
    }
}
