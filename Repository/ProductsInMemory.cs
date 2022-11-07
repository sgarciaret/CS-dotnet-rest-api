﻿using ApiRest.Modelo;

namespace ApiRest.Repository
{
    public class ProductsInMemory : IProductsInMemory
    {
        private readonly List<Product> products = new List<Product>()
        {
            new Product{ Id = 1, Name = "Martillo", Description = "Martillo super preciso", Price = 12.99, RegistrationDate = DateTime.Now, SKU ="MART01" },
            new Product{ Id = 2, Name = "Caja de clavos", Description = "100 unidades de clavos", Price = 110, RegistrationDate = DateTime.Now, SKU = "CLAV01" },
            new Product{ Id = 3, Name = "Destornillador", Description = "Excelente destornillador", Price = 9.99, RegistrationDate = DateTime.Now, SKU = "DEST01" },
            new Product{ Id = 4, Name = "Bombilla", Description = "Bombilla muy luminosa", Price = 3, RegistrationDate = DateTime.Now, SKU = "BOMB01" }
        };

        public IEnumerable<Product> GiveProducts()
        {
            return products;
        }

        public Product GiveProduct(string SKU)
        {
            return products.Where(p => p.SKU == SKU).SingleOrDefault();
        }

        public void CreateProduct(Product product)
        {
            products.Add(product);
        }

        public void ModifyProduct(Product p)
        {
            int index = products.FindIndex(productExixt=>productExixt.Id== p.Id);
            products[index] = p;
        }

        public void DeleteProduct(string SKU)
        {
            int index = products.FindIndex(productExixt => productExixt.SKU == SKU);
            products.RemoveAt(index);
        }
    }
}
