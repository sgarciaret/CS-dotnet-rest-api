using ApiRest.DTO;
using ApiRest.Modelo;

namespace ApiRest
{
    public static class Utilities
    {
        public static ProductDTO convertDTO( this Product p) // Se pone el this para que lo reconozca como una extensión de este objeto
        {
            if (p != null)
            {

                return new ProductDTO
                {
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    SKU = p.SKU,
                };
            }
            return null;
        }
    }
}
